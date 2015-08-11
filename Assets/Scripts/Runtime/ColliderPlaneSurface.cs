using UnityEngine;

namespace CamCon
{
    [RequireComponent(typeof(BoxCollider))]
    public class ColliderPlaneSurface : SurfaceComponent
    {
        private BoxCollider box;
        private PlaneSurface surface;

        public void Awake()
        {
            box = GetComponent<BoxCollider>();
            surface = new PlaneSurface(transform.position + box.center, transform.up, transform.forward);
        }

        public override Vector3 GetOrigin()
        {
            return surface.GetOrigin();
        }

        public override Vector3 GetWorldUpVector()
        {
            return surface.GetWorldUpVector();
        }

        public override Vector3 GetNormalAtPoint(Vector3 position)
        {
            return surface.GetNormalAtPoint(position);
        }
    }
}
