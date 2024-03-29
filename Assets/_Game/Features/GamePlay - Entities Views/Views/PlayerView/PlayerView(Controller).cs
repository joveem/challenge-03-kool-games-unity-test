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


namespace KoolGames.Test03.GamePlay.Entities.Views
{
    public partial class PlayerView : MovableEntityView
    {
        void ApplyInitialState()
        {
            HideRope();
        }

        protected override bool HasToEnableFootStepsParticle()
        {
            bool value = false;

            value = _isGrounded && !_isMounted;

            return value;
        }
    }
}
