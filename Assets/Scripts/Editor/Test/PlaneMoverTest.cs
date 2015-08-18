using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class PlaneMoverTest
    {
        private PlaneMover testObj;
        private LookAtSurfaceCamera camera;
        private PlaneSurface surface;
        private Vector3 surfaceOrigin;
        private Vector3 surfaceNormal;
        private Vector3 surfaceUp;
        private float zoomSpeed;
        private float panSpeed;

        [SetUp]
        public void SetUp()
        {
            surfaceOrigin = Vector3.zero;
            surfaceNormal = Vector3.up;
            surfaceUp = Vector3.forward;
            surface = new PlaneSurface(surfaceOrigin, surfaceNormal, surfaceUp);

            var cameraGO = new GameObject();
            camera = cameraGO.AddComponent<LookAtSurfaceCamera>();
            camera.Surface = surface;
            camera.Start();
            camera.SetDistanceToTarget(100f);

            zoomSpeed = 5f;
            panSpeed = 10f;
            testObj = new PlaneMover(camera, surface, zoomSpeed, panSpeed);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(camera.gameObject);
            camera = null;
        }

        [Test]
        public void MoveInDecreasesCameraDistanceBySpeedTimesTimeDelta()
        {
            var initialDistance = camera.GetDistanceToTarget();
            var timeDelta = .25f;

            testObj.MoveIn(timeDelta);

            var finalDistance = camera.GetDistanceToTarget();
            var expectedDelta = -zoomSpeed * timeDelta;
            Assert.AreEqual(expectedDelta, finalDistance - initialDistance, float.Epsilon);
        }

        [Test]
        public void MoveOutIncreasesCameraDistanceBySpeedTimesTimeDelta()
        {
            var initialDistance = camera.GetDistanceToTarget();
            var timeDelta = .25f;

            testObj.MoveOut(timeDelta);

            var finalDistance = camera.GetDistanceToTarget();
            var expectedDelta = zoomSpeed * timeDelta;
            TestUtil.AssertApproximatelyEqual(expectedDelta, finalDistance - initialDistance);
        }

        [Test]
        public void MoveUpMovesCamerasLookTargetAlongUpVectorBySpeedTimesTimeDelta()
        {
            var initialTarget = camera.GetLookTarget();
            var timeDelta = .25f;

            testObj.MoveUp(timeDelta);

            var finalTarget = camera.GetLookTarget();
            var expectedDelta = surfaceOrigin + (surfaceUp * panSpeed * timeDelta);
            TestUtil.AssertApproximatelyEqual(expectedDelta, finalTarget - initialTarget);
        }

        [Test]
        public void MoveDownMovesCamerasLookTargetAlongUpVectorBySpeedTimesTimeDelta()
        {
            var initialTarget = camera.GetLookTarget();
            var timeDelta = .25f;

            testObj.MoveDown(timeDelta);

            var finalTarget = camera.GetLookTarget();
            var expectedDelta = surfaceOrigin - (surfaceUp * panSpeed * timeDelta);
            TestUtil.AssertApproximatelyEqual(expectedDelta, finalTarget - initialTarget);
        }

        [Test]
        public void MoveLeftMovesCamerasLookTargetAlongLeftVectorBySpeedTimesTimeDelta()
        {
            var initialTarget = camera.GetLookTarget();
            var timeDelta = .25f;

            testObj.MoveLeft(timeDelta);

            var finalTarget = camera.GetLookTarget();
            var leftVector = Vector3.Cross(surfaceNormal, surfaceUp);
            var expectedDelta = surfaceOrigin + (leftVector * panSpeed * timeDelta);
            TestUtil.AssertApproximatelyEqual(expectedDelta, finalTarget - initialTarget);
        }

        [Test]
        public void MoveRightMovesCamerasLookTargetAlongLeftVectorBySpeedTimesTimeDelta()
        {
            var initialTarget = camera.GetLookTarget();
            var timeDelta = .25f;

            testObj.MoveRight(timeDelta);

            var finalTarget = camera.GetLookTarget();
            var leftVector = Vector3.Cross(surfaceNormal, surfaceUp);
            var expectedDelta = surfaceOrigin - (leftVector * panSpeed * timeDelta);
            TestUtil.AssertApproximatelyEqual(expectedDelta, finalTarget - initialTarget);
        }
    }
}
