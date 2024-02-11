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


namespace KoolGames.Test03.GamePlay.VFX
{
    public partial class SwivelModel : MonoBehaviour
    {
        void HandleCycleUpdate()
        {
            if (!_isPlayingSwirlBeginning)
                return;

            _cycleT += 1 / _swirlBeginningDuration * Time.deltaTime;

            if (_cycleT > 1)
            {
                _isPlayingSwirlBeginning = false;
                _cycleT -= 0;
            }
        }

        [SerializeField] Vector3 _centerPositionDelta = new Vector3(0, 0, 0);

        void HandleMaterialsUpdate()
        {
            if (!_isPlayingSwirlBeginning)
                return;

            float finalRotationFactor = Remap(_cycleT, 0f, 1f, _rotationFactorRemap.x, _rotationFactorRemap.y);
            float finalAdditionalCurveForce = Remap(_cycleT, 0f, 1f, _additionalCurveForceRemap.x, _additionalCurveForceRemap.y);

            Vector3 oldBasePosition = _swirlCenterGlobalPosition;
            foreach (SkinnedMeshRenderer meshRenderer in _objectAMeshList)
            {
                float finalCurveDelta = Vector3.Distance(meshRenderer.transform.position, oldBasePosition) + _objectAAdditionalCurveDelta;
                finalCurveDelta = finalCurveDelta / 2;

                Vector3 fixedPosition = new Vector3(
                    oldBasePosition.x,
                    oldBasePosition.y,
                    oldBasePosition.z * -1);
                
                fixedPosition += _centerPositionDelta;

                // Vector3 fixedPosition = new Vector3(
                //     oldBasePositio.x - meshRenderer.transform.position.x,
                //     oldBasePositio.y - meshRenderer.transform.position.y,
                //     (oldBasePositio.z - meshRenderer.transform.position.z) * -1);
                meshRenderer.DoIfNotNull(() =>
                {
                    int i = 0;
                    foreach (Material material in meshRenderer.materials)
                    {
                        i++;
                        material.SetVector("_CenterPosition", fixedPosition);

                        material.SetFloat("_RotationFactor", finalRotationFactor * -1);
                        material.SetFloat("_AdditionalCurvesForceFactor", finalAdditionalCurveForce);

                        material.SetFloat("_AdditionalCurvesAmountByUnit", _objectAAdditionalCurvesAmountByUnit);
                        material.SetFloat("_AdditionalCurvePivotDelta", finalCurveDelta);
                        // "_AdditionalCurvesAmountByUnit"
                        // "_AdditionalCurvePivotDelta"
                    }
                    // Debug.Log("AAA | i = " + i);
                });
            }
        }

        float Remap(
            float value,
            float minA, float maxA,
            float minB, float maxB)
        {

            float result = 0;

            float normal = Mathf.InverseLerp(minA, maxA, value);
            result = Mathf.Lerp(minB, maxB, normal);

            return result;
        }
    }
}
