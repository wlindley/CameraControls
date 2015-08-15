using UnityEngine;
using System;

namespace CamCon
{
    public class SphereSurfaceComponent : SurfaceComponent
    {
        public float radius = 10f;
        private SphereSurface surface;

        public void Awake()
        {
            surface = SphereSurface.GetInstance(transform.position, radius, transform.up);
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
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
