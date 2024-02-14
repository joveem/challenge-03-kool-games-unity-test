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
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Entities;


namespace KoolGames.Test03.GamePlay.Bots
{
    public partial class BotPathHandler
    {
        void OnBotReachedGoalPosition()
        {
            if (BaseAnimalEntity == null)
                return;

            BaseAnimalEntity.CurrentPathIndex = -1;
            BaseAnimalEntity.CurrentPathNodesList = new List<GraphPathNode>();
            OnBotReachedGoalPositionCallback?.Invoke();
        }
    }
}
