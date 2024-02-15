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


namespace KoolGames.Test03.GamePlay.Entities.Views
{
    public partial class MovableEntityView : EntityView, IMovableView
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        protected bool _isGrounded = true;



        [Space(5), Header("[ Parts ]"), Space(10)]

        [SerializeField] protected Animator _animator;
        [SerializeField] ParticleSystem _footStepsParticle;


        [Space(5), Header("[ Configs ]"), Space(10)]

        [SerializeField] float _footStepsParticleMinVelocity = 5f;


        // void Awake()
        // {

        // }

        // void Start()
        // {

        // }

        // void Update()
        // {

        // }

        // void FixedUpdate()
        // {

        // }
    }
}
