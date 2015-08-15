using UnityEngine;
using System;

namespace CamCon
{
    public class PlaneSurfaceComponent : SurfaceComponent
    {
        public float renderSize = 100f;
        private PlaneSurface surface;

        public void Awake()
        {
            surface = PlaneSurface.GetInstance(transform.position, transform.up, transform.forward);
        }

        public override Vector3 GetInitialPointOnSurface()
        {
            return surface.GetInitialPointOnSurface();
        }

        public override Vector3 GetWorldUpVector()
        {
            return surface.GetWorldUpVector();
        }

        public override Vector3 GetNormalAtPoint(Vector3 position)
        {
            return surface.GetNormalAtPoint(position);
        }

        public override Vector3 ClampPointToSurface(Vector3 position)
        {
            return surface.ClampPointToSurface(position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(renderSize, 0f, renderSize));
        }
    }
}
