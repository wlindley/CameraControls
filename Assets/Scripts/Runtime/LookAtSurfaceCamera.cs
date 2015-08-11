using UnityEngine;

namespace CamCon
{
    public class LookAtSurfaceCamera : MonoBehaviour
    {
        private Vector3 lookTarget;
        private float height;

        public Surface Surface { get; set; }

        public virtual void Start()
        {
            lookTarget = Surface.GetOrigin();
            height = 0f;
            transform.position = lookTarget;
            transform.forward = -Surface.GetNormalAtPoint(lookTarget);
        }

        public virtual void SetHeight(float height)
        {
            this.height = height;
            UpdateTransform();
        }

        public virtual void TranslateLookTargetTo(Vector3 target)
        {
            lookTarget = target;
            UpdateTransform();
        }

        public virtual void TranslateLookTargetWithHeight(Vector3 surfacePoint, float height)
        {
            lookTarget = surfacePoint;
            this.height = height;
            UpdateTransform();
        }

        private void UpdateTransform()
        {
            var normal = Surface.GetNormalAtPoint(lookTarget);
            transform.position = lookTarget + (height * normal);
            //transform.localRotation.SetLookRotation(lookTarget - transform.position, Surface.GetWorldUpVector());
            transform.LookAt(lookTarget, Surface.GetWorldUpVector());
        }
    }
}
