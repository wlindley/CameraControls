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

        internal override Surface GetSurface()
        {
            return surface;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(renderSize, 0f, renderSize));
        }
    }
}
