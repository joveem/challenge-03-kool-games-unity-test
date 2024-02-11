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
        public void OnControllerCameraInputChanged(Vector3 inputVector)
        {
            HandleCameraInput(inputVector);
        }

        void OnAimHitUpdate(Vector3 hitGlobalPosition)
        {
            OnAimHitUpdateCallback?.Invoke(hitGlobalPosition);
        }
    }
}
