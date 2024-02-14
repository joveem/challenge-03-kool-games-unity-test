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


namespace KoolGames.Test03.GamePlay
{
    public partial class RopeView : MonoBehaviour
    {
        public void ShowRope()
        {
            _meshTransform.SetActiveIfNotNull(true);
        }

        public void HideRope()
        {
            _meshTransform.SetActiveIfNotNull(false);
        }
    }
}