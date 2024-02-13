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
using KoolGames.Test03.GamePlay.Entities;
using UnityEngine.PlayerLoop;


namespace PackageName.MajorContext.MinorContext
{
    public partial class AnimalsMerging : MonoBehaviour
    {
        void SubscribeAllListeners()
        {
            _capturingTrigger.DoIfNotNull(() =>
            {
                _capturingTrigger.OnTriggerEnterCallback += OnEnterCapturingArea;
                _capturingTrigger.OnTriggerExitCallback += OnExitCapturingArea;
            });
        }

        void OnEnterCapturingArea(Collider collider)
        {
            if (collider.tag.Equals("animal"))
            {
                AnimalEntity animalEntity = collider.GetComponent<AnimalEntity>();
                TryToAddAnimalToCapturingArea(animalEntity);
            }
        }

        void OnExitCapturingArea(Collider collider)
        {
            if (collider.tag.Equals("animal"))
            {
                AnimalEntity animalEntity = collider.GetComponent<AnimalEntity>();
                TryToRemoveAnimalToCapturingArea(animalEntity);
            }
        }
    }
}
