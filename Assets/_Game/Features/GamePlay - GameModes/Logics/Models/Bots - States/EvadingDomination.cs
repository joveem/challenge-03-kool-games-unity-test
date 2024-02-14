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
    public class EvadingDomination : IState
    {
        // dependencies
        AnimalEntity _animalEntity;
        BotPathHandler _botPathHandler;
        MapData _mapData;

        // configs
        float _maxDominationTryCooldown = 3f;

        public EvadingDomination(
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
            if (_animalEntity.IsBeeingDominated)
                _animalEntity.DominationTryCooldown = _maxDominationTryCooldown;
            else
                _animalEntity.DominationTryCooldown -= deltaTime;

            Vector3 opositeDirection = GetDomineeringOpositeDirectionPosition();

            _botPathHandler.SetGoalPosition(opositeDirection, true);

            _botPathHandler.HandlePathIndexing();
            _botPathHandler.HandleBotRotation(deltaTime);
            _botPathHandler.HandleBotPositioning(deltaTime);
        }

        void IState.OnEnter()
        {
            // DebugExtension.DevLog("EvadingDomination #>".ToColor(GoodColors.Green));
            // float randomIdleDuration = UnityRandom.Range(_minIdleTime, _maxIdleTime);
            // _baseAnimalEntity.RemaingIdleTime = randomIdleDuration;
        }

        void IState.OnExit()
        {
            // DebugExtension.DevLog("EvadingDomination <#".ToColor(GoodColors.Orange));
        }

        Vector3 GetDomineeringOpositeDirectionPosition()
        {
            Vector3 value = default;

            Vector3 animalPosition = _animalEntity.transform.position;
            Vector3 domineeringPosition = _animalEntity.DomineeringEntity.transform.position;

            Vector3 opositeDirection = animalPosition - domineeringPosition;
            opositeDirection = opositeDirection.normalized * 10f;

            value = animalPosition + opositeDirection;

            return value;
        }
    }
}
