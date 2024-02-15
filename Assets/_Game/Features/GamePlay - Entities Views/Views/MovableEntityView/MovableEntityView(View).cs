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
        public void ApplyZVelocity(float zVelocity)
        {
            _animator.DoIfNotNull(() => _animator.SetFloat("z-velocity", zVelocity));

            bool hasToEmitParticles = zVelocity >= _footStepsParticleMinVelocity;
            _footStepsParticle.DoIfNotNull(() => _footStepsParticle.enableEmission = hasToEmitParticles);
        }
    }
}
