using UnityEngine;

namespace CamCon
{
    public class AABoxCameraBounds : LookAtSurfaceCameraBounds
    {
        private Surface surface;
        private Vector3 boundsCenter;
        private Vector3 boundsSize;

        public AABoxCameraBounds(Surface surface, Vector3 boundsCenter, Vector3 boundsSize)
        {
            this.surface = surface;
            this.boundsCenter = boundsCenter;
            this.boundsSize = boundsSize;
        }

        public void ClampCameraToBounds(LookAtSurfaceCamera camera)
        {
            var maxY = boundsCenter.y + (boundsSize.y * .5f);
            var minY = boundsCenter.y - (boundsSize.y * .5f);
            var distanceToTarget = camera.GetDistanceToTarget();
            if (distanceToTarget >= maxY)
                camera.SetDistanceToTarget(maxY);
            else if (distanceToTarget <= minY)
                camera.SetDistanceToTarget(minY);

            var minX = boundsCenter.x - (boundsSize.x * .5f);
            var lookTarget = camera.GetLookTarget();
            var delta = lookTarget - boundsCenter;
            if (lookTarget.x <= minX)
                camera.TranslateLookTargetTo(new Vector3(minX, lookTarget.y, lookTarget.z));

        }
    }
}
