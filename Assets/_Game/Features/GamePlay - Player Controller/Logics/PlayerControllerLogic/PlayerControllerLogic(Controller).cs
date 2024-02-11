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
        public void SetPlayerControll(bool hasControll)
        {
            _hasControll = hasControll;
        }

        public void SetPlayerAlive(bool isAlive)
        {
            _isAlive = isAlive;
        }

        Vector3 _DEBUG_keyboardMovementInput = Vector3.zero;

        void HandleDEBUGKeyboardMovement()
        {
            float currentXInput = 0f;
            float currentYInput = 0f;

            // x axys
            if (Input.GetKey(KeyCode.A))
                currentXInput += -1f;
            if (Input.GetKey(KeyCode.D))
                currentXInput += 1f;

            // y axys
            if (Input.GetKey(KeyCode.W))
                currentYInput += 1f;
            if (Input.GetKey(KeyCode.S))
                currentYInput += -1f;

            Vector3 _currentKeyboardMovementInput = new Vector3(currentXInput, currentYInput);

            if (_currentKeyboardMovementInput != _DEBUG_keyboardMovementInput)
                OnMovementInputChanged(_currentKeyboardMovementInput);

            _DEBUG_keyboardMovementInput = _currentKeyboardMovementInput;
        }

    }
}
