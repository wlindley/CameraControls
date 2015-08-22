using UnityEngine;

namespace CamCon
{
    public class AngledLookAtSurfaceCamera : LookAtSurfaceCamera
    {
        private LerpCurve curve;

        public AngledLookAtSurfaceCamera(Transform transform, Surface surface, LerpCurve curve) : base(transform, surface)
        {
            this.curve = curve;
        }

        protected override void UpdateTransform()
        {
            var cameraAngle = curve.Probe(distanceToTarget) * Mathf.Deg2Rad;
            var offsetMagnitude = distanceToTarget * Mathf.Cos(cameraAngle);
            var heightFromSurface = distanceToTarget * Mathf.Sin(cameraAngle);
            var normal = Surface.GetNormalAtPoint(lookTarget);
            var up = Surface.GetUpVectorAtPoint(lookTarget);

            transform.position = lookTarget + (heightFromSurface * normal) - (up * offsetMagnitude);
            PointCameraAtLookTarget();
        }
    }
}
