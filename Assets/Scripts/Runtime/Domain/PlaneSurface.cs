using UnityEngine;
using System;

namespace CamCon
{
    public class PlaneSurface : Surface
    {
        private Vector3 origin;
        private Vector3 up;
        private Vector3 normal;

        public PlaneSurface(Vector3 origin, Vector3 normal, Vector3 up)
        {
            this.origin = origin;
            this.normal = normal.normalized;
            this.up = up;
        }

        virtual public Vector3 GetInitialPointOnSurface()
        {
            return origin;
        }

        virtual public Vector3 GetUpVectorAtPoint(Vector3 position)
        {
            return up;
        }

        virtual public Vector3 GetNormalAtPoint(Vector3 position)
        {
            return normal;
        }

        virtual public Vector3 ClampPointToSurface(Vector3 position)
        {
            var delta = position - origin;
            var distanceFromSurface = Vector3.Dot(delta, normal);
            return position - (distanceFromSurface * normal);
        }
    }
}
