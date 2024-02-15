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
        public void HandlePathIndexing()
        {
            if (BaseAnimalEntity == null)
                return;

            float goalMaxDistance = BaseAnimalEntity.GoalMaxDistance;

            if (BaseAnimalEntity.CurrentPathNodesList != null && BaseAnimalEntity.CurrentPathIndex >= 0)
            {
                int pathIndex = BaseAnimalEntity.CurrentPathIndex;

                if (pathIndex < BaseAnimalEntity.CurrentPathNodesList.Count)
                {
                    // bot following path node
                    GraphPathNode currentPathNode = BaseAnimalEntity.CurrentPathNodesList[pathIndex];
                    float nodeDistance = Vector3.Distance(
                        BaseAnimalEntity.transform.position,
                        currentPathNode.transform.position);

                    if (nodeDistance <= currentPathNode.AreaSize)
                        BaseAnimalEntity.CurrentPathIndex++;
                }
                else
                {
                    // bot following goal end position
                    float nodeDistance = Vector3.Distance(
                        BaseAnimalEntity.transform.position,
                        BaseAnimalEntity.CurrentGoalPosition);

                    if (nodeDistance <= goalMaxDistance)
                        OnBotReachedGoalPosition();
                }
            }
        }

        public void HandleBotRotation(float deltaTime, float maxVelocityMultiplier = 1f)
        {
            if (BaseAnimalEntity == null)
                return;

            float currentXInput = 0f;

            if (BaseAnimalEntity.CurrentPathNodesList != null && BaseAnimalEntity.CurrentPathIndex >= 0)
            {
                bool hasReachedPathGoal = HasReachedPathGoal();
                if (hasReachedPathGoal)
                    return;

                int pathIndex = BaseAnimalEntity.CurrentPathIndex;
                Vector3 goalDirection;

                if (pathIndex < BaseAnimalEntity.CurrentPathNodesList.Count)
                {
                    // bot following path node
                    GraphPathNode currentPathNode = BaseAnimalEntity.CurrentPathNodesList[pathIndex];
                    goalDirection = currentPathNode.transform.position - BaseAnimalEntity.transform.position;
                    // DebugExtension.DebugPosition(currentPathNode.transform.position, Color.blue);
                }
                else
                {
                    // bot following goal end position
                    goalDirection = BaseAnimalEntity.CurrentGoalPosition - BaseAnimalEntity.transform.position;
                    // DebugExtension.DebugPosition(botState.CurrentGoalPosition, Color.magenta);
                }

                Debug.DrawLine(BaseAnimalEntity.transform.position, BaseAnimalEntity.transform.position + goalDirection, Color.magenta);

                float currentYRotation = BaseAnimalEntity.transform.rotation.eulerAngles.y;
                float destinationYRotation = Quaternion.LookRotation(goalDirection, Vector3.up).eulerAngles.y;

                while (currentYRotation > 180)
                    currentYRotation -= 360f;
                while (destinationYRotation > 180)
                    destinationYRotation -= 360f;

                float deltaYRotation = destinationYRotation - currentYRotation;

                while (deltaYRotation > 180)
                    deltaYRotation -= 360f;

                while (deltaYRotation <= -180)
                    deltaYRotation += 360f;

                if (Mathf.Abs(deltaYRotation) < 2f)
                    currentXInput = 0f;
                else if (Mathf.Abs(deltaYRotation) < 15f)
                    currentXInput = 0.1f;
                else if (Mathf.Abs(deltaYRotation) < 30f)
                    currentXInput = 0.2f;
                else if (Mathf.Abs(deltaYRotation) < 45f)
                    currentXInput = 0.3f;
                else if (Mathf.Abs(deltaYRotation) < 90f)
                    currentXInput = 0.7f;
                else
                    currentXInput = 1f;

                if (deltaYRotation < 0f)
                    currentXInput *= -1f;

                // if (BaseAnimalEntity.gameObject.activeInHierarchy)
                // {
                //     DebugExtension.DevLog(
                //         "current-y-rotation = " + currentYRotation.ToString("0.00") + " | " +
                //         "goal-y-rotation = " + destinationYRotation.ToString("0.00") + " | " +
                //         "delta-y-rotation = " + deltaYRotation.ToString("0.00") + " | " +
                //         "x-input = " + currentXInput.ToString("0.00") + " | " +
                //         "y-rotation-vel = " + BaseAnimalEntity.CurrentYRotationVelocityFactor.ToString("0.00") + " | " +
                //         "");
                // }
            }

            currentXInput *= maxVelocityMultiplier;
            Rigidbody rigidbody = BaseAnimalEntity.Rigidbody;

            if (BaseAnimalEntity.CurrentYRotationVelocityFactor != currentXInput)
            {
                if (BaseAnimalEntity.CurrentYRotationVelocityFactor < currentXInput)
                {
                    BaseAnimalEntity.CurrentYRotationVelocityFactor += deltaTime * BaseAnimalEntity.YRotationAccelerationFactor;
                    BaseAnimalEntity.CurrentYRotationVelocityFactor = Mathf.Clamp(BaseAnimalEntity.CurrentYRotationVelocityFactor, -maxVelocityMultiplier, currentXInput);
                }
                else
                {
                    BaseAnimalEntity.CurrentYRotationVelocityFactor -= deltaTime * BaseAnimalEntity.YRotationAccelerationFactor;
                    BaseAnimalEntity.CurrentYRotationVelocityFactor = Mathf.Clamp(BaseAnimalEntity.CurrentYRotationVelocityFactor, currentXInput, maxVelocityMultiplier);
                }
            }

            float currentYRotationVelocizy = BaseAnimalEntity.CurrentYRotationVelocityFactor * BaseAnimalEntity.MaxYRotationVelocity * deltaTime;
            Quaternion additionalRotation = Quaternion.AngleAxis(currentYRotationVelocizy, Vector3.up);
            Quaternion targetRotation = rigidbody.rotation * additionalRotation;
            rigidbody.MoveRotation(targetRotation);
        }

        public void HandleBotPositioning(float deltaTime, float maxVelocityMultiplier = 1f)
        {
            if (BaseAnimalEntity == null)
                return;

            float currentYInput = 0f;

            float goalMaxDistance = BaseAnimalEntity.GoalMaxDistance;

            if (BaseAnimalEntity.CurrentPathNodesList != null && BaseAnimalEntity.CurrentPathIndex >= 0)
            {
                bool hasReachedPathGoal = HasReachedPathGoal();
                if (hasReachedPathGoal)
                    return;

                int pathIndex = BaseAnimalEntity.CurrentPathIndex;
                if (pathIndex < BaseAnimalEntity.CurrentPathNodesList.Count)
                {
                    // bot following path node
                    currentYInput = 1f;
                }
                else
                {
                    float nodeDistance = Vector3.Distance(
                        BaseAnimalEntity.transform.position,
                        BaseAnimalEntity.CurrentGoalPosition);

                    // bot following goal end position
                    if (nodeDistance > goalMaxDistance * 2)
                        currentYInput = 1f;
                    else
                        currentYInput = 0.5f;
                }
            }

            currentYInput *= maxVelocityMultiplier;
            Rigidbody rigidbody = BaseAnimalEntity.Rigidbody;

            if (BaseAnimalEntity.CurrentZMoveVelocityFactor != currentYInput)
            {
                if (BaseAnimalEntity.CurrentZMoveVelocityFactor < currentYInput)
                {
                    BaseAnimalEntity.CurrentZMoveVelocityFactor += deltaTime * BaseAnimalEntity.ZMoveAccelerationFactor;
                    BaseAnimalEntity.CurrentZMoveVelocityFactor = Mathf.Clamp(BaseAnimalEntity.CurrentZMoveVelocityFactor, -1f, currentYInput);
                }
                else
                {
                    BaseAnimalEntity.CurrentZMoveVelocityFactor -= deltaTime * BaseAnimalEntity.ZMoveAccelerationFactor;
                    BaseAnimalEntity.CurrentZMoveVelocityFactor = Mathf.Clamp(BaseAnimalEntity.CurrentZMoveVelocityFactor, currentYInput, 1f);
                }
            }

            float currentZMovementVelocizy = BaseAnimalEntity.CurrentZMoveVelocityFactor * BaseAnimalEntity.MaxZMoveVelocity * deltaTime;
            Vector3 inputPosition = new Vector3(0, 0, currentZMovementVelocizy);
            Vector3 direction = rigidbody.rotation * inputPosition;
            rigidbody.MovePosition(rigidbody.position + direction);
        }
    }
}
