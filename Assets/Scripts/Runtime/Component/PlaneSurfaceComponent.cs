using UnityEngine;
using System;

namespace CamCon
{
    public class PlaneSurfaceComponent : SurfaceComponent
    {
        public float renderSize = 100f;
        private PlaneSurface surface;

        internal override Surface GetSurface()
        {
            if (null == surface)
                surface = PlaneSurface.GetInstance(transform.position, transform.up, transform.forward);
            return surface;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(renderSize, 0f, renderSize));
        }
    }
}
