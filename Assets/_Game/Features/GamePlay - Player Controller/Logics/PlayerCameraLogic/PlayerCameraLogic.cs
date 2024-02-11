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

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        Vector3 _currentCameraInput;
        float _cameraSensitivityFactor = 1f;
        float _baseCameraSensitivity = 180f;
        float _minVerticalAngle = -85f;
        float _maxVerticalAngle = 70f;

        public Action<Vector3> OnAimHitUpdateCallback = null;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] Transform _cameraPositionPivot;
        [SerializeField] Transform _cameraRotationPivot;
        [SerializeField] Rigidbody _playerRigidbody;
        // [SerializeField] Transform _cameraDistancePivot;
        // [SerializeField] TankEntityView _baseTankEntityView;

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

        // void LateUpdate()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
