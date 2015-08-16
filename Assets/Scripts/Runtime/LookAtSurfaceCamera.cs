using UnityEngine;
using System;

namespace CamCon
{
    public class LookAtSurfaceCamera : MonoBehaviour
    {
        public const float InitialDistanceToTarget = 1f;
        private Vector3 lookTarget;
        private float distanceToTarget;

        public Surface Surface { get; set; }

        public virtual void Start()
        {
            TranslateLookTargetAndDistance(Surface.GetInitialPointOnSurface(), 1f);
        }

        public virtual Vector3 GetLookTarget()
        {
            return lookTarget;
        }

        public virtual float GetDistanceToTarget()
        {
            return distanceToTarget;
        }

        public virtual void SetDistanceToTarget(float distance)
        {
            this.distanceToTarget = distance;
            UpdateTransform();
        }

        public virtual void TranslateLookTargetTo(Vector3 target)
        {
            lookTarget = target;
            UpdateTransform();
        }

        public virtual void TranslateLookTargetAndDistance(Vector3 surfacePoint, float distance)
        {
            lookTarget = surfacePoint;
            this.distanceToTarget = distance;
            UpdateTransform();
        }

        protected virtual void UpdateTransform()
        {
            transform.position = CalculatePointDirectlyAboveLookTarget();
            PointCameraAtLookTarget();
        }

        protected Vector3 CalculatePointDirectlyAboveLookTarget()
        {
            var normal = Surface.GetNormalAtPoint(lookTarget);
            return lookTarget + (distanceToTarget * normal);
        }

        protected void PointCameraAtLookTarget()
        {
            transform.LookAt(lookTarget, Surface.GetWorldUpVector());
        }
    }
}
