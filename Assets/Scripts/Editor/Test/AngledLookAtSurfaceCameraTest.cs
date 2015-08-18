using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class AngledLookAtSurfaceCameraTest
    {
        private AngledLookAtSurfaceCamera testObj;
        private Transform testTransform;
        private PlaneSurface surface;
        private Vector3 surfaceOrigin;
        private Vector3 surfaceNormal;
        private Vector3 surfaceUp;
        private LerpCurve curve;
        private CurveControlPoint[] curvePoints;

        [SetUp]
        public void SetUp()
        {
            surfaceOrigin = Vector3.zero;
            surfaceNormal = Vector3.up;
            surfaceUp = Vector3.forward;
            surface = new PlaneSurface(surfaceOrigin, surfaceNormal, surfaceUp);

            curvePoints = new CurveControlPoint[2];
            curvePoints[0] = new CurveControlPoint(1f, 10f);
            curvePoints[1] = new CurveControlPoint(10f, 90f);
            curve = new LerpCurve(curvePoints);

            var testGO = new GameObject();
            testTransform = testGO.transform;
            testObj = new AngledLookAtSurfaceCamera(testTransform, surface, curve);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(testTransform.gameObject);
            testTransform = null;
        }

        [Test]
        public void SetDistanceToTargetSetsCameraAngleBasedOnCurve()
        {
            var cameraDistance = curvePoints[0].Input;
            var cameraAngle = curvePoints[0].Output * Mathf.Deg2Rad;
            AssertCameraIsLookingAtTargetWithCorrectAngle(cameraDistance, cameraAngle);
        }

        [Test]
        public void SetDistanceToTargetSetsCameraAngleBasedOnCurveAgain()
        {
            var cameraDistance = curvePoints[1].Input;
            var cameraAngle = curvePoints[1].Output * Mathf.Deg2Rad;
            AssertCameraIsLookingAtTargetWithCorrectAngle(cameraDistance, cameraAngle);
        }

        private void AssertCameraIsLookingAtTargetWithCorrectAngle(float cameraDistance, float cameraAngle)
        {
            testObj.SetDistanceToTarget(cameraDistance);

            var lookTarget = surface.GetInitialPointOnSurface();

            AssertTestObjLookingAt(lookTarget);
            TestUtil.AssertApproximatelyEqual(cameraDistance, (testTransform.position - lookTarget).magnitude);

            var offsetLength = cameraDistance * Mathf.Cos(cameraAngle);
            var cameraHeight = cameraDistance * Mathf.Sin(cameraAngle);
            var expectedCameraPosition = lookTarget + (surfaceNormal * cameraHeight) + (-surfaceUp * offsetLength);
            TestUtil.AssertApproximatelyEqual(expectedCameraPosition, testTransform.position);
        }

        private void AssertTestObjLookingAt(Vector3 position)
        {
            var d = position - testTransform.position;
            TestUtil.AssertApproximatelyEqual(testTransform.forward, d.normalized);
        }
    }
}
