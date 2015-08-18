using UnityEngine;
using System;

namespace CamCon
{
    public class SphereSurfaceComponent : SurfaceComponent
    {
        public float radius = 10f;
        private SphereSurface surface;

        internal override Surface GetSurface()
        {
            if (null == surface)
                surface = SphereSurface.GetInstance(transform.position, radius, transform.up);
            return surface;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
