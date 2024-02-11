// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;

// from company
using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using JovDK.Physics.Triggers;
using DG.Tweening;

// from project
// ...


namespace KoolGames.Test03.GamePlay.PlayerController.Testing.Showcase
{
    public partial class Asmb_PlayerTouchControllerShowcaseScene : MonoBehaviour
    {
        void HandleDEBUGInputs()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DebugExtension.DevLog("[1]".ToColor(GoodColors.Blue) + " > PlayCatchAnimation");
                PlayCatchAnimation();
            }
        }

        [SerializeField] float _catchAnimationDuration = 1f;

        void PlayCatchAnimation()
        {
            _playerView.transform.SetParent(null);

            Vector3 mountDeltaPosition = _animalView.MountContainerTransform.position - _playerView.transform.position;
            Vector3 middleMountDeltaPosition = mountDeltaPosition * 0.5f;
            Vector3 middlePosition = _playerView.transform.position + middleMountDeltaPosition + (Vector3.up * 2);

            Tween playerTransformInitialTween = _playerTransform.DOMove(_animalView.transform.position, _catchAnimationDuration);
            _playerTransform.DORotate(_animalView.transform.rotation.eulerAngles, _catchAnimationDuration);

            Tween playerViewInitialTween = _playerView.transform.DOMoveY(middlePosition.y, _catchAnimationDuration * 0.5f).SetEase(Ease.OutCubic);
            _playerView.transform.DOMoveX(_animalView.MountContainerTransform.position.x, _catchAnimationDuration).SetEase(Ease.OutCubic);
            _playerView.transform.DOMoveZ(_animalView.MountContainerTransform.position.z, _catchAnimationDuration).SetEase(Ease.OutCubic);

            playerViewInitialTween.onComplete = () =>
            {
                _playerView.transform.SetParent(_animalView.MountContainerTransform);

                _playerView.PlayMountAnimation();

                _playerView.transform.DOLocalMoveY(0f, _catchAnimationDuration * 0.5f).SetEase(Ease.InCubic);
                _playerView.transform.DOLocalRotate(Vector3.zero, _catchAnimationDuration * 0.5f).SetEase(Ease.InCubic);
            };

            playerTransformInitialTween.onComplete = () =>
            {
                _animalView.transform.SetParent(_playerTransform);
                _animalView.transform.DOLocalMove(Vector3.zero, 0.1f);
                _animalView.transform.DOLocalRotate(Vector3.zero, 0.1f);

                _playerMovementLogic.AddMovableView(_animalView);
            };
        }
    }
}
