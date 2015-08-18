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

        internal override Surface GetSurface()
        {
            return surface;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
