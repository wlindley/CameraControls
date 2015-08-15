﻿using UnityEngine;
using System;

namespace CamCon
{
    public class PlaneSurface : Surface
    {
        public static PlaneSurface TestInstance = null;
        public static PlaneSurface GetInstance(Vector3 origin, Vector3 normal, Vector3 up)
        {
            if (null != TestInstance)
                return TestInstance;
            return new PlaneSurface(origin, normal, up);
        }

        private Vector3 origin;
        private Vector3 up;
        private Vector3 normal;

        public PlaneSurface() : this(Vector3.zero, Vector3.up, Vector3.forward) { }

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

        virtual public Vector3 GetWorldUpVector()
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
            var distanceFromSurface = Vector3.Dot(position, normal);
            return position - (distanceFromSurface * normal);
        }
    }
}
