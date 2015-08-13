using UnityEngine;

namespace CamCon
{
    public class LookAtSurfaceCamera : MonoBehaviour
    {
        private const float InitialHeightAboveSurface = 1f;
        private Vector3 lookTarget;
        private float height;

        public Surface Surface { get; set; }

        public virtual void Start()
        {
            var origin = Surface.GetOrigin();
            var normal = Surface.GetNormalAtPoint(origin);
            var height = Surface.GetSurfaceHeightAtPoint(origin);
            TranslateLookTargetWithHeight(origin + (normal * height), InitialHeightAboveSurface);
        }

        public virtual Vector3 GetLookTarget()
        {
            return lookTarget;
        }

        public virtual float GetHeightAboveSurface()
        {
            return height;
        }

        public virtual void SetHeightAboveSurface(float height)
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
