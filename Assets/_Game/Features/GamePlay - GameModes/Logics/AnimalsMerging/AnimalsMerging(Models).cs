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
    public class AnimalData
    {
        public bool IsDominated = false;
        public bool IsBeeingDominated = false;
        public float CurrentDomainForce = 0f;
        public float RequiredDomainForce = 5f;
        public AnimalEntity AnimalEntity;
        public Slider DomainSlider;

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
