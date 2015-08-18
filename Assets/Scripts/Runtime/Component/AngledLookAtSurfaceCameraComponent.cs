using UnityEngine;
using System;

namespace CamCon
{
    public class AngledLookAtSurfaceCameraComponent : CameraComponent
    {
        public SurfaceComponent surface;
        public CameraControlPoint[] controlPoints;

        private AngledLookAtSurfaceCamera cam;

        internal override LookAtSurfaceCamera GetCamera()
        {
            if (null == cam)
                BuildCamera();
            return cam;
        }

        private void BuildCamera()
        {
            var curvePoints = new CurveControlPoint[controlPoints.Length];
            for (var i = 0; i < controlPoints.Length; i++)
                curvePoints[i] = new CurveControlPoint(controlPoints[i].distanceToLookTarget, controlPoints[i].cameraAngle);
            var curve = new LerpCurve(curvePoints);

            cam = new AngledLookAtSurfaceCamera(transform, surface.GetSurface(), curve);
            cam.InitializeCamera();
        }
        
        [Serializable]
        public class CameraControlPoint
        {
            public float distanceToLookTarget;

            [Range(0f, 90f)]
            [Tooltip("0 degrees is the camera looking horizontally, 90 degrees is the camera looking vertically")]
            public float cameraAngle = 90f;
        }
    }
}
