using UnityEngine;

namespace CamCon
{
    public class LookAtSurfaceCamera : MonoBehaviour
    {
        private Vector3 lookTarget;
        private float height;

        public Surface Surface { get; set; }

        public void Start()
        {
            transform.position = Surface.GetOrigin();
            transform.forward = -Surface.GetNormalAtPoint(transform.position);
            //transform.LookAt(Surface.GetOrigin(), Surface.GetWorldUpVector());
        }

        public void RotateToLookAt(Vector3 target)
        {
            /*
            lookTarget = target;
            transform.LookAt(target, Surface.GetWorldUpVector());
            */
        }

        public void TranslateToLookAt(Vector3 target)
        {
            /*
            var prevNormal = Surface.GetNormalAtPoint(lookTarget);

            var offset = lookTarget - transform.position;
            lookTarget = target;
            transform.position = lookTarget - offset;
            */

            SetHeightAboveSurface(target, height);
        }

        public void SetHeightAboveSurface(Vector3 surfacePoint, float height)
        {
            //this.height = height;
            var normal = Surface.GetNormalAtPoint(surfacePoint);
            transform.position = surfacePoint + (height * normal);
        }
    }
}
