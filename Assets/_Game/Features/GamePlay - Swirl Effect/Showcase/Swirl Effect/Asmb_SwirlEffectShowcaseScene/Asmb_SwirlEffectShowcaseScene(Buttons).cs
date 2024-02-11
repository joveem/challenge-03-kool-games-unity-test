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
            _elephantSwivelModel.PlaySwirlAnimation(centerPosition, _swirlDuration);
        }

        void PlayBothSwirlAnimationsButton()
        {
            Vector3 centerPosition = _baseSwirlPositionTransform.position;
            _elephantSwivelModel.PlaySwirlAnimation(centerPosition, _swirlDuration);
            _beetleSwivelModel.PlaySwirlAnimation(centerPosition, _swirlDuration);
        }

        void PlayBettleSwirlAnimationButton()
        {
            Vector3 centerPosition = _baseSwirlPositionTransform.position;
            _beetleSwivelModel.PlaySwirlAnimation(centerPosition, _swirlDuration);
        }
    }
}
