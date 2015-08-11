using UnityEngine;

namespace CamCon
{
    public class LookAtSurfaceCamera : MonoBehaviour
    {
        //private Vector3 lookTarget;

        public Surface Surface { get; set; }

        public void LookAt(Vector3 target)
        {
            //lookTarget = target;
            transform.LookAt(target, Surface.GetWorldUpVector());
        }

        public void SetHeightAboveSurface(Vector3 surfacePoint, float height)
        {
            var normal = Surface.GetNormalAtPoint(surfacePoint);
            transform.position = surfacePoint + (height * normal);
        }
    }
}
