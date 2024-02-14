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


namespace KoolGames.Test03.GamePlay
{
    public partial class RopeView : MonoBehaviour
    {
        void SetInitialState()
        {
            bool hasToHide = true;

#if UNITY_EDITOR
            if (_DEBUG_ignoreInitialHiding)
                hasToHide = false;
#endif

            if (hasToHide)
                HideRope();
        }

        void HandleEndPosition()
        {
            _meshTransform.DoIfNotNull(() =>
            {
                if (_endPositionTransform != null)
                {
                    Vector3 startPosition = _meshTransform.position;
                    Vector3 endPosition = _endPositionTransform.position + _endPositionDelta;
                    float distance = Vector3.Distance(startPosition, endPosition);

                    Vector3 lookDirection = endPosition - startPosition;
                    Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

                    _meshTransform.rotation = lookRotation;
                    _meshTransform.localScale = new Vector3(1f, 1f, distance);
                }
                else
                    HideRope();
            });
        }

        public void SetEndPositionTransform(Transform endPositionTransform)
        {
            _endPositionTransform = endPositionTransform;
        }

        public void SetEndPositionDelta(Vector3 endPositionDelta)
        {
            _endPositionDelta = endPositionDelta;
        }
    }
}
