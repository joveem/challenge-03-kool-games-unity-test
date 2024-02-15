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
using KoolGames.Test03.GamePlay.Entities.Views;


namespace KoolGames.Test03.GamePlay.Entities
{
    public partial class Entity : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        List<Entity> _dominatedEntitiesList = new List<Entity>();

        public Vector3 LastPosition;


        [Space(5), Header("[ Parts ]"), Space(10)]

        public EntityView EntityView;
        public Rigidbody Rigidbody;
        public Collider Collider;


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

        // void FixedUpdate()
        // {

        // }

        protected void HandleAnimation(float deltaTime)
        {
            if (Rigidbody == null)
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to HandleAnimation!" + "\n" +
                    "_playerEntity.Rigidbody IS NULL!" + "\n" +
                    "";
                DebugExtension.DevLog(debugText);
                return;
            }

            Rigidbody playerRigidbody = Rigidbody;

            Vector3 lastPosition = LastPosition;
            Vector3 realVelocity = (playerRigidbody.position - lastPosition) / deltaTime;
            float playerZVelocity = realVelocity.magnitude;
            LastPosition = playerRigidbody.position;

            if (EntityView is MovableEntityView movableEntityView)
            {
                if (!movableEntityView.GetIsGrounded())
                    playerZVelocity = 0f;

                movableEntityView.ApplyZVelocity(playerZVelocity);
            }

            List<Entity> underDomainEntities = GetDominatedEntitiesList();
            foreach (Entity dominatedEntity in underDomainEntities)
            {
                dominatedEntity.DoIfNotNull(() =>
                {
                    if (dominatedEntity.EntityView is MovableEntityView movableEntityView)
                        movableEntityView.ApplyZVelocity(playerZVelocity);
                });
            }
        }
    }
}
