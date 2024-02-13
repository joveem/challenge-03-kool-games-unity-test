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
using KoolGames.Test03.GamePlay.Entities.Views;

// from project
// ...


namespace KoolGames.Test03.GamePlay.PlayerController.Testing.Showcase
{
    public partial class Asmb_GameSceneController : MonoBehaviour
    {
        void SubscribeAllListeners()
        {
            _playerControllerLogic.OnMovementInputChangedCallback += _playerMovementLogic.OnControllerMovementInputChanged;
        }

        void UnsubscribeAllListeners()
        {
            _playerControllerLogic.OnMovementInputChangedCallback -= _playerMovementLogic.OnControllerMovementInputChanged;
        }
    }
}
