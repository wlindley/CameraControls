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
        public void LookAtCausesObjectToPointTowardTarget()
        {
            target.transform.position = new Vector3(10, 10, 0);
            surface.GetWorldUpVector().Returns(Vector3.up);

            testObj.LookAt(target.transform.position);

            RaycastHit hitInfo;
            Assert.IsTrue(Physics.Raycast(testObj.transform.position, testObj.transform.forward, out hitInfo));
            Assert.AreEqual(target, hitInfo.collider);
        }

        [Test]
        public void SetHeightAboveSurfaceMovesObjectAlongSurfaceNormal()
        {
            var normal = new Vector3(0, 1, 0);
            var pos = new Vector3(10, 0, 10);
            var height = 2f;
            surface.GetNormalAtPoint(pos).Returns(normal);

            testObj.SetHeightAboveSurface(pos, height);

            Assert.AreEqual(new Vector3(10, 2, 10), testObj.transform.position);
        }
    }
}
