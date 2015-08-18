using UnityEngine;

namespace CamCon
{
    public class PlaneMoverComponent : MoverComponent
    {
        public LookAtSurfaceCamera cam;
        public PlaneSurfaceComponent surface;
        public float zoomSpeed;
        public float panSpeed;

        private PlaneMover mover;

        internal override LookAtSurfaceCameraMover GetMover()
        {
            if (null == mover)
            {
                var planeSurface = surface.GetSurface() as PlaneSurface;
                mover = new PlaneMover(cam, planeSurface, zoomSpeed, panSpeed);
            }
            return mover;
        }
    }
}
