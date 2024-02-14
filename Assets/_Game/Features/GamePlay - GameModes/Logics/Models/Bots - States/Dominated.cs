// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityRandom = UnityEngine.Random;

// third
using TMPro;

// from company
using JovDK.Debug;
using JovDK.Generic.StateMachine;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Bots;
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.Scenario;


namespace KoolGames.Test03.GamePlay.GameModes
{
    public class Dominated : IState
    {

        // dependencies
        AnimalEntity _animalEntity;
        BotPathHandler _botPathHandler;
        MapData _mapData;

        // configs
        float _minDistanceToFollow = 5f;

        public Dominated(
            AnimalEntity animalEntity,
            BotPathHandler botPathHandler,
            MapData mapData)
        {
            _animalEntity = animalEntity;
            _botPathHandler = botPathHandler;
            _mapData = mapData;
        }

        void IState.Tick(float deltaTime)
        {
            if (!_animalEntity.IsMounted && _animalEntity.OwnerEntity != null)
            {
                Vector3 animalPosition = _animalEntity.transform.position;
                Vector3 ownerPosition = _animalEntity.OwnerEntity.transform.position;
                float distance = Vector3.Distance(animalPosition, ownerPosition);

                if (distance > _minDistanceToFollow)
                {
                    _botPathHandler.SetGoalPosition(ownerPosition, true);

                    _botPathHandler.HandlePathIndexing();
                    _botPathHandler.HandleBotRotation(deltaTime);
                    _botPathHandler.HandleBotPositioning(deltaTime);
                }
            }
        }

        void IState.OnEnter()
        {
            // DebugExtension.DevLog("Dominated #>".ToColor(GoodColors.Green));
        }

        void IState.OnExit()
        {
            // DebugExtension.DevLog("Dominated <#".ToColor(GoodColors.Orange));
        }
    }
}
