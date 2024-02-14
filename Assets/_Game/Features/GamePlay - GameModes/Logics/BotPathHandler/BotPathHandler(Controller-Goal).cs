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

// from project
using KoolGames.Test03.GamePlay.Entities;


namespace KoolGames.Test03.GamePlay.Bots
{
    public partial class BotPathHandler
    {
        public bool HasReachedPathGoal()
        {
            bool value = false;

            value = BaseAnimalEntity.CurrentPathIndex == -1;

            return value;
        }

        void SetBotGoal(
            Vector3 goalPosition,
            List<GraphPathNode> goalPathNodesList)
        {
            BaseAnimalEntity.CurrentPathIndex = 0;
            BaseAnimalEntity.CurrentGoalPosition = goalPosition;
            BaseAnimalEntity.CurrentPathNodesList = goalPathNodesList;
        }

        public void SetGoalPosition(Vector3 goalPosition, bool debugPath = false)
        {
            Vector3 startPosition = BaseAnimalEntity.transform.position;
            List<GraphPathNode> graphPathNodes = _pathNodesHandler.GetShortestPath(startPosition, goalPosition);
            SetBotGoal(goalPosition, graphPathNodes);


            // ! DEBUG ONLY!!!
            if (!debugPath)
                return;

            List<Vector3> pathPosition = new List<Vector3>();

            foreach (var item in graphPathNodes)
            {
                if (item != null)
                    pathPosition.Add(item.transform.position);
            }
            pathPosition.Add(goalPosition);

            float debugLineDuration = 3f;
            // float debugLineDuration = _DEBUG_debugDuration;

            DebugExtension.DebugPath(pathPosition, debugLineDuration, new Color(1f, 0f, 0f));
            DebugExtension.DebugPosition(goalPosition, new Color(1f, 0f, 0f), debugLineDuration);
        }

        public void SetDirectGoalPosition(Vector3 goalPosition, bool debugPath = false)
        {
            Vector3 startPosition = BaseAnimalEntity.transform.position;
            List<GraphPathNode> graphPathNodes = new List<GraphPathNode>();
            SetBotGoal(goalPosition, graphPathNodes);


            // ! DEBUG ONLY!!!
            if (!debugPath)
                return;

            List<Vector3> pathPosition = new List<Vector3>();

            foreach (var item in graphPathNodes)
            {
                if (item != null)
                    pathPosition.Add(item.transform.position);
            }
            pathPosition.Add(goalPosition);

            float debugLineDuration = 3f;
            // float debugLineDuration = _DEBUG_debugDuration;

            DebugExtension.DebugPath(pathPosition, debugLineDuration, new Color(1f, 0f, 0f));
            DebugExtension.DebugPosition(goalPosition, new Color(1f, 0f, 0f), debugLineDuration);
        }
    }
}
