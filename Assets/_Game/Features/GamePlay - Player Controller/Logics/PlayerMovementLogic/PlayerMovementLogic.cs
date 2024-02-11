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

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        float _currentXMoveVelocityFactor = 0f;
        float _maxXMoveVelocity = 1f;
        float _xMoveAccelerationFactor = 3f;
        float _currentZMoveVelocityFactor = 0f;
        float _maxZMoveVelocity = 1f;
        float _zMoveAccelerationFactor = 3f;

        Vector3 _currentMovementInput;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] Rigidbody _playerRigidbody;
        [SerializeField] List<MovableView> _playerViewsList = new List<MovableView>();
        [SerializeField] PlayerView _playerView;


        // void Awake()
        // {

        // }

        // void Start()
        // {

        // }

        // void Update()
        // {

        // }

        void FixedUpdate()
        {
            HandleMovement();
            HandleAnimation();
        }

        // void FixedUpdate()
        // {

        // }
    }
}
