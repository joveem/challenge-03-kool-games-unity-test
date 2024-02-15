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
using JovDK.Generic.SpatialUI;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay;
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.GameModes;
using KoolGames.Test03.GamePlay.Montary;
using KoolGames.Test03.GamePlay.Scenario;
using JovDK.GamePlay.Camera;


namespace KoolGames.Test03.GamePlay
{
    public partial class Asmb_GameScene : MonoBehaviour
    {
        void SubscribeAllListeners()
        {
            _montaryLogic.DoIfNotNull(
                () =>
                {
                    _montaryLogic.OnAnimalMountCompleteCallback += OnAnimalMountComplete;
                    _montaryLogic.OnAnimalDismountCompleteCallback += OnAnimalDismountComplete;
                });
        }

        void OnAnimalMountComplete()
        {
            _playerMovementLogic.SetMaxMoveVelocity(8f);
            _cameraRotationAnimationView.SetVelocityMultiplierFactor(5f / 8f);
        }

        void OnAnimalDismountComplete()
        {
            _playerMovementLogic.SetMaxMoveVelocity(4f);
            _cameraRotationAnimationView.SetVelocityMultiplierFactor(0.25f);
        }
    }
}
