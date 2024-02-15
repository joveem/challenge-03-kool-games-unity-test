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
        void ShowRope()
        {
            foreach (GameObject ropePart in _ropePartsList)
                ropePart.SetActiveIfNotNull(true);
        }

        void HideRope()
        {
            foreach (GameObject ropePart in _ropePartsList)
                ropePart.SetActiveIfNotNull(false);
        }

        public void PlayCatchAnimation()
        {
            ShowRope();
            _animator.SetBool("is-catching", true);
        }

        public void StopCatchAnimation()
        {
            HideRope();
            _animator.SetBool("is-catching", false);
        }

        public void PlayMountAnimation()
        {
            _isMounted = true;
            RefreshMountingAnimationState();
        }

        public void StopMountAnimation()
        {
            _isMounted = false;
            RefreshMountingAnimationState();
        }

        void RefreshMountingAnimationState()
        {
            _animator.SetBool("is-mounting", _isMounted);
        }
    }
}
