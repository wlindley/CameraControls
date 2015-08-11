using UnityEngine;

namespace CamCon
{
    public class LookAtSurfaceCameraComponent : LookAtSurfaceCamera
    {
        public SurfaceComponent surfaceObj;

        public override void Start()
        {
            base.Surface = surfaceObj;
            base.Start();
        }
    }
}
