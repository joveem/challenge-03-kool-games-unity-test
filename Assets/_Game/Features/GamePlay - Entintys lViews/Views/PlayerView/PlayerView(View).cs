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


namespace PackageName.MajorContext.MinorContext
{
    public partial class PlayerView : MovableView
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
            _animator.SetTrigger("start-catch");
        }

        public void StopCatchAnimation()
        {
            HideRope();
            _animator.SetTrigger("end-catch");
        }

        public void PlayMountAnimation()
        {
            _animator.SetTrigger("start-mount");
        }

        public void StopMountAnimation()
        {
            _animator.SetTrigger("end-mount");
        }
    }
}
