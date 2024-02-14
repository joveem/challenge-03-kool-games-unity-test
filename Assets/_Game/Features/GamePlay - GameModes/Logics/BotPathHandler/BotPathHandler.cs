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

        [Space(5), Header("[ Dependencies ]"), Space(10)]

        // public BotInstance BaseBotInstance;
        public AnimalEntity BaseAnimalEntity;
        PathNodesHandler _pathNodesHandler;


        [Space(5), Header("[ State ]"), Space(10)]

        bool _state;
        public Action OnBotReachedGoalPositionCallback = null;


        // [Space(5), Header("[ Parts ]"), Space(10)]

        // bool _parts;


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        public BotPathHandler() { }

        public BotPathHandler(
            AnimalEntity baseAnimalEntity,
            PathNodesHandler pathNodesHandler)
        {
            BaseAnimalEntity = baseAnimalEntity;
            _pathNodesHandler = pathNodesHandler;
        }
    }
}
