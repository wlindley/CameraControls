using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class PlaneSurfaceComponentTest
    {
        private PlaneSurfaceComponent testObj;
        private PlaneSurface surface;
        private Vector3 testObjPos;

        [SetUp]
        public void SetUp()
        {
            surface = Substitute.For<PlaneSurface>();
            PlaneSurface.TestInstance = surface;

            testObjPos = new Vector3(50, 50, 50);

            var testObjGO = new GameObject();
            testObjGO.transform.position = testObjPos;

            testObj = testObjGO.AddComponent<PlaneSurfaceComponent>();

            testObj.Awake();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(testObj.gameObject);
            testObj = null;
            PlaneSurface.TestInstance = null;
        }

        private Vector3 GetRandomPoint()
        {
            return new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        }

        [Test]
        public void GetInitialPointOnSurfacePassesThroughToWrappedInstance()
        {
            surface.GetInitialPointOnSurface().Returns(testObjPos);
            TestUtil.AssertApproximatelyEqual(testObjPos, testObj.GetInitialPointOnSurface());
        }

        [Test]
        public void GetNormalAtPointPassesThroughToWrappedInstance()
        {
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(Vector3.forward);
            var pos = GetRandomPoint();
            TestUtil.AssertApproximatelyEqual(Vector3.forward, testObj.GetNormalAtPoint(pos));
        }

        [Test]
        public void GetWorldUpVectorPassesThroughToWrappedInstance()
        {
            surface.GetWorldUpVector().Returns(Vector3.up);
            TestUtil.AssertApproximatelyEqual(Vector3.up, testObj.GetWorldUpVector());
        }

        [Test]
        public void ClampPointToSurfacePassesThroughToWrappedInstance()
        {
            var expectedClampedPoint = GetRandomPoint();
            surface.ClampPointToSurface(Arg.Any<Vector3>()).Returns(expectedClampedPoint);
            var pos = GetRandomPoint();
            TestUtil.AssertApproximatelyEqual(expectedClampedPoint, testObj.ClampPointToSurface(pos));
        }
    }
}
