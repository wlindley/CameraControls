using UnityEngine;

namespace CamCon
{
    public class CubeSurfaceComponent : SurfaceComponent
    {
        public float sideLength = 10f;

        private CubeSurface surface;

        public void Awake()
        {
            surface = new CubeSurface(transform.position, sideLength, transform.up);
        }

        public override Vector3 GetOrigin()
        {
            return surface.GetOrigin();
        }

        public override Vector3 GetWorldUpVector()
        {
            return surface.GetWorldUpVector();
        }

        public override Vector3 GetNormalAtPoint(Vector3 position)
        {
            return surface.GetNormalAtPoint(position);
        }

        public override float GetSurfaceHeightAtPoint(Vector3 position)
        {
            return surface.GetSurfaceHeightAtPoint(position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, Vector3.one * sideLength);
        }
    }
}
