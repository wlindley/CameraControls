using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class LookAtSurfaceCameraTest
    {
        private LookAtSurfaceCamera testObj;
        private Transform testTransform;
        private Transform targetTransform;
        private Surface surface;

        [SetUp]
        public void SetUp()
        {
            var testGO = new GameObject();
            testTransform = testGO.transform;

            var targetGO = new GameObject();
            targetTransform = targetGO.transform;

            surface = Substitute.For<Surface>();
        }

        private void BuildTestObj()
        {
            testObj = new LookAtSurfaceCamera(testTransform, surface);
            testObj.InitializeCamera();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(testTransform.gameObject);
            testTransform = null;
            GameObject.DestroyImmediate(targetTransform.gameObject);
            targetTransform = null;
        }

        [Test]
        public void CameraStartsAboveSurfaceAtOriginLookingDownAtDistanceOne()
        {
            var origin = new Vector3(0, 10, 0);
            var normalAtOrigin = new Vector3(0, 1, 0);
            var worldUp = new Vector3(0, 0, 1);
            surface.GetInitialPointOnSurface().Returns(origin);
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(normalAtOrigin);
            surface.GetUpVectorAtPoint(Arg.Any<Vector3>()).Returns(worldUp);

            BuildTestObj();

            TestUtil.AssertApproximatelyEqual(origin + (normalAtOrigin * LookAtSurfaceCamera.InitialDistanceToTarget), testTransform.position);
            TestUtil.AssertApproximatelyEqual(-normalAtOrigin, testTransform.forward);
            TestUtil.AssertApproximatelyEqual(worldUp, testTransform.up);
            TestUtil.AssertApproximatelyEqual(origin, testObj.GetLookTarget());
            TestUtil.AssertApproximatelyEqual(1f, testObj.GetDistanceToTarget());
        }

        [Test]
        public void TranslateLookTargetAndDistanceMovesCameraAlongSurfaceNormal()
        {
            var normal = new Vector3(0, 1, 0);
            var pos = new Vector3(10, 0, 10);
            var distance = 2f;
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(normal);

            BuildTestObj();
            testObj.TranslateLookTargetAndDistance(pos, distance);

            TestUtil.AssertApproximatelyEqual(pos + (normal * distance), testTransform.position);
        }

        [Test]
        public void SetDistanceToTargetMovesCameraAlongSurfaceNormal()
        {
            var origin = new Vector3(0, 10, 0);
            var normalAtOrigin = new Vector3(0, 1, 0);
            var worldUp = new Vector3(0, 0, 1);
            var distance = 5f;
            surface.GetInitialPointOnSurface().Returns(origin);
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(normalAtOrigin);
            surface.GetUpVectorAtPoint(Arg.Any<Vector3>()).Returns(worldUp);

            BuildTestObj();
            testObj.SetDistanceToTarget(distance);

            TestUtil.AssertApproximatelyEqual(origin + (normalAtOrigin * distance), testTransform.position);
            TestUtil.AssertApproximatelyEqual(-normalAtOrigin, testTransform.forward);
            TestUtil.AssertApproximatelyEqual(worldUp, testTransform.up);
            TestUtil.AssertApproximatelyEqual(distance, testObj.GetDistanceToTarget());
        }

        [Test]
        public void TranslateLookTargetToMaintainsDistance()
        {
            var origin = new Vector3(0, 10, 0);
            var newTarget = new Vector3(10, 10, 10);
            var normal = new Vector3(0, 1, 0);
            var worldUp = new Vector3(0, 0, 1);
            var distance = 5f;
            surface.GetInitialPointOnSurface().Returns(origin);
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(normal);
            surface.GetUpVectorAtPoint(Arg.Any<Vector3>()).Returns(worldUp);

            BuildTestObj();
            testObj.SetDistanceToTarget(distance);
            testObj.TranslateLookTargetTo(newTarget);

            TestUtil.AssertApproximatelyEqual(newTarget + (normal * distance), testTransform.position);
            TestUtil.AssertApproximatelyEqual(-normal, testTransform.forward);
            TestUtil.AssertApproximatelyEqual(worldUp, testTransform.up);
            TestUtil.AssertApproximatelyEqual(newTarget, testObj.GetLookTarget());
        }

        [Test]
        public void TranslateLookTargetToRespectsChangingNormal()
        {
            var origin = new Vector3(0, 10, 0);
            var newTarget = new Vector3(10, 10, 10);
            var normalAtOrigin = new Vector3(0, 1, 0);
            var normalAtNewTarget = new Vector3(0, 0, 1);
            var worldUp = new Vector3(0, 0, 1);
            var distance = 5f;
            surface.GetInitialPointOnSurface().Returns(origin);
            surface.GetNormalAtPoint(origin).Returns(normalAtOrigin);
            surface.GetNormalAtPoint(newTarget).Returns(normalAtNewTarget);
            surface.GetUpVectorAtPoint(Arg.Any<Vector3>()).Returns(worldUp);

            BuildTestObj();
            testObj.SetDistanceToTarget(distance);
            testObj.TranslateLookTargetTo(newTarget);

            TestUtil.AssertApproximatelyEqual(newTarget + (normalAtNewTarget * distance), testTransform.position);
            TestUtil.AssertApproximatelyEqual(-normalAtNewTarget, testTransform.forward);
            //Assert.IsTrue(worldUp == testTransform.up);
        }

        private void AssertCameraLookingAt(Vector3 pos)
        {
            var diff = targetTransform.position - testTransform.position;
            TestUtil.AssertApproximatelyEqual(testTransform.forward, diff.normalized);
        }
    }
}
