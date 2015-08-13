using UnityEngine;

namespace CamCon
{
    public abstract class SurfaceComponent : MonoBehaviour, Surface
    {
        abstract public Vector3 GetOrigin();
        abstract public Vector3 GetWorldUpVector();
        abstract public Vector3 GetNormalAtPoint(Vector3 position);
        abstract public float GetSurfaceHeightAtPoint(Vector3 position);
    }
}
