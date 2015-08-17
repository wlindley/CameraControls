using UnityEngine;

namespace CamCon
{
    public class AngledLookAtSurfaceCamera : LookAtSurfaceCamera
    {
        public LerpCurve Curve { get; set; }

        protected override void UpdateTransform()
        {
            var cameraAngle = Curve.Probe(distanceToTarget);
            var offsetMagnitude = distanceToTarget * Mathf.Cos(cameraAngle);
            var heightFromSurface = distanceToTarget * Mathf.Sin(cameraAngle);
            var normal = Surface.GetNormalAtPoint(lookTarget);
            var up = Surface.GetWorldUpVector();

            transform.position = lookTarget + (heightFromSurface * normal) - (up * offsetMagnitude);
            PointCameraAtLookTarget();
        }
    }
}
