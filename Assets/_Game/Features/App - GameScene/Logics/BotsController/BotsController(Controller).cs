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
        List<AnimalData> TryGetAnimalsList()
        {
            List<AnimalData> value = new List<AnimalData>();

            if (AnimalsListGetter != null)
                value = AnimalsListGetter();

            return value;
        }

        void HandleBotsFixed()
        {
            List<AnimalData> animalsList = TryGetAnimalsList();

            foreach (AnimalData botInstance in animalsList)
                botInstance.StateMachine.Tick(Time.fixedDeltaTime);
        }
    }
}
