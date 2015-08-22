using UnityEngine;
using System;

namespace CamCon
{
    public class CubeSurface : Surface
    {
        private Vector3 origin;
        private Vector3 up;
        private float sideLength;

        public CubeSurface(Vector3 origin, float sideLength, Vector3 up)
        {
            this.origin = origin;
            this.sideLength = sideLength;
            this.up = up;
        }

        virtual public Vector3 GetInitialPointOnSurface()
        {
            return origin + (.5f * sideLength * Vector3.forward);
        }

        virtual public Vector3 GetUpVectorAtPoint(Vector3 position)
        {
            return up;
        }

        virtual public Vector3 GetNormalAtPoint(Vector3 position)
        {
            var delta = position - origin;
            if (Mathf.Abs(delta.x) >= sideLength * .5f)
                return new Vector3(delta.x / Mathf.Abs(delta.x), 0f, 0f);
            else if (Mathf.Abs(delta.y) >= sideLength * .5f)
                return new Vector3(0f, delta.y / Mathf.Abs(delta.y), 0f);
            else if (Mathf.Abs(delta.z) >= sideLength * .5f)
                return new Vector3(0f, 0f, delta.z / Mathf.Abs(delta.z));
            return Vector3.zero;
        }

        virtual public Vector3 ClampPointToSurface(Vector3 position)
        {
            var diff = position - origin;

            diff.x = Mathf.Clamp(diff.x, -.5f * sideLength, .5f * sideLength);
            diff.y = Mathf.Clamp(diff.y, -.5f * sideLength, .5f * sideLength);
            diff.z = Mathf.Clamp(diff.z, -.5f * sideLength, .5f * sideLength);

            var maxComponentMagnitude = Mathf.Max(Mathf.Max(Mathf.Abs(diff.x), Mathf.Abs(diff.y)), Mathf.Abs(diff.z));
            var finalDiff = diff;
            if (Mathf.Approximately(diff.x, maxComponentMagnitude))
                finalDiff.x = sideLength * .5f * (diff.x / Mathf.Abs(diff.x));
            if (Mathf.Approximately(diff.y, maxComponentMagnitude))
                finalDiff.y = sideLength * .5f * (diff.y / Mathf.Abs(diff.y));
            if (Mathf.Approximately(diff.z, maxComponentMagnitude))
                finalDiff.z = sideLength * .5f * (diff.z / Mathf.Abs(diff.z));

            return origin + finalDiff;
        }
    }
}
