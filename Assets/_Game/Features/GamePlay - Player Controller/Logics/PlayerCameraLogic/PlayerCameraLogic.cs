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
    public partial class PlayerCameraLogic : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        [SerializeField] Entity _playerEntity;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] Transform _cameraPositionPivot;
        [SerializeField] Vector3 _cameraPositionDelta = new Vector3(0f, 0f, 0f);

        // layers
        int _localPlayerLayer;


        // void Awake()
        // {

        // }

        void Start()
        {
            SetiInitialState();
        }

        // void Update()
        // {

        // }

        void LateUpdate()
        {
            HandleCameraPosition();
        }

        // void FixedUpdate()
        // {

        // }
    }
}
