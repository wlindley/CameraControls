using UnityEngine;
using System;

namespace CamCon
{
    public class SphereSurface : Surface
    {
        private Vector3 origin;
        private float radius;
        private Vector3 up;

        public Vector3 Origin
        {
            get
            {
                return origin;
            }
        }

        public float Radius
        {
            get
            {
                return radius;
            }
        }

        public SphereSurface(Vector3 origin, float radius, Vector3 up)
        {
            this.origin = origin;
            this.radius = radius;
            this.up = up;
        }

        virtual public Vector3 GetInitialPointOnSurface()
        {
            return origin + (radius * Vector3.right);
        }

        virtual public Vector3 GetWorldUpVector()
        {
            return up;
        }

        virtual public Vector3 GetNormalAtPoint(Vector3 position)
        {
            return (position - origin).normalized;
        }

        virtual public Vector3 ClampPointToSurface(Vector3 position)
        {
            return origin + (GetNormalAtPoint(position) * radius);
        }
    }
}
