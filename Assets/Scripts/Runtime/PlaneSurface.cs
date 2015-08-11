using UnityEngine;

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
            this.normal = normal;
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
            return normal;
        }
    }
}
