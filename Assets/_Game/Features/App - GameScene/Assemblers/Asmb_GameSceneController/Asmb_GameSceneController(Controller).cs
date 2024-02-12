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


namespace KoolGames.Test03.GamePlay.PlayerController.Testing.Showcase
{
    public partial class Asmb_GameSceneController : MonoBehaviour
    {
        void SetInitialState()
        {
            // _baseTankEntityView.DoIfNotNull(() => _baseTankEntityView.IsLocal = true);
        }

        void DoAnimalCatch(
            Transform basePlayerTransform,
            PlayerView basePlayerView,
            AnimalView baseAnimalView)
        {
            basePlayerView.transform.SetParent(null);

            Vector3 mountDeltaPosition = baseAnimalView.MountContainerTransform.position - basePlayerView.transform.position;
            Vector3 middleMountDeltaPosition = mountDeltaPosition * 0.5f;
            Vector3 middlePosition = basePlayerView.transform.position + middleMountDeltaPosition + (Vector3.up * 2);

            Tween playerTransformInitialTween = basePlayerTransform.DOMove(baseAnimalView.transform.position, _catchAnimationDuration);
            basePlayerTransform.DORotate(baseAnimalView.transform.rotation.eulerAngles, _catchAnimationDuration);

            Tween playerViewInitialTween = basePlayerView.transform.DOMoveY(middlePosition.y, _catchAnimationDuration * 0.5f).SetEase(Ease.OutCubic);
            basePlayerView.transform.DOMoveX(baseAnimalView.MountContainerTransform.position.x, _catchAnimationDuration).SetEase(Ease.OutCubic);
            basePlayerView.transform.DOMoveZ(baseAnimalView.MountContainerTransform.position.z, _catchAnimationDuration).SetEase(Ease.OutCubic);

            playerViewInitialTween.onComplete = () =>
            {
                basePlayerView.transform.SetParent(baseAnimalView.MountContainerTransform);

                basePlayerView.PlayMountAnimation();

                basePlayerView.transform.DOLocalMoveY(0f, _catchAnimationDuration * 0.5f).SetEase(Ease.InCubic);
                basePlayerView.transform.DOLocalRotate(Vector3.zero, _catchAnimationDuration * 0.5f).SetEase(Ease.InCubic);
            };

            playerTransformInitialTween.onComplete = () =>
            {
                baseAnimalView.transform.SetParent(basePlayerTransform);
                baseAnimalView.transform.DOLocalMove(Vector3.zero, 0.1f);
                baseAnimalView.transform.DOLocalRotate(Vector3.zero, 0.1f);

                // !!! TODO: CONTINUE FROM HERE!!!!!
                // !!! TODO: CONTINUE FROM HERE!!!!!
                // !!! TODO: CONTINUE FROM HERE!!!!!
                // !!! TODO: CONTINUE FROM HERE!!!!!
                _playerMovementLogic.AddMovableView(baseAnimalView);
            };
        }
    }
}
