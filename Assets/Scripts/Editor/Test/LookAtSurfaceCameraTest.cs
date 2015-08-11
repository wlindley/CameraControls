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
            target.radius = 1f;
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
        public void CameraStartsAboveSurfaceOriginLookingDownAtHeightZero()
        {
            var origin = new Vector3(0, 10, 0);
            var normalAtOrigin = new Vector3(0, 1, 0);
            var worldUp = new Vector3(0, 0, 1);
            surface.GetOrigin().Returns(origin);
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(normalAtOrigin);
            surface.GetWorldUpVector().Returns(worldUp);

            testObj.Start();

            Assert.AreEqual(origin, testObj.transform.position);
            Assert.IsTrue(-normalAtOrigin == testObj.transform.forward);
            Assert.IsTrue(worldUp == testObj.transform.up);
        }

        [Test]
        public void TranslateLookTargetWithHeightMovesCameraAlongSurfaceNormal()
        {
            var normal = new Vector3(0, 1, 0);
            var pos = new Vector3(10, 0, 10);
            var height = 2f;
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(normal);

            testObj.Start();
            testObj.TranslateLookTargetWithHeight(pos, height);

            Assert.AreEqual(pos + (normal * height), testObj.transform.position);
        }

        [Test]
        public void SetHeightMovesCameraAlongSurfaceNormal()
        {
            var origin = new Vector3(0, 10, 0);
            var normalAtOrigin = new Vector3(0, 1, 0);
            var worldUp = new Vector3(0, 0, 1);
            var height = 5f;
            surface.GetOrigin().Returns(origin);
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(normalAtOrigin);
            surface.GetWorldUpVector().Returns(worldUp);

            testObj.Start();
            testObj.SetHeight(height);

            Assert.AreEqual(origin + (normalAtOrigin * height), testObj.transform.position);
            Assert.IsTrue(-normalAtOrigin == testObj.transform.forward);
            Assert.IsTrue(worldUp == testObj.transform.up);
        }

        [Test]
        public void TranslateLookTargetToMaintainsHeight()
        {
            var origin = new Vector3(0, 10, 0);
            var newTarget = new Vector3(10, 10, 10);
            var normal = new Vector3(0, 1, 0);
            var worldUp = new Vector3(0, 0, 1);
            var height = 5f;
            surface.GetOrigin().Returns(origin);
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(normal);
            surface.GetWorldUpVector().Returns(worldUp);

            testObj.Start();
            testObj.SetHeight(height);
            testObj.TranslateLookTargetTo(newTarget);

            Assert.AreEqual(newTarget + (normal * height), testObj.transform.position);
            Assert.IsTrue(-normal == testObj.transform.forward);
            Assert.IsTrue(worldUp == testObj.transform.up);
        }

        private void AssertCameraLookingAtTarget()
        {
            RaycastHit hitInfo;
            Assert.IsTrue(Physics.Raycast(testObj.transform.position, testObj.transform.forward, out hitInfo));
            Assert.AreEqual(target, hitInfo.collider);
        }
    }
}
