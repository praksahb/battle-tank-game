using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle.Services
{
    public class CameraController : GenericMonoSingleton<CameraController>
    {
        // Approximate time for the camera to refocus.
        [SerializeField] private float dampTime = 0.2f;
        // Space between the top/bottom most target and the screen edge
        [SerializeField] private float screenEdgeBuffer = 4f;
        // The smallest orthographic size the camera can be
        [SerializeField] private float minSize = 6.5f;

        public List<Transform> targets;
        private Camera mainCamera;


        // Reference speed for the smooth damping of the orthographic size
        private float zoomSpeed;
        // Reference velocity for the smooth damping of the position
        private Vector3 moveVelocity;
        // The position the camera is moving towards
        private Vector3 desiredPosition;

         protected override void Awake()
        {
            base.Awake();
            mainCamera = GetComponentInChildren<Camera>();
            targets = new List<Transform>();
        }

        private void LateUpdate()
        {
            Move();
            Zoom();
        }

        public void AddTransformToTarget(Transform tankTransform)
        {
            targets.Add(tankTransform);
        }

        public void SetStartPositionAndSize()
        {
            FindAveragePosition();
            transform.position = desiredPosition;
            mainCamera.orthographicSize = FindRequiredSize();
        }

        private void Move()
        {
            FindAveragePosition();

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);
        }

        private void FindAveragePosition()
        {

            Vector3 averagePos = new Vector3();
            int numTargets = 0;

            // Go through all the targets and add their positions together
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != null)
                {
                    // Add to the average and increment the number of targets in the average.
                    averagePos += targets[i].position;
                    numTargets++;
                }
            }
            // If there are targets divide the sum of the positions by the number of them to find the average.
            if (numTargets > 0)
            {
                averagePos /= numTargets;
            }

            averagePos.y = transform.position.y;
            desiredPosition = averagePos;
            
        }

        private void Zoom()
        {
            // Find the required size based on the desired position and smoothly transition to that size.
            float requiredSize = FindRequiredSize();
            mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
        }

        private float FindRequiredSize()
        {
            // Find the position the camera rig is moving towards in its local space.
            Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);
            float size = 0f;

            // Go through all the targets
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != null)
                {
                    // find the position of the target in the camera's local space
                    Vector3 targetLocalPos = transform.InverseTransformPoint(targets[i].position);
                    // Find the position of the target from the desired position of the camera's local space.
                    Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
                    // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
                    size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
                    // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
                    size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / mainCamera.aspect);
                }
            }
            // Add the edge buffer to the size.
            size += screenEdgeBuffer;
            size = Mathf.Max(size, minSize);
            return size;
        }
    }
}
