using UnityEngine;

namespace CamCon
{
    public abstract class SurfaceComponent : MonoBehaviour, Surface
    {
        abstract public Vector3 GetInitialPointOnSurface();
        abstract public Vector3 GetWorldUpVector();
        abstract public Vector3 GetNormalAtPoint(Vector3 position);
        abstract public Vector3 ClampPointToSurface(Vector3 position);
    }
}
