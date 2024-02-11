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
    public partial class PlayerControllerLogic : MonoBehaviour
    {
        void SubscribeAllListeners()
        {
            _leftAnalog.OnDragAsInputCallback += OnMovementInputChanged;
            _leftAnalog.OnTouchFinishCallback += OnMovementAnalogRelease;
            // _rightTouchDragArea.OnDragAsInputCallback += OnCameraInputChanged;
            // _rightTouchDragArea.OnTouchFinishCallback += OnCameraDragAreaRelease;
        }

        void UnsubscribeAllListeners()
        {
            _leftAnalog.OnDragAsInputCallback -= OnMovementInputChanged;
            _leftAnalog.OnTouchFinishCallback -= OnMovementAnalogRelease;
            // _rightTouchDragArea.OnDragAsInputCallback -= OnCameraInputChanged;
            // _rightTouchDragArea.OnTouchFinishCallback -= OnCameraDragAreaRelease;
        }

        void OnMovementAnalogRelease(Vector3 _)
        {
            OnMovementInputChanged(Vector3.zero);
        }

        // void OnCameraDragAreaRelease(Vector3 _)
        // {
        //     OnCameraInputChanged(Vector3.zero);
        // }
    }
}
