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
using JovDK.Math;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Bots;
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.Scenario;


namespace KoolGames.Test03.GamePlay.GameModes
{
    public class IdleMovement : IState
    {
        // dependencies
        AnimalEntity _animalEntity;
        BotPathHandler _botPathHandler;
        MapData _mapData;

        // configs
        Vector3 _maxDeltaArea = new Vector3(20f, 0f, 20f);


        public IdleMovement(
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
            _botPathHandler.HandlePathIndexing();
            _botPathHandler.HandleBotRotation(deltaTime);
            _botPathHandler.HandleBotPositioning(deltaTime, 0.5f);
        }

        void IState.OnEnter()
        {
            // DebugExtension.DevLog("IdleMovement 01 #>".ToColor(GoodColors.Green));

            Vector3 randomNearPosition = GetRandomNearValidPosition();
            _botPathHandler.SetGoalPosition(randomNearPosition);
        }

        void IState.OnExit()
        {
            // DebugExtension.DevLog("IdleMovement 01 <#".ToColor(GoodColors.Orange));
        }

        Vector3 GetRandomNearValidPosition()
        {
            Vector3 value = default;

            Vector3 initialPosition = _animalEntity.transform.position;
            Vector3 areaStartPosition = _mapData.GetAnimalsSpawnStart();
            Vector3 areaEndPosition = _mapData.GetAnimalsSpawnEnd();

            value = MathSpatial.GetRandomNearPositionInArea(
                        initialPosition,
                        areaStartPosition,
                        areaEndPosition,
                        _maxDeltaArea);

            return value;
        }
    }
}
