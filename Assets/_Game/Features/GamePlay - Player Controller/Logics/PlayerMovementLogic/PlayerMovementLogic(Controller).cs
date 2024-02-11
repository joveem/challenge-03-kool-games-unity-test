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
using KoolGames.Test03.GamePlay.Entities;


namespace KoolGames.Test03.GamePlay.PlayerController
{
    public partial class PlayerMovementLogic : MonoBehaviour
    {
        void HandleMovement()
        {
            if (_playerRigidbody == null)
                return;

            float currentXInput = _currentMovementInput.x;
            float currentYInput = _currentMovementInput.y;

            if (_currentZMoveVelocityFactor != currentYInput)
            {
                if (_currentZMoveVelocityFactor < currentYInput)
                {
                    _currentZMoveVelocityFactor += Time.fixedDeltaTime * _zMoveAccelerationFactor;
                    _currentZMoveVelocityFactor = Mathf.Clamp(_currentZMoveVelocityFactor, -1f, currentYInput);
                }
                else
                {
                    _currentZMoveVelocityFactor -= Time.fixedDeltaTime * _zMoveAccelerationFactor;
                    _currentZMoveVelocityFactor = Mathf.Clamp(_currentZMoveVelocityFactor, currentYInput, 1f);
                }
            }

            if (_currentXMoveVelocityFactor != currentXInput)
            {
                if (_currentXMoveVelocityFactor < currentXInput)
                {
                    _currentXMoveVelocityFactor += Time.fixedDeltaTime * _xMoveAccelerationFactor;
                    _currentXMoveVelocityFactor = Mathf.Clamp(_currentXMoveVelocityFactor, -1f, currentXInput);
                }
                else
                {
                    _currentXMoveVelocityFactor -= Time.fixedDeltaTime * _xMoveAccelerationFactor;
                    _currentXMoveVelocityFactor = Mathf.Clamp(_currentXMoveVelocityFactor, currentXInput, 1f);
                }
            }


            float currentXMovementVelocity = _currentXMoveVelocityFactor * _maxXMoveVelocity;
            float currentZMovementVelocity = _currentZMoveVelocityFactor * _maxZMoveVelocity;
            Vector3 inputPosition = new Vector3(currentXMovementVelocity, 0, currentZMovementVelocity);

            if (currentXInput != 0 || currentYInput != 0)
                _playerRigidbody.rotation = Quaternion.LookRotation(inputPosition, Vector3.up);

            Vector3 direction = _playerRigidbody.transform.rotation * (Vector3.forward * inputPosition.magnitude);

            _playerRigidbody.velocity = direction;

            // if (direction.magnitude > 0.1f)
            //     _playerView.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        // [SerializeField] Transform _playerView

        Vector3 _oldPosition;
        void HandleAnimation()
        {
            Vector3 realVelocity = (_playerRigidbody.position - _oldPosition) / Time.fixedDeltaTime;
            _oldPosition = _playerRigidbody.position;

            foreach (MovableView playerView in _playerViewsList)
            {
                playerView.DoIfNotNull(() =>
                {
                    float playerZVelocity = realVelocity.magnitude;
                    playerView.ApplyZVelocity(playerZVelocity);
                });
            }
        }

        public void AddMovableView(MovableView movableView)
        {
            if (!_playerViewsList.Contains(movableView))
                _playerViewsList.Add(movableView);
        }

        public void RemoveMovableView(MovableView movableView)
        {
            if (_playerViewsList.Contains(movableView))
                _playerViewsList.Remove(movableView);
        }
    }
}
