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
using KoolGames.Test03.GamePlay.Entities;


namespace KoolGames.Test03.GamePlay.GameModes
{
    public class AnimalData
    {
        public AnimalEntity AnimalEntity;
        public Slider DomainSlider;

        public StateMachine StateMachine;

        public AnimalData() { }

        public AnimalData(
            AnimalEntity animalEntity,
            Slider domainSlider)
        {
            AnimalEntity = animalEntity;
            DomainSlider = domainSlider;
        }
    }
}
