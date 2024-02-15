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
    public partial class MovableEntityView : EntityView, IMovableView
    {
        public void SetIsGrounded(bool isGrounded)
        {
            _isGrounded = isGrounded;
            RefreshInGroundedAnimationState();
            RefreshFootStepParticleState();
        }

        void RefreshInGroundedAnimationState()
        {
            _animator.SetBool("is-grounded", _isGrounded);
        }

        public bool GetIsGrounded()
        {
            return _isGrounded;
        }

        protected void RefreshFootStepParticleState()
        {
            bool hasToEnableFootStepsParticle = HasToEnableFootStepsParticle();
            _footStepsParticle.SetActiveIfNotNull(hasToEnableFootStepsParticle);
        }

        protected virtual bool HasToEnableFootStepsParticle()
        {
            bool value = false;

            value = _isGrounded;

            return value;
        }
    }
}
