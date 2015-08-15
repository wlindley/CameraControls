﻿using UnityEngine;

namespace CamCon
{
    public interface Surface
    {
        Vector3 GetInitialPointOnSurface();
        Vector3 GetWorldUpVector();
        Vector3 GetNormalAtPoint(Vector3 position);
        Vector3 ClampPointToSurface(Vector3 position);
    }
}
