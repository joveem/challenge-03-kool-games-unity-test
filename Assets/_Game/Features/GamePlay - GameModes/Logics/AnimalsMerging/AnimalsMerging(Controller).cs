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
using System.Linq;


namespace KoolGames.Test03.GamePlay.GameModes
{
    public partial class AnimalsMerging : MonoBehaviour
    {
        void ApplyInitialState()
        {
            RegisterSpatialUIItems();
            InstantiateAnimals(_animalsPrefabsList, _initialAnimalsAmount);
        }

        void RegisterSpatialUIItems()
        {
            _spatialUIHandler.DoIfNotNull(() =>
            {
                Vector3 positionDelta = new Vector3(0f, 2f, 0f);
                _spatialUIHandler.RegisterSpatialUIItems(_playerEntity.transform, _maxCarryingWarning, positionDelta);
            });
        }

        public List<AnimalData> GetCurrentAnimalsDatasList()
        {
            List<AnimalData> value = new List<AnimalData>();

            value = _currentAnimalsList.Values.ToList();

            return value;
        }

        public List<AnimalEntity> GetCurrentAnimalsEntitiesList()
        {
            List<AnimalEntity> value = new List<AnimalEntity>();

            value = _currentAnimalsList.Keys.ToList();

            return value;
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
        [SerializeField] TriggerEmitter _mergeStationTrigger;
        [SerializeField] RectTransform _maxCarryingWarning;
        Dictionary<AnimalEntity, AnimalData> _inCapturingAreaAnimalsList = new Dictionary<AnimalEntity, AnimalData>();

        void HandleCapturingArea()
        {
            foreach (AnimalData animalData in _currentAnimalsList.Values)
                animalData.DoIfNotNull(() => animalData.AnimalEntity.IsBeeingDominated = false);

            int capturingAnimalsAmount = 0;

            foreach (AnimalData animalData in _inCapturingAreaAnimalsList.Values)
            {
                animalData.DoIfNotNull(
                    () =>
                    {
                        if (!animalData.AnimalEntity.IsDominated)
                        {
                            animalData.AnimalEntity.IsBeeingDominated = true;
                            animalData.AnimalEntity.DomineeringEntity = _playerEntity;
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

            int carryingAmount = GetCurrentCarryingAmount();
            bool hasReachedMaxCarryingAmount = carryingAmount >= 2;
            bool hasToShowMaxCarryingWarning = hasReachedMaxCarryingAmount && isCapturingAnimals;
            _maxCarryingWarning.SetActiveIfNotNull(hasToShowMaxCarryingWarning);
        }

        void HandleCapturing()
        {
            foreach (AnimalData animalData in _currentAnimalsList.Values)
            {
                int carryingAmount = GetCurrentCarryingAmount();
                if (carryingAmount >= 2)
                    return;

                animalData.DoIfNotNull(() =>
                {
                    Slider domainSlider = animalData.DomainSlider;
                    bool showDomainSlider = false;

                    if (!animalData.AnimalEntity.IsDominated)
                    {
                        // if (true) // TODO: REVIEW THIS!
                        if (animalData.AnimalEntity.IsBeeingDominated)
                        {
                            // TODO: REVIEW THIS!
                            animalData.AnimalEntity.CurrentDomainForce += Time.deltaTime;

                            if (animalData.AnimalEntity.CurrentDomainForce >= animalData.AnimalEntity.RequiredDomainForce)
                                DoAnimalCapturing(_playerEntity, animalData);
                            else
                            {
                                showDomainSlider = true;

                                float domainFactor = animalData.AnimalEntity.CurrentDomainForce / animalData.AnimalEntity.RequiredDomainForce;
                                domainSlider.DoIfNotNull(() => domainSlider.value = domainFactor);
                            }
                        }
                        else
                            animalData.AnimalEntity.CurrentDomainForce = 0f;
                    }

                    domainSlider.SetActiveIfNotNull(showDomainSlider);
                });
            }
        }

        [SerializeField] List<Transform> _mergePositionsTransformsList = new List<Transform>();


        void DoAnimalCapturing(
            PlayerEntity playerEntity,
            AnimalData animalData)
        {
            int initialCarryingAmount = GetCurrentCarryingAmount();

            animalData.AnimalEntity.IsDominated = true;
            animalData.AnimalEntity.OwnerEntity = playerEntity;
            animalData.AnimalEntity.IsBeeingDominated = false;
            animalData.AnimalEntity.CurrentDomainForce = 0f;

            if (initialCarryingAmount == 0)
            {
                animalData.AnimalEntity.IsMounted = true;
                _montaryLogic.DoAnimalMount(playerEntity, animalData.AnimalEntity);
            }
            else
                DoAnimalCarrying(playerEntity, animalData);
        }

        void DoAnimalCarrying(
            PlayerEntity playerEntity,
            AnimalData animalData)
        {
            PlayerView playerView = (PlayerView)playerEntity.EntityView;
            RopeView ropeView = playerView.RopeView;

            ropeView.ShowRope();
            ropeView.SetEndPositionTransform(animalData.AnimalEntity.transform);
            ropeView.SetEndPositionDelta(new Vector3(0f, 1f, 0f));
        }

        void TryToDoAnimalDelivery()
        {
            int carryingAmount = GetCurrentCarryingAmount();

            bool hasEnoughAnimals = carryingAmount >= 2;

            if (hasEnoughAnimals)
            {
                _montaryLogic.DoAnimalDismount(_playerEntity);
                List<AnimalData> carryingAnimalsList = GetCurrentCarryingAnimalsList();

                PlayMergeAnimation(carryingAnimalsList);
            }
        }

        [SerializeField] float _mergeTranslateDuration = 0.5f;
        [SerializeField] float _mergeAnimationDuration = 5f;

        async void PlayMergeAnimation(List<AnimalData> animalsList)
        {
            if (animalsList.Count < 2)
            {
                string debugText =
                    "$$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to PlayMergeAnimation" + "\n" +
                    "NOT ENOUGHT animals on list!" + "\n" +
                    "animalsList.Count =" + animalsList.Count + "\n" +
                    "";
                DebugExtension.DevLogWarning(debugText);
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                AnimalData animalData = animalsList[i];
                Transform positionPivot = _mergePositionsTransformsList[i];

                float waitSeconds = _mergeTranslateDuration + _mergeAnimationDuration;
                await Task.Delay((int)(waitSeconds * 1000));
                TryToDestroyAnimal(animalData.AnimalEntity);
            }
        }

        List<AnimalData> GetCurrentCarryingAnimalsList()
        {
            List<AnimalData> value = new List<AnimalData>();


            return value;
        }
    }
}
