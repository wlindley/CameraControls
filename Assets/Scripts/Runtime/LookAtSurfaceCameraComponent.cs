using UnityEngine;

namespace CamCon
{
    public class LookAtSurfaceCameraComponent : LookAtSurfaceCamera
    {
        public SurfaceComponent surface;

        public override void Start()
        {
            base.Surface = surface;
            base.Start();
        }
    }
}
