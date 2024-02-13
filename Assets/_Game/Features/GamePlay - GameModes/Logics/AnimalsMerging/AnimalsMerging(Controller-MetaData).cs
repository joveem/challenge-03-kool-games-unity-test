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


namespace PackageName.MajorContext.MinorContext
{
    public partial class AnimalsMerging : MonoBehaviour
    {
        [SerializeField] RectTransform _spatialUIContainer;
        [SerializeField] Slider _domainSliderPrefab;
        [SerializeField] SpatialUIHandler _spatialUIHandler;

        public void SetSpatialUIHandler(SpatialUIHandler spatialUIHandler)
        {
            _spatialUIHandler = spatialUIHandler;
        }

        void IntantiateAnimal(
            AnimalEntity animalEntityPrefab,
            Vector3 position,
            Quaternion rotation)
        {
            AnimalEntity entityInstance = Instantiate(animalEntityPrefab, position, rotation);
            Slider domainSlider = Instantiate(_domainSliderPrefab, _spatialUIContainer);

            _spatialUIHandler.DoIfNotNull(() =>
                {
                    _spatialUIHandler.RegisterSpatialUIItems(
                        entityInstance.transform,
                        domainSlider.transform as RectTransform);
                });

            AnimalData animalData = new AnimalData(entityInstance, domainSlider);

            _currentAnimalsList.Add(entityInstance, animalData);
        }

        void TryToAddAnimalToCapturingArea(AnimalEntity animalEntity)
        {
            if (animalEntity == null)
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to TryToAddAnimalToCapturingArea" + "\n" +
                    "animalEntity IS NULL!" + "\n" +
                    "";
                DebugExtension.DevLogWarning(debugText);
                return;
            }

            if (!_inCapturingAreaAnimalsList.ContainsKey(animalEntity))
            {
                bool isAnimalRegistered =
                    _currentAnimalsList.TryGetValue(
                        animalEntity,
                        out AnimalData animalData);

                if (isAnimalRegistered)
                    _inCapturingAreaAnimalsList.Add(animalEntity, animalData);
                else
                {
                    string debugText =
                        "$ > ".ToColor(GoodColors.Red) +
                        "ERROR trying to TryToAddAnimalToCapturingArea" + "\n" +
                        "animalEntity IS NOT REGISTERED!" + "\n" +
                        "";
                    DebugExtension.DevLogWarning(debugText);
                }
            }
        }

        void TryToRemoveAnimalToCapturingArea(AnimalEntity animalEntity)
        {
            if (animalEntity == null)
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to TryToRemoveAnimalToCapturingArea" + "\n" +
                    "animalEntity IS NULL!" + "\n" +
                    "";
                DebugExtension.DevLogWarning(debugText);
                return;
            }

            if (_inCapturingAreaAnimalsList.ContainsKey(animalEntity))
                _inCapturingAreaAnimalsList.Remove(animalEntity);
            else
            {
                string debugText =
                    "$ > ".ToColor(GoodColors.Red) +
                    "ERROR trying to TryToRemoveAnimalToCapturingArea" + "\n" +
                    "animalEntity is NOT REGISTERED!" + "\n" +
                    "";
                DebugExtension.DevLogWarning(debugText);
            }
        }
    }
}
