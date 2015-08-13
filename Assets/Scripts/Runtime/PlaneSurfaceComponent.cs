using UnityEngine;

namespace CamCon
{
    public class PlaneSurfaceComponent : SurfaceComponent
    {
        public float renderSize = 100f;
        private PlaneSurface surface;

        public void Awake()
        {
            surface = new PlaneSurface(transform.position, transform.up, transform.forward);
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

        public override float GetSurfaceHeightAtPoint(Vector3 position)
        {
            return surface.GetSurfaceHeightAtPoint(position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(renderSize, 0f, renderSize));
        }
    }
}
