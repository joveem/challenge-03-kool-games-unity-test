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
using JovDK.Generic.StateMachine;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Bots;
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.Scenario;


namespace KoolGames.Test03.GamePlay.GameModes
{
    public partial class AnimalsMerging : MonoBehaviour
    {
        StateMachine GetDefaultBotStateMachine(
            AnimalEntity animalEntity,
            PathNodesHandler pathNodesHandler,
            MapData mapData)
        {
            StateMachine value = new StateMachine();

            // dependecies
            BotPathHandler botPathHandler = new BotPathHandler(animalEntity, pathNodesHandler);

            // states
            Idle idle = new Idle(animalEntity);
            Dominated dominated = new Dominated(animalEntity, botPathHandler, mapData);
            IdleMovement idleMovement = new IdleMovement(animalEntity, botPathHandler, mapData);
            EvadingDomination evadingDomination = new EvadingDomination(animalEntity, botPathHandler, mapData);
            Static staticState = new Static();

            // transitions
            value.AddTransition(idle, idleMovement, HasIdleFinished());
            value.AddTransition(idleMovement, idle, HasReachedPathGoal());
            value.AddTransition(evadingDomination, idle, IsNotBeeingDominated());

            // any transitions
            value.AddAnyTransition(evadingDomination, IsBeeingDominated());
            value.AddAnyTransition(dominated, IsDominated());
            value.AddAnyTransition(dominated, IsDominated());
            value.AddAnyTransition(staticState, IsStatic());

            value.SetState(idle);

            return value;

            Func<bool> IsDominated() => () => !animalEntity.IsStatic && animalEntity.IsDominated;
            Func<bool> HasIdleFinished() => () => !animalEntity.IsStatic && animalEntity.RemaingIdleTime <= 0;
            Func<bool> IsBeeingDominated() => () => !animalEntity.IsStatic && !animalEntity.IsDominated && (animalEntity.IsBeeingDominated || animalEntity.DominationTryCooldown > 0f);
            Func<bool> IsNotBeeingDominated() => () => !animalEntity.IsStatic && !animalEntity.IsBeeingDominated && animalEntity.DominationTryCooldown <= 0f;
            Func<bool> HasReachedPathGoal() => () => !animalEntity.IsStatic && botPathHandler.HasReachedPathGoal();
            Func<bool> IsStatic() => () => animalEntity.IsStatic;
        }
    }
}
