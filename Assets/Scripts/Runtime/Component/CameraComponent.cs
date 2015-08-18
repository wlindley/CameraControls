using UnityEngine;

namespace CamCon
{
    public abstract class CameraComponent : MonoBehaviour
    {
        internal abstract LookAtSurfaceCamera GetCamera();
    }
}
