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

        void HandleCameraPosition()
        {
            if (_playerEntity == null || _cameraPositionPivot == null)
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to HandleMovement!" + "\n" +
                    "_playerEntity OR _cameraPositionPivot IS NULL!" + "\n" +
                    "";
                DebugExtension.DevLog(debugText);
                return;
            }

            Vector3 cameraPosition = _playerEntity.transform.position;
            cameraPosition += _cameraPositionDelta;

            _cameraPositionPivot.position = cameraPosition;
        }
    }
}
