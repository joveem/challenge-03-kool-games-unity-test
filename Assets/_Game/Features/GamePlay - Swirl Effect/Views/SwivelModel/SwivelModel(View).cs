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
// ...


namespace KoolGames.Test03.GamePlay.VFX
{
    public partial class SwivelModel : MonoBehaviour
    {
        public async void PlaySwirlAnimation(
            Vector3 swirlCenterGlobalPosition,
            float duration)
        {
            float animationHalfDuration = duration / 2f;

            _swirlBeginningDuration = animationHalfDuration;
            _isPlayingSwirlBeginning = true;
            _swirlCenterGlobalPosition = swirlCenterGlobalPosition;

            float waitSeconds = _swirlBeginningDuration;
            await Task.Delay(Mathf.FloorToInt(waitSeconds * 1000));

            GameObject rotationPivotIntance = new GameObject();
            rotationPivotIntance.transform.position = _swirlCenterGlobalPosition;
            rotationPivotIntance.transform.rotation = Quaternion.Euler(0, 0, 0);

            gameObject.transform.SetParent(rotationPivotIntance.transform);


            rotationPivotIntance.transform.DORotate(new Vector3(0f, 180f * 4, 0f), animationHalfDuration, RotateMode.FastBeyond360);
            rotationPivotIntance.transform.DOScale(Vector3.zero, animationHalfDuration);
        }
    }
}
