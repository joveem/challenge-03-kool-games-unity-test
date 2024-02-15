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
using JovDK.GamePlay.Camera;
using JovDK.Generic.SpatialUI;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay;
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.GameModes;
using KoolGames.Test03.GamePlay.Montary;
using KoolGames.Test03.GamePlay.PlayerController;
using KoolGames.Test03.GamePlay.Scenario;


namespace KoolGames.Test03.GamePlay
{
    public partial class Asmb_GameScene : MonoBehaviour
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        [SerializeField] SpatialUIHandler _spatialUIHandler = null;
        [SerializeField] MapData _mapData = null;
        [SerializeField] PathNodesHandler _pathNodesHandler = null;
        [SerializeField] MontaryLogic _montaryLogic = null;
        [SerializeField] AnimalsMerging _animalsMerging = null;
        [SerializeField] PlayerEntity _playerEntity = null;
        [SerializeField] CameraRotationAnimationView _cameraRotationAnimationView = null;
        [SerializeField] PlayerMovementLogic _playerMovementLogic = null;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]


        [SerializeField] Transform _mergeStationTransform;
        [SerializeField] RectTransform _mergeStationUIBubbleTransform;
        [SerializeField] BotsController _botsController;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        void Awake()
        {
            SetupDependencies();
        }

        void Start()
        {
            SetInitialState();
            SubscribeAllListeners();
        }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
