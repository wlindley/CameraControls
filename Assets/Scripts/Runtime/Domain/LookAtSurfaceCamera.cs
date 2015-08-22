using UnityEngine;
using System;

namespace CamCon
{
    public class LookAtSurfaceCamera
    {
        public const float InitialDistanceToTarget = 1f;

        protected Transform transform;
        protected Surface Surface;
        protected Vector3 lookTarget;
        protected float distanceToTarget;

        public LookAtSurfaceCamera() : this(null, null) { }

        public LookAtSurfaceCamera(Transform transform, Surface surface)
        {
            this.transform = transform;
            this.Surface = surface;
        }

        public virtual void InitializeCamera()
        {
            TranslateLookTargetAndDistance(Surface.GetInitialPointOnSurface(), InitialDistanceToTarget);
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
            transform.LookAt(lookTarget, Surface.GetUpVectorAtPoint(lookTarget));
        }
    }
}
