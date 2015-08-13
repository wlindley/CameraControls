using UnityEngine;

namespace CamCon
{
    public interface Surface
    {
        Vector3 GetOrigin();
        Vector3 GetWorldUpVector();
        Vector3 GetNormalAtPoint(Vector3 position);
        float GetSurfaceHeightAtPoint(Vector3 position);
    }
}
