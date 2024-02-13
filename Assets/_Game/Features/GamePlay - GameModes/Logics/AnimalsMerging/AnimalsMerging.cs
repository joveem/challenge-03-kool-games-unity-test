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
using UnityEngine.PlayerLoop;


namespace PackageName.MajorContext.MinorContext
{
    public partial class AnimalsMerging : MonoBehaviour
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        [SerializeField] MapData _mapData = null;


        [Space(5), Header("[ State ]"), Space(10)]

        [SerializeField] PlayerEntity _playerEntity;
        Dictionary<AnimalEntity, AnimalData> _currentAnimalsList = new Dictionary<AnimalEntity, AnimalData>();
        // List<AnimalData> _currentAnimalsList = new List<AnimalData>();


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] List<AnimalEntity> _animalsPrefabsList = new List<AnimalEntity>();
        [SerializeField] int _initialAnimalsAmount = 20;


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
