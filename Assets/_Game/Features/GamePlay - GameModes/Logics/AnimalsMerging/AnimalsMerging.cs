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
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.Montary;
using KoolGames.Test03.GamePlay.Scenario;


namespace KoolGames.Test03.GamePlay.GameModes
{
    public partial class AnimalsMerging : MonoBehaviour
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        MapData _mapData = null;
        PathNodesHandler _pathNodesHandler = null;
        MontaryLogic _montaryLogic = null;


        [Space(5), Header("[ State ]"), Space(10)]

        PlayerEntity _playerEntity;
        Dictionary<AnimalEntity, AnimalData> _currentAnimalsList = new Dictionary<AnimalEntity, AnimalData>();


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Transform _mergeCenterPositionTransform;
        [SerializeField] Transform _mergeDestinationPositionTransform;
        [SerializeField] List<Transform> _mergePositionsTransformsList = new List<Transform>();


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] List<AnimalEntity> _animalsPrefabsList = new List<AnimalEntity>();
        [SerializeField] int _initialAnimalsAmount = 20;

        [Space(10)]
        [SerializeField] float _mergeTranslateDuration = 0.5f;
        [SerializeField] float _mergeAnimationDuration = 5f;


        // void Awake()
        // {

        // }

        void Start()
        {
            ApplyInitialState();
            SubscribeAllListeners();
        }

        void Update()
        {
            HandleCapturingArea();
            HandleCapturing();
        }

        // void FixedUpdate()
        // {

        // }
    }
}
