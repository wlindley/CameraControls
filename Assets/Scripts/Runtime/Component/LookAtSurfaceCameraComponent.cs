using UnityEngine;

namespace CamCon
{
    public class LookAtSurfaceCameraComponent : CameraComponent
    {
        public SurfaceComponent surface;

        private LookAtSurfaceCamera cam;

        internal override LookAtSurfaceCamera GetCamera()
        {
            if (null == cam)
            {
                cam = new LookAtSurfaceCamera(transform, surface.GetSurface());
                cam.InitializeCamera();
            }
            return cam;
        }
    }
}
