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
using JovDK.SerializingTools.Json;

// from project
// ...


namespace KoolGames.Test03.GamePlay.VFX.Testing.Showcase
{
    public partial class Asmb_SwirlEffectShowcaseScene : MonoBehaviour
    {
        void SetupAllButton()
        {
            _playElephantSwirlAnimationButton.SetOnClickIfNotNull(PlayElephantSwirlAnimationButton);
            _playBothSwirlAnimationsButton.SetOnClickIfNotNull(PlayBothSwirlAnimationsButton);
            _playBettleSwirlAnimationButton.SetOnClickIfNotNull(PlayBettleSwirlAnimationButton);
        }

        void PlayElephantSwirlAnimationButton()
        {
            Vector3 centerPosition = _baseSwirlPositionTransform.position;
            _elephantSwivelModel.PlaySwirlAnimation(centerPosition, _swirlDuration, false);

            InstantiateMergeEffect(centerPosition);
        }

        void PlayBothSwirlAnimationsButton()
        {
            Vector3 centerPosition = _baseSwirlPositionTransform.position;
            _elephantSwivelModel.PlaySwirlAnimation(centerPosition, _swirlDuration, false);
            _beetleSwivelModel.PlaySwirlAnimation(centerPosition, _swirlDuration, false);

            InstantiateMergeEffect(centerPosition);
        }

        void PlayBettleSwirlAnimationButton()
        {
            Vector3 centerPosition = _baseSwirlPositionTransform.position;
            _beetleSwivelModel.PlaySwirlAnimation(centerPosition, _swirlDuration, false);

            InstantiateMergeEffect(centerPosition);
        }

        [SerializeField] Vector3 _particleScale = new Vector3(0.5f, 0.5f, 0.5f);

        async void InstantiateMergeEffect(Vector3 centerPosition)
        {
            // return;
            float waitSeconds = _swirlDuration * 0.35f;
            await Task.Delay(Mathf.FloorToInt(waitSeconds * 1000));

            GameObject particleInstance = Instantiate(_mergeParticleEffectPrefab, centerPosition, Quaternion.Euler(0, 0, 0));
            particleInstance.transform.localScale = _particleScale;
        }
    }
}
