using UnityEngine;

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
        public Vector3 GetOrigin()
        {
            return origin;
        }

        public Vector3 GetWorldUpVector()
        {
            return up;
        }

        public Vector3 GetNormalAtPoint(Vector3 position)
        {
            var delta = position - origin;
            if (origin == position)
                return Vector3.forward;
            if (Mathf.Abs(delta.x) >= sideLength * .5f)
                return new Vector3(delta.x / Mathf.Abs(delta.x), 0f, 0f);
            else if (Mathf.Abs(delta.y) >= sideLength * .5f)
                return new Vector3(0f, delta.y / Mathf.Abs(delta.y), 0f);
            else if (Mathf.Abs(delta.z) >= sideLength * .5f)
                return new Vector3(0f, 0f, delta.z / Mathf.Abs(delta.z));
            return Vector3.zero;
        }

        public float GetSurfaceHeightAtPoint(Vector3 position)
        {
            return sideLength * .5f;
        }
    }
}
