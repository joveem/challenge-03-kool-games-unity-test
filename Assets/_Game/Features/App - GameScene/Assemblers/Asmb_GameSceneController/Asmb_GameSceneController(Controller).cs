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
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Entities.Views;
using KoolGames.Test03.GamePlay.Entities;


namespace KoolGames.Test03.GamePlay.PlayerController.Testing.Showcase
{
    public partial class Asmb_GameSceneController : MonoBehaviour
    {
        void SetInitialState()
        {
            // _baseTankEntityView.DoIfNotNull(() => _baseTankEntityView.IsLocal = true);
        }

        void DoAnimalCatch(
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
                    "ERROR trying to DoAnimalCatch!" + "\n" +
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
                    "ERROR trying to DoAnimalCatch!" + "\n" +
                    "ALREADY dominated!" + "\n" +
                    "";
                DebugExtension.DevLogWarning(debugText);
                return;
            }

            HandlePreviosAnimalDomain(basePlayerEntity);

            PlayerView playerView = (PlayerView)basePlayerEntity.EntityView;
            AnimalView animalView = (AnimalView)baseAnimalEntity.EntityView;
            Transform playerTransform = basePlayerEntity.transform;
            Transform animalTransform = baseAnimalEntity.transform;
            Transform playerViewTransform = playerView.transform;
            Transform animalViewTransform = animalView.transform;

            playerViewTransform.SetParent(null);

            Vector3 mountDeltaPosition = animalView.MountContainerTransform.position - playerViewTransform.position;
            Vector3 middleMountDeltaPosition = mountDeltaPosition * 0.5f;
            Vector3 middlePosition = playerViewTransform.position + middleMountDeltaPosition + (Vector3.up * 2);

            Tween playerTransformInitialTween = playerTransform.DOMove(animalViewTransform.position, _catchAnimationDuration);
            playerTransform.DORotate(animalViewTransform.rotation.eulerAngles, _catchAnimationDuration);

            Tween playerViewInitialTween = playerViewTransform.DOMoveY(middlePosition.y, _catchAnimationDuration * 0.5f).SetEase(Ease.OutCubic);
            playerViewTransform.DOMoveX(animalView.MountContainerTransform.position.x, _catchAnimationDuration).SetEase(Ease.OutCubic);
            playerViewTransform.DOMoveZ(animalView.MountContainerTransform.position.z, _catchAnimationDuration).SetEase(Ease.OutCubic);

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

                basePlayerEntity.AddEntityDomain(baseAnimalEntity);
            };
        }

        void HandlePreviosAnimalDomain(Entity basePlayerEntity)
        {
            List<Entity> dominatedEntitiesList = basePlayerEntity.GetDominatedEntitiesList();

            foreach (Entity dominatedEntity in dominatedEntitiesList)
            {
                dominatedEntity.DoIfNotNull(() =>
                {
                    bool isAnimal = dominatedEntity is AnimalEntity;
                    if (isAnimal)
                    {
                        basePlayerEntity.RemoveEntityDomain(dominatedEntity);

                        dominatedEntity.transform.SetParent(null);
                        basePlayerEntity.EntityView.transform.SetParent(basePlayerEntity.transform);
                    }
                });
            }
        }
    }
}
