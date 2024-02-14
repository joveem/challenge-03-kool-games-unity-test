// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
using KoolGames.Test03.GamePlay.Entities;
using KoolGames.Test03.GamePlay.GameModes;


namespace KoolGames.Test03.GamePlay
{
    public partial class BotsController : MonoBehaviour
    {

        // [Space(5), Header("[ Dependencies ]"), Space(10)]

        // bool _dependencies;


        [Space(5), Header("[ State ]"), Space(10)]

        public AnimalsListGetter AnimalsListGetter;


        [Space(5), Header("[ Parts ]"), Space(10)]

        Dictionary<AnimalEntity, AnimalData> _currentAnimalsList = new Dictionary<AnimalEntity, AnimalData>();


        // [Space(5), Header("[ Configs ]"), Space(10)]

        // bool _configs;


        // void Awake()
        // {

        // }

        // void Start()
        // {

        // }

        // void Update()
        // {

        // }

        void FixedUpdate()
        {
            HandleBotsFixed();
        }
    }
}
