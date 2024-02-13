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
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using JovDK.Physics.Triggers;

// from project
using KoolGames.Test03.GamePlay.Entities.Views;


namespace KoolGames.Test03.GamePlay.PlayerController.Testing.Showcase
{
    public partial class Asmb_PlayerTouchControllerShowcaseScene : MonoBehaviour
    {
        void HandleDEBUGInputs()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DebugExtension.DevLog("[1]".ToColor(GoodColors.Blue) + " > PlayCatchAnimation (Beetle)");
                PlayBeetleCatchAnimation();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                DebugExtension.DevLog("[2]".ToColor(GoodColors.Blue) + " > PlayBeetlephantCatchAnimation (Beetlephant)");
                PlayBeetlephantCatchAnimation();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                DebugExtension.DevLog("[3]".ToColor(GoodColors.Blue) + " > PlayCatchAnimation (Elephant)");
                PlayElephantCatchAnimation();
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                DebugExtension.DevLog("[Q]".ToColor(GoodColors.Blue) + " > DismountFromCurrentAnimal");
                DismountFromCurrentAnimal();
            }
        }

        void PlayBeetleCatchAnimation()
        {
            DoAnimalMount(_playerEntity, _beetleEntity);
        }

        void PlayBeetlephantCatchAnimation()
        {
            DoAnimalMount(_playerEntity, _beetlephantEntity);
        }

        void PlayElephantCatchAnimation()
        {
            DoAnimalMount(_playerEntity, _elephantEntity);
        }

        void DismountFromCurrentAnimal()
        {
            DoAnimalDismount(_playerEntity);
        }
    }
}
