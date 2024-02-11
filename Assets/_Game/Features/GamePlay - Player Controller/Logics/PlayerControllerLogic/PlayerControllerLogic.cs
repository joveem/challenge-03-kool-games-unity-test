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
using JovDK.Control.Touch;

// from project
// ...

namespace KoolGames.Test03.GamePlay.PlayerController
{
    public partial class PlayerControllerLogic : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        // bool _hasControll = false;
        // bool _isAlive = false;
        bool _hasControll = true; // TODO: REVIEW THIS
        bool _isAlive = true; // TODO: REVIEW THIS

        Vector3 _currentMovementInput;
        Vector3 _currentCameraInput;

        public Action<Vector3> OnMovementInputChangedCallback = null;
        public Action<Vector3> OnCameraInputChangedCallback = null;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] TouchAnalog _leftAnalog;
        // [SerializeField] TouchDragArea _rightTouchDragArea;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        // void Awake()
        // {

        // }

        void Start()
        {
            SubscribeAllListeners();
        }

        void Update()
        {
#if UNITY_EDITOR
            HandleDEBUGKeyboardMovement();
#endif
        }

        // void FixedUpdate()
        // {

        // }

        void OnDestroy()
        {
            UnsubscribeAllListeners();
        }

    }
}
