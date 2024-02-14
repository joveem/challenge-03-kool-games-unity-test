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
// ...


namespace KoolGames.Test03.GamePlay.Scenario
{
    public partial class MapData : MonoBehaviour
    {

        public Vector3 GetAnimalsSpawnStart()
        {
            Vector3 value = default;

            _animalsSpawnsStart.DoIfNotNull(() => value = _animalsSpawnsStart.position);

            return value;
        }

        public Vector3 GetAnimalsSpawnEnd()
        {
            Vector3 value = default;

            _animalsSpawnsStart.DoIfNotNull(() => value = _animalsSpawnsEnd.position);

            return value;
        }
    }
}
