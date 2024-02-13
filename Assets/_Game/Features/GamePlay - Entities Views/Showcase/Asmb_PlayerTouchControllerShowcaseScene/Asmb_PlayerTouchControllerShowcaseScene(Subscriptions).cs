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
// ...


namespace KoolGames.Test03.GamePlay.PlayerController.Testing.Showcase
{
    public partial class Asmb_PlayerTouchControllerShowcaseScene : MonoBehaviour
    {
        void SubscribeAllListeners()
        {
            _playerControllerLogic.OnMovementInputChangedCallback += _playerMovementLogic.OnControllerMovementInputChanged;
            _catchAreaTrigger.OnTriggerEnterCallback += OnCatchAreaEnter;
            _catchAreaTrigger.OnTriggerExitCallback += OnCatchAreaExit;
        }

        void UnsubscribeAllListeners()
        {
            _playerControllerLogic.OnMovementInputChangedCallback -= _playerMovementLogic.OnControllerMovementInputChanged;
            _catchAreaTrigger.OnTriggerEnterCallback -= OnCatchAreaEnter;
            _catchAreaTrigger.OnTriggerExitCallback -= OnCatchAreaExit;
        }

        void OnCatchAreaEnter(Collider collider)
        {
            if (collider.tag.Equals("player"))
            {
                PlayerView playerView = (PlayerView)_playerEntity.EntityView;
                playerView.PlayCatchAnimation();
            }
        }

        void OnCatchAreaExit(Collider collider)
        {
            if (collider.tag.Equals("player"))
            {
                PlayerView playerView = (PlayerView)_playerEntity.EntityView;
                playerView.StopCatchAnimation();
            }
        }
    }
}
