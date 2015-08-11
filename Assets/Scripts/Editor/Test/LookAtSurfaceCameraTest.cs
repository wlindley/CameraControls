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
            surface.GetNormalAtPoint(origin).Returns(normalAtOrigin);
            surface.GetWorldUpVector().Returns(worldUp);

            testObj.Start();

            Assert.AreEqual(origin, testObj.transform.position);
            Assert.IsTrue(-normalAtOrigin == testObj.transform.forward);
        }

        [Test]
        [Ignore("Probably not an interface we want to expose")]
        public void RotateToLookAtCausesCameraToPointTowardTarget()
        {
            target.transform.position = new Vector3(10, 10, 0);
            surface.GetWorldUpVector().Returns(Vector3.up);

            testObj.RotateToLookAt(target.transform.position);

            AssertCameraLookingAtTarget();
        }

        [Test]
        [Ignore("Probably not behavior we need")]
        public void TranslateToLookAtMaintainsOffsetBetweenCameraAndTarget()
        {
            target.transform.position = new Vector3(2, 0, 0);
            testObj.RotateToLookAt(target.transform.position);
            var difference = target.transform.position - testObj.transform.position;
            Assert.AreEqual(new Vector3(2, 0, 0), difference);

            target.transform.position = new Vector3(2, 0, 5);
            testObj.TranslateToLookAt(target.transform.position);
            Assert.AreEqual(difference, target.transform.position - testObj.transform.position);

            AssertCameraLookingAtTarget();
        }

        [Test]
        public void TranslateToLookAtMaintainsHeightAboveSurfaceAlongNormal()
        {
            var firstFaceNormal = new Vector3(1, 0, 0);
            var secondFaceNormal = new Vector3(0, 0, 1);
            var firstSurfacePoint = new Vector3(1, 0, 0);
            var secondSurfacePoint = new Vector3(0, 0, 1);
            surface.GetNormalAtPoint(firstSurfacePoint).Returns(firstFaceNormal);
            surface.GetNormalAtPoint(secondSurfacePoint).Returns(secondFaceNormal);
            surface.GetWorldUpVector().Returns(Vector3.up);

            testObj.transform.position = new Vector3(2, 0, 0);
            testObj.RotateToLookAt(new Vector3(0, 0, 0));
            testObj.SetHeightAboveSurface(firstSurfacePoint, 1);
            testObj.TranslateToLookAt(secondSurfacePoint);

            Assert.AreEqual(new Vector3(0, 0, 2), testObj.transform.position);
        }

        [Test]
        public void SetHeightAboveSurfaceMovesCameraAlongSurfaceNormal()
        {
            var normal = new Vector3(0, 1, 0);
            var pos = new Vector3(10, 0, 10);
            var height = 2f;
            surface.GetNormalAtPoint(pos).Returns(normal);

            testObj.SetHeightAboveSurface(pos, height);

            Assert.AreEqual(new Vector3(10, 2, 10), testObj.transform.position);
        }

        private void AssertCameraLookingAtTarget()
        {
            RaycastHit hitInfo;
            Assert.IsTrue(Physics.Raycast(testObj.transform.position, testObj.transform.forward, out hitInfo));
            Assert.AreEqual(target, hitInfo.collider);
        }
    }
}
