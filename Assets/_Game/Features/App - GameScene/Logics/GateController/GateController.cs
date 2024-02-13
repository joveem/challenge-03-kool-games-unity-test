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
using JovDK.Physics.Triggers;

// from project
// ...


namespace KoolGames.Test03.GamePlay.GameScene
{
    public partial class GateController : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeReference] Animator _animator;
        [SerializeReference] TriggerEmitter _areaTrigger;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        // void Awake()
        // {

        // }

        void Start()
        {
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
