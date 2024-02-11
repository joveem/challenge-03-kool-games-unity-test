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


namespace KoolGames.Test03.GamePlay.VFX
{
    public partial class SwivelModel : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        bool _isPlayingSwirlBeginning = false;
        float _cycleT = 0;
        float _swirlBeginningDuration;
        Vector3 _swirlCenterGlobalPosition;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] List<SkinnedMeshRenderer> _objectAMeshList = new List<SkinnedMeshRenderer>();


        [Space(5), Header("[ Configs ]"), Space(10)]

        // [SerializeField] float _cycleDuration = 4;

        [SerializeField] Vector2 _rotationFactorRemap = new Vector2(0, 0.02f);
        [SerializeField] Vector2 _additionalCurveForceRemap = new Vector2(3000, 30000);
        [SerializeField] float _objectAAdditionalCurvesAmountByUnit = 4f;
        [SerializeField] float _objectAAdditionalCurveDelta = 0f;


        // void Awake()
        // {

        // }

        // void Start()
        // {

        // }

        void Update()
        {
            HandleCycleUpdate();
            HandleMaterialsUpdate();
        }

        // void FixedUpdate()
        // {

        // }
    }
}
