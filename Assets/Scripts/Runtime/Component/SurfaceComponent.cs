using UnityEngine;

namespace CamCon
{
    public abstract class SurfaceComponent : MonoBehaviour
    {
        abstract internal Surface GetSurface();
    }
}
