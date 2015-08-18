using UnityEngine;
using System;

namespace CamCon
{
    public class CubeSurfaceComponent : SurfaceComponent
    {
        public float sideLength = 10f;
        private CubeSurface surface;

        internal override Surface GetSurface()
        {
            if (null == surface)
                surface = CubeSurface.GetInstance(transform.position, sideLength, transform.up);
            return surface;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, Vector3.one * sideLength);
        }
    }
}
