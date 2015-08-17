using UnityEngine;
using System;

namespace CamCon
{
    public class AngledLookAtSurfaceCameraComponent : AngledLookAtSurfaceCamera
    {
        public SurfaceComponent surface;
        public CameraControlPoint[] controlPoints;

        public override void Start()
        {
            base.Surface = surface;

            var curvePoints = new CurveControlPoint[controlPoints.Length];
            for (var i = 0; i < controlPoints.Length; i++)
                curvePoints[i] = new CurveControlPoint(controlPoints[i].distanceToLookTarget, controlPoints[i].cameraAngle);
            base.Curve = new LerpCurve(curvePoints);

            base.Start();
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
