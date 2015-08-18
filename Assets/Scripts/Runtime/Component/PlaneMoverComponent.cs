using UnityEngine;

namespace CamCon
{
    public class PlaneMoverComponent : MoverComponent
    {
        public CameraComponent lookAtCamera;
        public PlaneSurfaceComponent surface;
        public float zoomSpeed;
        public float panSpeed;

        private PlaneMover mover;

        internal override LookAtSurfaceCameraMover GetMover()
        {
            if (null == mover)
            {
                var planeSurface = surface.GetSurface() as PlaneSurface;
                mover = new PlaneMover(lookAtCamera.GetCamera(), planeSurface, zoomSpeed, panSpeed);
            }
            return mover;
        }
    }
}
