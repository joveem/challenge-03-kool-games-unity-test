// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using DG.Tweening;
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.Entities.Views;


namespace KoolGames.Test03.GamePlay.Montary
{
    public partial class MontaryLogic : MonoBehaviour
    {
        public void DoAnimalMount(
            Entity basePlayerEntity,
            Entity baseAnimalEntity)
        {
            bool isValidEntities =
                basePlayerEntity.EntityView is PlayerView &&
                baseAnimalEntity.EntityView is AnimalView;

            if (!isValidEntities)
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to DoAnimalMount!" + "\n" +
                    "INVALID ENTITIES!" + "\n" +
                    "";
                DebugExtension.DevLogWarning(debugText);
                return;
            }

            bool isAlreadyDominated = basePlayerEntity.HasDomain(baseAnimalEntity);

            if (isAlreadyDominated)
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to DoAnimalMount!" + "\n" +
                    "ALREADY dominated!" + "\n" +
                    "";
                DebugExtension.DevLogWarning(debugText);
                return;
            }

            PlayerView playerView = (PlayerView)basePlayerEntity.EntityView;
            AnimalView animalView = (AnimalView)baseAnimalEntity.EntityView;
            Transform playerTransform = basePlayerEntity.transform;
            Transform animalTransform = baseAnimalEntity.transform;
            Transform playerViewTransform = playerView.transform;
            Transform animalViewTransform = animalView.transform;

            DismountResult dismountResult = HandlePreviosAnimalDomain(basePlayerEntity);
            if (dismountResult.HasDismountedSomeAnimal)
                playerView.StopMountAnimation();

            playerViewTransform.SetParent(null);

            Vector3 startPosition = playerViewTransform.position;
            Vector3 entityFinalPosition = animalTransform.position;
            Quaternion entityFinalRotation = animalTransform.rotation;
            Vector3 viewFinalPosition = animalView.MountContainerTransform.position;

            Vector3 viewMovementDelta = viewFinalPosition - startPosition;
            Vector3 halfViewMovementDelta = viewMovementDelta * 0.5f;
            Vector3 viewMiddlePosition = startPosition + halfViewMovementDelta + (Vector3.up * 2);

            // basePlayerEntity.Collider.enabled = false;
            basePlayerEntity.AddEntityDomain(baseAnimalEntity);
            baseAnimalEntity.Rigidbody.isKinematic = true;
            baseAnimalEntity.Collider.enabled = false;
            animalView.ApplyZVelocity(0f);

            Tween playerTransformInitialTween = playerTransform.DOMove(entityFinalPosition, _catchAnimationDuration);
            playerTransform.DORotate(entityFinalRotation.eulerAngles, _catchAnimationDuration);

            playerView.SetIsGrounded(false);
            Tween playerViewInitialTween = playerViewTransform.DOMoveY(viewMiddlePosition.y, _catchAnimationDuration * 0.5f).SetEase(Ease.OutCubic);
            playerViewTransform.DOMoveX(viewFinalPosition.x, _catchAnimationDuration).SetEase(Ease.OutCubic);
            playerViewTransform.DOMoveZ(viewFinalPosition.z, _catchAnimationDuration).SetEase(Ease.OutCubic);

            playerViewInitialTween.onComplete = () =>
            {
                playerViewTransform.SetParent(animalView.MountContainerTransform);

                playerView.PlayMountAnimation();

                playerViewTransform.DOLocalMoveY(0f, _catchAnimationDuration * 0.5f).SetEase(Ease.InCubic);
                playerViewTransform.DOLocalRotate(Vector3.zero, _catchAnimationDuration * 0.5f).SetEase(Ease.InCubic);
            };

            playerTransformInitialTween.onComplete = () =>
            {
                baseAnimalEntity.transform.SetParent(playerTransform);
                baseAnimalEntity.transform.DOLocalMove(Vector3.zero, 0.1f);
                baseAnimalEntity.transform.DOLocalRotate(Vector3.zero, 0.1f);

                playerView.SetIsGrounded(true);

                OnAnimalMountComplete();
            };
        }

        public void DoAnimalDismount(Entity basePlayerEntity)
        {
            DismountResult dismountResult = HandlePreviosAnimalDomain(basePlayerEntity);

            if (dismountResult.HasDismountedSomeAnimal)
            {
                Entity baseAnimalEntity = dismountResult.DismountedAnimalEntity;
                PlayerView playerView = (PlayerView)basePlayerEntity.EntityView;
                AnimalView animalView = (AnimalView)baseAnimalEntity.EntityView;
                Transform playerTransform = basePlayerEntity.transform;
                Transform animalTransform = baseAnimalEntity.transform;
                Transform playerViewTransform = playerView.transform;
                Transform animalViewTransform = animalView.transform;

                Vector3 startPosition = playerViewTransform.position;
                Vector3 entityFinalPosition = animalView.DismountPivotTransform.position;
                Quaternion entityFinalRotation = animalView.DismountPivotTransform.rotation;
                Vector3 viewFinalPosition = animalView.DismountPivotTransform.position;

                Vector3 viewMovementDelta = viewFinalPosition - startPosition;
                Vector3 halfViewMovementDelta = viewMovementDelta * 0.5f;
                Vector3 viewMiddlePosition = startPosition + halfViewMovementDelta + (Vector3.up * 2);

                Tween playerTransformInitialTween = playerTransform.DOMove(entityFinalPosition, _catchAnimationDuration);
                playerTransform.DORotate(entityFinalRotation.eulerAngles, _catchAnimationDuration);

                playerView.SetIsGrounded(false);

                Tween playerViewInitialTween = playerViewTransform.DOMoveY(viewMiddlePosition.y, _catchAnimationDuration * 0.5f).SetEase(Ease.OutCubic);
                playerViewTransform.DOMoveX(viewFinalPosition.x, _catchAnimationDuration).SetEase(Ease.OutCubic);
                playerViewTransform.DOMoveZ(viewFinalPosition.z, _catchAnimationDuration).SetEase(Ease.OutCubic);

                playerViewInitialTween.onComplete = () =>
                {
                    playerViewTransform.SetParent(playerTransform);

                    playerView.StopMountAnimation();

                    playerViewTransform.DOLocalMoveY(0f, _catchAnimationDuration * 0.5f).SetEase(Ease.InCubic);
                    playerViewTransform.DOLocalRotate(Vector3.zero, _catchAnimationDuration * 0.5f).SetEase(Ease.InCubic);
                };

                playerTransformInitialTween.onComplete = () =>
                {
                    playerViewTransform.DOLocalMove(Vector3.zero, 0.1f);
                    playerViewTransform.DOLocalRotate(Vector3.zero, 0.1f);

                    playerView.SetIsGrounded(true);

                    // basePlayerEntity.Collider.enabled = true;
                    baseAnimalEntity.Rigidbody.isKinematic = false;
                    baseAnimalEntity.Collider.enabled = true;

                    OnAnimalDismountComplete();
                };
            }
            else
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to DoAnimalDismount!" + "\n" +
                    "ALREADY dismounted!" + "\n" +
                    "";
                DebugExtension.DevLogWarning(debugText);
                return;
            }
        }

        DismountResult HandlePreviosAnimalDomain(Entity basePlayerEntity)
        {
            DismountResult dismountResult = new DismountResult();

            List<Entity> dominatedEntitiesList = basePlayerEntity.GetDominatedEntitiesList();
            List<AnimalEntity> toRemoveEntitiesList = new List<AnimalEntity>();

            foreach (Entity dominatedEntity in dominatedEntitiesList)
            {
                dominatedEntity.DoIfNotNull(() =>
                {
                    if (dominatedEntity is AnimalEntity animalEntity)
                        toRemoveEntitiesList.Add(animalEntity);
                });
            }

            foreach (AnimalEntity animalEntity in toRemoveEntitiesList)
            {
                basePlayerEntity.RemoveEntityDomain(animalEntity);

                animalEntity.transform.SetParent(null);
                basePlayerEntity.EntityView.transform.SetParent(basePlayerEntity.transform);

                if (!dismountResult.HasDismountedSomeAnimal)
                {
                    dismountResult.HasDismountedSomeAnimal = true;
                    dismountResult.DismountedAnimalEntity = animalEntity;
                }
            }

            return dismountResult;
        }

    }
}
