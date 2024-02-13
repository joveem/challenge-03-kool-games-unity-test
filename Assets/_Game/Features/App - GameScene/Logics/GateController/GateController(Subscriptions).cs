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
// ...


namespace KoolGames.Test03.GamePlay.GameScene
{
    public partial class GateController : MonoBehaviour
    {
        void SubscribeAllListeners()
        {
            _areaTrigger.DoIfNotNull(() =>
            {
                _areaTrigger.OnTriggerEnterCallback += OnAreaEnter;
                _areaTrigger.OnTriggerExitCallback += OnAreaExit;
            });
        }

        void OnAreaEnter(Collider collider)
        {
            if (collider.tag.Equals("player"))
                PlayOpenAnimation();
        }

        void OnAreaExit(Collider collider)
        {
            if (collider.tag.Equals("player"))
                PlayCloseAnimation();
        }
    }
}
