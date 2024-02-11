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
using Codice.Client.Commands;

// from project
// ...


namespace KoolGames.Test03.GamePlay.VFX.Testing.Showcase
{
    public partial class Asmb_SwirlEffectShowcaseScene : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        // [Space(5), Header("[ State ]"), Space(10)]

        // bool _state;


        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] List<SkinnedMeshRenderer> _objectAMeshList = new List<SkinnedMeshRenderer>();

        [SerializeField] Button _playElephantSwirlAnimationButton;
        [SerializeField] Button _playBothSwirlAnimationsButton;
        [SerializeField] Button _playBettleSwirlAnimationButton;

        [SerializeField] SwivelModel _beetleSwivelModel;
        [SerializeField] SwivelModel _elephantSwivelModel;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] float _swirlDuration = 3f;
        [SerializeField] Transform _baseSwirlPositionTransform;


        // void Awake()
        // {

        // }

        void Start()
        {
            SetupAllButton();
        }

        float _cycleT = 0;
        [SerializeField] float _cycleDuration = 4;

        [SerializeField] Vector2 _rotationFactorRemap = new Vector2(0, 0.02f);
        [SerializeField] Vector2 _additionalCurveForceRemap = new Vector2(3000, 30000);
        [SerializeField] float _objectAAdditionalCurvesAmountByUnit = 4f;
        [SerializeField] float _objectAAdditionalCurveDelta = 0f;


        void Update()
        {
            // HandleCycleUpdate();
            // HandleMaterialsUpdate();
        }

        // void FixedUpdate()
        // {

        // }
    }
}
