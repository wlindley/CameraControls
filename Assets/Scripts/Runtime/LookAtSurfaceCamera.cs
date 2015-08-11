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
            lookTarget = Surface.GetOrigin();
            height = 0f;
            transform.position = lookTarget;
            transform.forward = -Surface.GetNormalAtPoint(lookTarget);
        }

        public void SetHeight(float height)
        {
            this.height = height;
            UpdateTransform();
        }

        public void TranslateLookTargetTo(Vector3 target)
        {
            lookTarget = target;
            UpdateTransform();
        }

        public void TranslateLookTargetWithHeight(Vector3 surfacePoint, float height)
        {
            lookTarget = surfacePoint;
            this.height = height;
            UpdateTransform();
        }

        private void UpdateTransform()
        {
            var normal = Surface.GetNormalAtPoint(lookTarget);
            transform.position = lookTarget + (height * normal);
        }
    }
}
