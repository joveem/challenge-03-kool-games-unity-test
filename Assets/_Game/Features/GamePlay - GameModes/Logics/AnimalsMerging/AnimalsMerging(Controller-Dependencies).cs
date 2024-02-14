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
using JovDK.Generic.SpatialUI;
using JovDK.Math;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.Montary;
using KoolGames.Test03.GamePlay.Scenario;


namespace KoolGames.Test03.GamePlay.GameModes
{
    public partial class AnimalsMerging : MonoBehaviour
    {
        public void SetPlayerEntity(PlayerEntity playerEntity)
        {
            _playerEntity = playerEntity;
        }

        public void SetMapData(MapData mapData)
        {
            _mapData = mapData;
        }

        public void SetPathNodesHandler(PathNodesHandler pathNodesHandler)
        {
            _pathNodesHandler = pathNodesHandler;
        }

        public void SetMontaryLogic(MontaryLogic montaryLogic)
        {
            _montaryLogic = montaryLogic;
        }
    }
}
