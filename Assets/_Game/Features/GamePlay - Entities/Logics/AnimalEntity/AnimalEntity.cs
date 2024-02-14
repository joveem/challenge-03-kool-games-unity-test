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


namespace KoolGames.Test03.GamePlay.Entities
{
    public partial class AnimalEntity : Entity
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        // idle
        public float RemaingIdleTime = 0f;
        // domain

        public Entity OwnerEntity = null;
        public Entity DomineeringEntity = null;
        public bool IsDominated = false;
        public bool IsMounted = false;
        public bool IsBeeingDominated = false;
        public float DominationTryCooldown = 0f;
        public float CurrentDomainForce = 0f;
        // public float RequiredDomainForce = 5f;
        public float RequiredDomainForce = 1f;
        // movement
        public float CurrentZMoveVelocityFactor = 0f;
        public float MaxZMoveVelocity = 0.0001f;
        public float ZMoveAccelerationFactor = 3f;

        // rotation
        public float CurrentYRotationVelocityFactor = 0f;
        public float MaxYRotationVelocity = 180f;
        public float YRotationAccelerationFactor = 5f;

        // path following
        public float GoalMaxDistance = 10f;
        public Vector3 CurrentGoalPosition = default;
        public List<GraphPathNode> CurrentPathNodesList = new List<GraphPathNode>();
        public int CurrentPathIndex = -1;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


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
            if (!IsMounted)
                HandleAnimation(Time.fixedDeltaTime);
        }
    }
}
