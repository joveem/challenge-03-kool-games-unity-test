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
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
using KoolGames.Test03.GamePlay.Entities;


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
            randomPositionList = GetUniformDistribuitionOverArea(spawStartPositon, spawEndPositon, totalAmount);

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

                Instantiate(prefab, position, rotation);
            }
        }

        List<Vector3> GetUniformDistribuitionOverArea(
            Vector3 startPosition,
            Vector3 endPosition,
            int positionsAmount)
        {
            List<Vector3> positionsList = null;

            Vector3 areaSize = endPosition - startPosition;
            positionsList = GetUniformDistribuitionOverArea(areaSize, positionsAmount);

            for (int i = 0; i < positionsList.Count; i++)
            {
                Vector3 position = positionsList[i];
                position += startPosition;
                positionsList[i] = position;
            }

            return positionsList;
        }

        List<Vector3> GetUniformDistribuitionOverArea(
            Vector3 areaSize,
            int positionsAmount)
        {
            List<Vector3> positionsList = new List<Vector3>();

            float xValue = 1;
            float zValue = 1;
            float horizontalFactor = areaSize.x / areaSize.z;
            float verticalFactor = areaSize.z / areaSize.x;
            int currentPositionsAmount = (int)(xValue * zValue);

            while (positionsAmount > currentPositionsAmount)
            {
                float nextHorizontalFactor = xValue + 1 / zValue;
                float nextVerticalFactor = zValue + 1 / xValue;

                if (nextHorizontalFactor / horizontalFactor < nextVerticalFactor / verticalFactor)
                    xValue++;
                else
                    zValue++;

                currentPositionsAmount = (int)(xValue * zValue);
            }

            float horizontalGap = areaSize.x / (xValue - 1);
            float verticalGap = areaSize.z / (zValue - 1);

            DebugExtension.DevLog("areaSize = " + areaSize);
            DebugExtension.DevLog("xValue = " + xValue);
            DebugExtension.DevLog("zValue = " + zValue);
            DebugExtension.DevLog("horizontalGap = " + horizontalGap);
            DebugExtension.DevLog("verticalGap = " + verticalGap);

            for (float z = 0; z < zValue; z++)
            {
                for (float x = 0; x < xValue; x++)
                {
                    Vector3 position = new Vector3(x * horizontalGap, 0, z * verticalGap);
                    positionsList.Add(position);
                }
            }

            return positionsList;
        }
    }
}
