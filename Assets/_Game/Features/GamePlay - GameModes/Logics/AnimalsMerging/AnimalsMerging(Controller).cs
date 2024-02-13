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
using JovDK.Math;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Entities;
using JovDK.Physics.Triggers;
using KoolGames.Test03.GamePlay.Entities.Views;


namespace PackageName.MajorContext.MinorContext
{
    public partial class AnimalsMerging : MonoBehaviour
    {
        void ApplyInitialState()
        {
            InstantiateAnimals(_animalsPrefabsList, _initialAnimalsAmount);
        }

        void InstantiateAnimals(
            List<AnimalEntity> animalsPrefabsList,
            int totalAmount)
        {
            Vector3 randomDeltaPositionAreaSize = new Vector3(20f, 0f, 20f);
            List<int> availableIndexes = new List<int>();

            for (int i = 0; i < totalAmount; i++)
                availableIndexes.Add(i);

            List<AnimalEntity> randomPrefabList = new List<AnimalEntity>(new AnimalEntity[totalAmount]);
            List<Vector3> randomPositionList = new List<Vector3>(new Vector3[totalAmount]);
            List<Quaternion> randomRotationList = new List<Quaternion>(new Quaternion[totalAmount]);

            // setting animals order (by prefab references order)
            while (availableIndexes.Count > 0)
            {
                for (int j = 0; j < animalsPrefabsList.Count; j++)
                {
                    AnimalEntity animalEntityPrefab = animalsPrefabsList[j];

                    if (availableIndexes.Count > 0)
                    {
                        int randomIndexIndex = UnityRandom.Range(0, availableIndexes.Count);
                        int randomIndex = availableIndexes[randomIndexIndex];
                        availableIndexes.RemoveAt(randomIndexIndex);

                        randomPrefabList[randomIndex] = animalEntityPrefab;
                    }
                }
            }

            // setting random positions
            Vector3 spawStartPositon = _mapData.GetAnimalsSpawnStart();
            Vector3 spawEndPositon = _mapData.GetAnimalsSpawnEnd();
            randomPositionList = MathSpatial.UniformDistribuitionOverArea(
                                    spawStartPositon,
                                    spawEndPositon,
                                    totalAmount);

            for (int i = 0; i < randomPositionList.Count; i++)
            {
                Vector3 position = randomPositionList[i];
                float randomDeltaX = UnityRandom.Range(0f, randomDeltaPositionAreaSize.x);
                float randomDeltaY = UnityRandom.Range(0f, randomDeltaPositionAreaSize.y);
                float randomDeltaZ = UnityRandom.Range(0f, randomDeltaPositionAreaSize.z);
                randomDeltaX -= randomDeltaPositionAreaSize.x / 2;
                randomDeltaY -= randomDeltaPositionAreaSize.y / 2;
                randomDeltaZ -= randomDeltaPositionAreaSize.z / 2;

                Vector3 randomDelta = new Vector3(randomDeltaX, randomDeltaY, randomDeltaZ);
                position += randomDelta;

                randomPositionList[i] = position;
            }

            // setting random rotations
            for (int i = 0; i < randomRotationList.Count; i++)
            {
                float randomYRotation = UnityRandom.Range(0f, 360);
                Quaternion randomRotation = Quaternion.Euler(0, randomYRotation, 0f);

                randomRotationList[i] = randomRotation;
            }

            // instatiate final objects
            for (int i = 0; i < totalAmount; i++)
            {
                AnimalEntity prefab = randomPrefabList[i];
                Vector3 position = randomPositionList[i];
                Quaternion rotation = randomRotationList[i];

                IntantiateAnimal(prefab, position, rotation);
            }
        }

        [SerializeField] MeshRenderer _capturingAreaMesh;
        [SerializeField] TriggerEmitter _capturingTrigger;
        Dictionary<AnimalEntity, AnimalData> _inCapturingAreaAnimalsList = new Dictionary<AnimalEntity, AnimalData>();

        void HandleCapturingArea()
        {
            foreach (AnimalData animalData in _currentAnimalsList.Values)
                animalData.DoIfNotNull(() => animalData.IsBeeingDominated = false);

            int capturingAnimalsAmount = 0;

            foreach (AnimalData animalData in _inCapturingAreaAnimalsList.Values)
            {
                animalData.DoIfNotNull(
                    () =>
                    {
                        if (!animalData.IsDominated)
                        {
                            animalData.IsBeeingDominated = true;
                            capturingAnimalsAmount++;
                        }
                    });
            }

            bool isCapturingAnimals = capturingAnimalsAmount > 0;
            _capturingAreaMesh.enabled = isCapturingAnimals;
            PlayerView playerView = (PlayerView)_playerEntity.EntityView;

            if (isCapturingAnimals)
                playerView.PlayCatchAnimation();
            else
                playerView.StopCatchAnimation();
        }

        void HandleCapturing()
        {
            foreach (AnimalData animalData in _currentAnimalsList.Values)
            {
                animalData.DoIfNotNull(() =>
                {
                    Slider domainSlider = animalData.DomainSlider;
                    bool showDomainSlider = false;

                    if (!animalData.IsDominated)
                    {
                        // if (true) // TODO: REVIEW THIS!
                        if (animalData.IsBeeingDominated)
                        {
                            // TODO: REVIEW THIS!
                            animalData.CurrentDomainForce += Time.deltaTime;

                            if (animalData.CurrentDomainForce >= animalData.RequiredDomainForce)
                                DoPlayerCapturing(_playerEntity, animalData);
                            else
                            {
                                showDomainSlider = true;

                                float domainFactor = animalData.CurrentDomainForce / animalData.RequiredDomainForce;
                                domainSlider.DoIfNotNull(() => domainSlider.value = domainFactor);
                            }
                        }
                        else
                            animalData.CurrentDomainForce = 0f;
                    }

                    domainSlider.SetActiveIfNotNull(showDomainSlider);
                });
            }
        }

        void DoPlayerCapturing(
            PlayerEntity playerEntity,
            AnimalData animalData)
        {
            animalData.IsDominated = true;
            animalData.IsBeeingDominated = false;
            animalData.CurrentDomainForce = 0f;
        }
    }
}
