using UnityEngine;

namespace CamCon
{
    public abstract class MoverComponent : MonoBehaviour
    {
        internal abstract LookAtSurfaceCameraMover GetMover();
    }
}
