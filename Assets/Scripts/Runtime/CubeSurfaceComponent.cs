using UnityEngine;
using System;

namespace CamCon
{
    public class CubeSurfaceComponent : SurfaceComponent
    {
        public float sideLength = 10f;
        private CubeSurface surface;

        public void Awake()
        {
            surface = CubeSurface.GetInstance(transform.position, sideLength, transform.up);
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
            Gizmos.DrawWireCube(transform.position, Vector3.one * sideLength);
        }
    }
}
