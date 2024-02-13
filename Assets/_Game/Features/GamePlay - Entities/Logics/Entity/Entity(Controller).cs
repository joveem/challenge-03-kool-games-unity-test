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
using KoolGames.Test03.GamePlay.Entities.Views;


namespace KoolGames.Test03.GamePlay.Entities
{
    public partial class Entity : MonoBehaviour
    {
        public void AddEntityDomain(Entity dominatedEntity)
        {
            if (!_dominatedEntitiesList.Contains(dominatedEntity))
                _dominatedEntitiesList.Add(dominatedEntity);
        }

        public void RemoveEntityDomain(Entity dominatedEntity)
        {
            if (_dominatedEntitiesList.Contains(dominatedEntity))
                _dominatedEntitiesList.Remove(dominatedEntity);
        }

        public bool HasDomain(Entity dominatedEntity)
        {
            bool value = false;

            value = _dominatedEntitiesList.Contains(dominatedEntity);

            return value;
        }

        public List<Entity> GetDominatedEntitiesList()
        {
            return _dominatedEntitiesList;
        }
    }
}
