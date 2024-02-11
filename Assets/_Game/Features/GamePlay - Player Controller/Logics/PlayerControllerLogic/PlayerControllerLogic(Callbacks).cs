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
        void OnMovementInputChanged(Vector3 inputVector)
        {
            if (_hasControll && _isAlive)
                OnMovementInputChangedCallback?.Invoke(inputVector);
        }

        void OnCameraInputChanged(Vector3 inputVector)
        {
            if (_hasControll && _isAlive)
                OnCameraInputChangedCallback?.Invoke(inputVector);
        }
    }
}
