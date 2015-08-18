using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class PlaneMoverComponentTest
    {
        private PlaneMoverComponent testObj;
        private PlaneMover mover;

        [SetUp]
        public void SetUp()
        {
            mover = Substitute.For<PlaneMover>();
            PlaneMover.TestInstance = mover;

            var testObjGO = new GameObject();
            testObj = testObjGO.AddComponent<PlaneMoverComponent>();
            testObj.surface = testObjGO.AddComponent<PlaneSurfaceComponent>();

            testObj.Awake();
        }

        [TearDown]
        public void TearDown()
        {
            PlaneMover.TestInstance = null;
            GameObject.DestroyImmediate(testObj.gameObject);
            testObj = null;
        }

        [Test]
        public void MoveUpPassesThroughToPlaneMover()
        {
            var dt = GetRandomTimeDelta();
            testObj.MoveUp(dt);
            mover.Received().MoveUp(dt);
        }

        [Test]
        public void MoveDownPassesThroughToPlaneMover()
        {
            var dt = GetRandomTimeDelta();
            testObj.MoveDown(dt);
            mover.Received().MoveDown(dt);
        }

        [Test]
        public void MoveLeftPassesThroughToPlaneMover()
        {
            var dt = GetRandomTimeDelta();
            testObj.MoveLeft(dt);
            mover.Received().MoveLeft(dt);
        }

        [Test]
        public void MoveRightPassesThroughToPlaneMover()
        {
            var dt = GetRandomTimeDelta();
            testObj.MoveRight(dt);
            mover.Received().MoveRight(dt);
        }

        [Test]
        public void MoveInPassesThroughToPlaneMover()
        {
            var dt = GetRandomTimeDelta();
            testObj.MoveIn(dt);
            mover.Received().MoveIn(dt);
        }

        [Test]
        public void MoveOutPassesThroughToPlaneMover()
        {
            var dt = GetRandomTimeDelta();
            testObj.MoveOut(dt);
            mover.Received().MoveOut(dt);
        }

        private float GetRandomTimeDelta()
        {
            return Random.Range(.1f, .3f);
        }
    }
}
