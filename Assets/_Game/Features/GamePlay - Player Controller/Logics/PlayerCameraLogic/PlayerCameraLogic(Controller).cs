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

// from project
// ...


namespace KoolGames.Test03.GamePlay.PlayerController
{
    public partial class PlayerCameraLogic : MonoBehaviour
    {
        void SetiInitialState()
        {
            _localPlayerLayer = LayerMask.NameToLayer("local-player");
        }

        // void HandleCameraPosition()
        // {
        //     if (_baseTankEntityView == null)
        //         return;

        //     Vector3 turretGlobalPosition = _baseTankEntityView.GetTurretGlobalPosition();
        //     _cameraPositionPivot.position = turretGlobalPosition;
        // }

        void HandleCameraInput(Vector3 inputVector)
        {
            _currentCameraInput = inputVector;

            float currentXInput = _currentCameraInput.x;
            float currentYInput = _currentCameraInput.y;

            Vector3 cameraRotationEuler = _cameraRotationPivot.transform.rotation.eulerAngles;
            // Vector3 playerRotationEuler = _playerRigidbody.rotation.eulerAngles;
            Vector3 playerRotationEuler = _playerRigidbody.transform.rotation.eulerAngles;

            cameraRotationEuler.x += currentYInput * _baseCameraSensitivity * _cameraSensitivityFactor * -1;
            playerRotationEuler.y += currentXInput * _baseCameraSensitivity * _cameraSensitivityFactor;

            while (cameraRotationEuler.x > 180f)
                cameraRotationEuler.x -= 360f;

            cameraRotationEuler.x = Mathf.Clamp(cameraRotationEuler.x, -_maxVerticalAngle, -_minVerticalAngle);

            _cameraRotationPivot.transform.rotation = Quaternion.Euler(cameraRotationEuler);
            // _playerRigidbody.MoveRotation(Quaternion.Euler(playerRotationEuler));
            _playerRigidbody.transform.rotation = Quaternion.Euler(playerRotationEuler);
        }

        // void HandleCameraDistance()
        // {
        //     Vector3 rotationPivotPosition = _cameraRotationPivot.transform.position;
        //     Quaternion rotationPivotGlobalRotation = _cameraRotationPivot.transform.rotation;
        //     Vector3 rotationPivotGlobalRotationDirection = rotationPivotGlobalRotation * Vector3.back;

        //     bool hasHit = Physics.Raycast(
        //                     rotationPivotPosition,
        //                     rotationPivotGlobalRotationDirection,
        //                     out RaycastHit raycastHit,
        //                     _maxDistance);

        //     float actualCameraDistance = _maxDistance;

        //     if (hasHit)
        //         actualCameraDistance = Vector3.Distance(rotationPivotPosition, raycastHit.point);

        //     _cameraDistancePivot.localPosition = new Vector3(0f, 0f, -actualCameraDistance);
        // }

        // void HandleAimEmitting()
        // {
        //     Vector3 rotationPivotPosition = _cameraRotationPivot.transform.position;
        //     Quaternion rotationPivotGlobalRotation = _cameraRotationPivot.transform.rotation;
        //     Vector3 rotationPivotGlobalRotationDirection = rotationPivotGlobalRotation * Vector3.forward;

        //     bool hasHit = Physics.Raycast(
        //                     rotationPivotPosition,
        //                     rotationPivotGlobalRotationDirection,
        //                     out RaycastHit raycastHit,
        //                     Mathf.Infinity);

        //     bool isLocalPlayer = false;
        //     Vector3 aimGlobalPosition;

        //     if (hasHit)
        //     {
        //         isLocalPlayer = raycastHit.collider.gameObject.layer == _localPlayerLayer;

        //         if (!isLocalPlayer)
        //             aimGlobalPosition = raycastHit.point;
        //     }

        //     if (hasHit && !isLocalPlayer)
        //         aimGlobalPosition = raycastHit.point;
        //     else
        //     {
        //         // fix aiming problems by changing the aim position
        //         // to a far point on the camera aim direction 
        //         Vector3 farNormalizedDirection = rotationPivotGlobalRotationDirection.normalized * 100000;
        //         aimGlobalPosition = rotationPivotPosition + farNormalizedDirection;
        //     }

        //     // Debug.DrawLine(rotationPivotPosition, aimGlobalPosition, Color.red);
        //     OnAimHitUpdate(aimGlobalPosition);
        // }
    }
}
