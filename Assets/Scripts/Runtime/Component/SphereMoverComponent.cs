using UnityEngine;

namespace CamCon
{
    public class SphereMoverComponent : MoverComponent
    {
        public CameraComponent lookAtCamera;
        public SphereSurfaceComponent surface;
        public float zoomSpeed;
        public float panSpeed;

        private SphereMover mover;

        internal override LookAtSurfaceCameraMover GetMover()
        {
            if (null == mover)
            {
                var sphereSurface = surface.GetSurface() as SphereSurface;
                mover = new SphereMover(lookAtCamera.GetCamera(), sphereSurface, zoomSpeed, panSpeed);
            }
            return mover;
        }
    }
}
