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
using KoolGames.Test03.GamePlay.Entities.Views;


namespace KoolGames.Test03.GamePlay.PlayerController
{
    public partial class PlayerMovementLogic : MonoBehaviour
    {
        void HandleMovement()
        {
            if (_playerEntity == null || _playerEntity.Rigidbody == null)
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to HandleMovement!" + "\n" +
                    "_playerEntity OR _playerEntity.Rigidbody IS NULL!" + "\n" +
                    "";
                DebugExtension.DevLog(debugText);
                return;
            }

            Rigidbody playerRigidbody = _playerEntity.Rigidbody;

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
                playerRigidbody.rotation = Quaternion.LookRotation(inputPosition, Vector3.up);

            Vector3 direction = playerRigidbody.transform.rotation * (Vector3.forward * inputPosition.magnitude);

            playerRigidbody.velocity = direction;

            // if (direction.magnitude > 0.1f)
            //     _playerView.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        // [SerializeField] Transform _playerView

        void HandleAnimation()
        {
            if (_playerEntity == null || _playerEntity.Rigidbody == null)
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to HandleAnimation!" + "\n" +
                    "_playerEntity OR _playerEntity.Rigidbody IS NULL!" + "\n" +
                    "";
                DebugExtension.DevLog(debugText);
                return;
            }

            Rigidbody playerRigidbody = _playerEntity.Rigidbody;

            Vector3 lastPosition = _playerEntity.LastPosition;
            Vector3 realVelocity = (playerRigidbody.position - lastPosition) / Time.fixedDeltaTime;
            float playerZVelocity = realVelocity.magnitude;
            _playerEntity.LastPosition = playerRigidbody.position;

            if (_playerEntity.EntityView is MovableEntityView movableEntityView)
                movableEntityView.ApplyZVelocity(playerZVelocity);

            List<Entity> underDomainEntities = _playerEntity.GetDominatedEntitiesList();
            foreach (Entity dominatedEntity in underDomainEntities)
            {
                dominatedEntity.DoIfNotNull(() =>
                {
                    if (dominatedEntity.EntityView is MovableEntityView movableEntityView)
                        movableEntityView.ApplyZVelocity(playerZVelocity);
                });
            }
        }
    }
}
