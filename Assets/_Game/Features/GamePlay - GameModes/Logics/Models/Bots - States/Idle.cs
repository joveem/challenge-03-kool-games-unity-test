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
using KoolGames.Test03.GamePlay.Entities;


namespace KoolGames.Test03.GamePlay.GameModes
{
    public class Idle : IState
    {
        float _minIdleTime = 3f;
        float _maxIdleTime = 8f;

        AnimalEntity _baseAnimalEntity;

        public Idle(AnimalEntity baseAnimalEntity)
        {
            _baseAnimalEntity = baseAnimalEntity;
        }

        void IState.Tick(float deltaTime)
        {
            _baseAnimalEntity.RemaingIdleTime -= deltaTime;
            _baseAnimalEntity.RemaingIdleTime = Mathf.Clamp(_baseAnimalEntity.RemaingIdleTime, 0f, Mathf.Infinity);
        }

        void IState.OnEnter()
        {
            // DebugExtension.DevLog("Idle #>".ToColor(GoodColors.Green));
            float randomIdleDuration = UnityRandom.Range(_minIdleTime, _maxIdleTime);
            _baseAnimalEntity.RemaingIdleTime = randomIdleDuration;
        }

        void IState.OnExit()
        {
            // DebugExtension.DevLog("Idle <#".ToColor(GoodColors.Orange));
        }
    }
}
