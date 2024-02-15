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
using JovDK.Physics.Triggers;

// from project
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.Entities.Views;
using KoolGames.Test03.GamePlay.Montary;


namespace KoolGames.Test03.GamePlay.PlayerController.Testing.Showcase
{
    public partial class Asmb_PlayerTouchControllerShowcaseScene : MonoBehaviour
    {

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        [SerializeField] MontaryLogic _montaryLogic = null;


        // bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] PlayerControllerLogic _playerControllerLogic;
        [SerializeField] PlayerMovementLogic _playerMovementLogic;
        [SerializeField] PlayerCameraLogic _playerCameraLogic;

        [SerializeField] TriggerEmitter _catchAreaTrigger;

        [SerializeField] Entity _playerEntity;
        [SerializeField] Entity _beetleEntity;
        [SerializeField] Entity _beetlephantEntity;
        [SerializeField] Entity _elephantEntity;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] float _catchAnimationDuration = 1f;



        void Awake()
        {
            SubscribeAllListeners();
        }

        // void Start()
        // {
        //     SetInitialState();
        // }

        void Update()
        {
#if UNITY_EDITOR
            HandleDEBUGInputs();
#endif
        }

        // void FixedUpdate()
        // {

        // }

        // void OnDestroy()
        // {
        //     UnsubscribeAllListeners();
        // }
    }
}
