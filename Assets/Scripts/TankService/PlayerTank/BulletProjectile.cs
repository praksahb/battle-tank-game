using System;
using System.Collections.Generic;
using TankBattle.Tank;
using TankBattle.Tank.PlayerTank;
using UnityEngine;

namespace TankBattle.Services
{
    public class BulletProjectile : MonoBehaviour
    {
        [SerializeField] private LineRenderer trajectoryLineRenderer;
        [SerializeField] private int numberOfPoints = 30;

        private float currentLaunchForce;

        private TankController playerController;
        private Transform firePointTransform;

        private List<Vector3> trajectoryPoints = new List<Vector3>();

        private float timeStep = 0.05f;
        private float checkCollisionRadius = 0.15f;

        public static bool isFirePressed;

        private void Update()
        {

            if (isFirePressed && trajectoryLineRenderer != null)
            {
                DrawTrajectory();
            }
            else
            {
                trajectoryLineRenderer.enabled = false;
            }
        }

        private void Start()
        {
            playerController = PlayerService.Instance.GetTankController();
            currentLaunchForce = playerController.TankModel.BulletLaunchForce;

        }

        private void DrawTrajectory()
        {
            firePointTransform = playerController.TankView.GetFireTransform();

            float gravity = Physics.gravity.y;

            trajectoryPoints.Clear();
            trajectoryPoints.Add(firePointTransform.position);
            trajectoryLineRenderer.enabled = true;

            Vector3 initialVelocity = firePointTransform.forward * currentLaunchForce;
            Vector3 position = firePointTransform.position;
            Vector3 currentVelocity = initialVelocity;

            for (int i = 1; i < numberOfPoints; i++)
            {
                position += currentVelocity * timeStep + 0.5f * Vector3.up * gravity * timeStep * timeStep;
                currentVelocity += firePointTransform.up * gravity * timeStep;

                if(CheckForCollision(position))
                {
                    break;
                }

                trajectoryPoints.Add(position);
            }
            trajectoryLineRenderer.positionCount = trajectoryPoints.Count;
            trajectoryLineRenderer.SetPositions(trajectoryPoints.ToArray());
        }

        private bool CheckForCollision(Vector3 position)
        {
            Collider[] hitColliders = new Collider[5];
            int hits = Physics.OverlapSphereNonAlloc(position, checkCollisionRadius, hitColliders);
            if(hits > 0)
            {
                return true;
            }
            return false;
        }
    }
}
