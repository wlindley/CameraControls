using UnityEngine;
using System;

namespace CamCon
{
    public class SphereSurface : Surface
    {
        public static SphereSurface TestInstance;
        public static SphereSurface GetInstance(Vector3 origin, float radius, Vector3 up)
        {
            if (null != TestInstance)
                return TestInstance;
            return new SphereSurface(origin, radius, up);
        }

        private Vector3 origin;
        private float radius;
        private Vector3 up;

        public SphereSurface(Vector3 origin, float radius, Vector3 up)
        {
            this.origin = origin;
            this.radius = radius;
            this.up = up;
        }

        public Vector3 GetInitialPointOnSurface()
        {
            return origin + (radius * Vector3.forward);
        }

        public Vector3 GetWorldUpVector()
        {
            return up;
        }

        public Vector3 GetNormalAtPoint(Vector3 position)
        {
            return (position - origin).normalized;
        }

        public Vector3 ClampPointToSurface(Vector3 position)
        {
            return origin + (GetNormalAtPoint(position) * radius);
        }
    }
}
