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
// ...


namespace KoolGames.Test03.GamePlay
{
    public partial class RopeView : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        Vector3 _endPositionDelta = default;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] Transform _meshTransform;
        [SerializeField] Transform _endPositionTransform;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] bool _DEBUG_ignoreInitialHiding = false;


        // void Awake()
        // {

        // }

        void Start()
        {
            SetInitialState();
        }

        // void Update()
        // {

        // }

        void LateUpdate()
        {
            HandleEndPosition();
        }

        // void FixedUpdate()
        // {

        // }
    }
}
