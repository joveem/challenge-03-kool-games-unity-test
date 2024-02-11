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


namespace KoolGames.Test03.GamePlay.Entities
{
    public partial class MovableView : MonoBehaviour, IMovableView
    {
        public void ApplyZVelocity(float zVelocity)
        {
            _animator.DoIfNotNull(() => _animator.SetFloat("z-velocity", zVelocity));
        }
    }
}
