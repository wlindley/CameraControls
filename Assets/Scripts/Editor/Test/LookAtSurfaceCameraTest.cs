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
        private SphereCollider target;
        private Surface surface;

        [SetUp]
        public void SetUp()
        {
            var testObjGO = new GameObject();
            testObj = testObjGO.AddComponent<LookAtSurfaceCamera>();

            surface = Substitute.For<Surface>();
            testObj.Surface = surface;

            var targetGO = new GameObject();
            target = targetGO.AddComponent<SphereCollider>();
            target.radius = .25f;
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(testObj.gameObject);
            testObj = null;
            GameObject.DestroyImmediate(target.gameObject);
            target = null;
        }

        [Test]
        public void CameraStartsAboveSurfaceAtOriginLookingDownAtDistanceOne()
        {
            var origin = new Vector3(0, 10, 0);
            var normalAtOrigin = new Vector3(0, 1, 0);
            var worldUp = new Vector3(0, 0, 1);
            surface.GetInitialPointOnSurface().Returns(origin);
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(normalAtOrigin);
            surface.GetWorldUpVector().Returns(worldUp);

            testObj.Start();

            TestUtil.AssertApproximatelyEqual(origin + (normalAtOrigin * LookAtSurfaceCamera.InitialDistanceToTarget), testObj.transform.position);
            TestUtil.AssertApproximatelyEqual(-normalAtOrigin, testObj.transform.forward);
            TestUtil.AssertApproximatelyEqual(worldUp, testObj.transform.up);
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

            testObj.Start();
            testObj.TranslateLookTargetAndDistance(pos, distance);

            TestUtil.AssertApproximatelyEqual(pos + (normal * distance), testObj.transform.position);
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
            surface.GetWorldUpVector().Returns(worldUp);

            testObj.Start();
            testObj.SetDistanceToTarget(distance);

            TestUtil.AssertApproximatelyEqual(origin + (normalAtOrigin * distance), testObj.transform.position);
            TestUtil.AssertApproximatelyEqual(-normalAtOrigin, testObj.transform.forward);
            TestUtil.AssertApproximatelyEqual(worldUp, testObj.transform.up);
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
            surface.GetWorldUpVector().Returns(worldUp);

            testObj.Start();
            testObj.SetDistanceToTarget(distance);
            testObj.TranslateLookTargetTo(newTarget);

            TestUtil.AssertApproximatelyEqual(newTarget + (normal * distance), testObj.transform.position);
            TestUtil.AssertApproximatelyEqual(-normal, testObj.transform.forward);
            TestUtil.AssertApproximatelyEqual(worldUp, testObj.transform.up);
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
            surface.GetWorldUpVector().Returns(worldUp);

            testObj.Start();
            testObj.SetDistanceToTarget(distance);
            testObj.TranslateLookTargetTo(newTarget);

            TestUtil.AssertApproximatelyEqual(newTarget + (normalAtNewTarget * distance), testObj.transform.position);
            TestUtil.AssertApproximatelyEqual(-normalAtNewTarget, testObj.transform.forward);
            //Assert.IsTrue(worldUp == testObj.transform.up);
        }

        private void AssertCameraLookingAt(Vector3 pos)
        {
            target.transform.position = pos;
            RaycastHit hitInfo;
            Assert.IsTrue(Physics.Raycast(testObj.transform.position, testObj.transform.forward, out hitInfo));
            Assert.AreEqual(target, hitInfo.collider);
        }
    }
}
