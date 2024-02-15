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
    public class Static : IState
    {
        public Static() { }

        void IState.Tick(float deltaTime) { }

        void IState.OnEnter() { }

        void IState.OnExit() { }
    }
}
