﻿using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class AngledLookAtSurfaceCameraTest
    {
        private AngledLookAtSurfaceCamera testObj;
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

            var testObjGO = new GameObject();
            testObj = testObjGO.AddComponent<AngledLookAtSurfaceCamera>();
            testObj.Surface = surface;
            testObj.Curve = curve;

            testObj.Start();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(testObj.gameObject);
            testObj = null;
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
            AssertApproximatelyEqual(cameraDistance, (testObj.transform.position - lookTarget).magnitude);

            var offsetLength = cameraDistance * Mathf.Cos(cameraAngle);
            var cameraHeight = cameraDistance * Mathf.Sin(cameraAngle);
            var expectedCameraPosition = lookTarget + (surfaceNormal * cameraHeight) + (-surfaceUp * offsetLength);
            AssertPositionsApproximatelyEqual(expectedCameraPosition, testObj.transform.position);
        }

        private void AssertApproximatelyEqual(float expected, float actual)
        {
            Assert.IsTrue(Mathf.Approximately(expected, actual), string.Format("Expected {0}, but got {1}", expected, actual));
        }

        private void AssertPositionsApproximatelyEqual(Vector3 expected, Vector3 actual)
        {
            Assert.IsTrue(expected == actual, string.Format("Expected {0}, but received {1}", expected, actual));
        }

        private void AssertTestObjLookingAt(Vector3 position)
        {
            var d = position - testObj.transform.position;
            Assert.IsTrue(testObj.transform.forward == d.normalized);
        }
    }
}
