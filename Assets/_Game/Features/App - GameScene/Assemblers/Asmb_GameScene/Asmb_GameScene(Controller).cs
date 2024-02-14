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
using JovDK.Generic.SpatialUI;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace KoolGames.Test03.GamePlay
{
    public partial class Asmb_GameScene : MonoBehaviour
    {
        void SetInitialState()
        {
            RegisterSpatialUIItems();
        }

        void SetupDependencies()
        {
            SetupBotsController();
            SetupAnimalsMerging();
        }

        void SetupBotsController()
        {
            _botsController.AnimalsListGetter = _animalsMerging.GetCurrentAnimalsDatasList;
        }

        void SetupAnimalsMerging()
        {
            _animalsMerging.SetPlayerEntity(_playerEntity);
            _animalsMerging.SetMapData(_mapData);
            _animalsMerging.SetPathNodesHandler(_pathNodesHandler);
            _animalsMerging.SetMontaryLogic(_montaryLogic);
        }

        void RegisterSpatialUIItems()
        {
            _spatialUIHandler.DoIfNotNull(() =>
            {
                Vector3 positionDelta = new Vector3(0f, 2f, 5f);
                _spatialUIHandler.RegisterSpatialUIItems(_mergeStationTransform, _mergeStationUIBubbleTransform, positionDelta);
            });
        }
    }
}
