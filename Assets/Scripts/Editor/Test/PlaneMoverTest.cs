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

        private float initialCameraDistance;
        private Vector3 initialLookTarget;
        private float timeDelta;

        [SetUp]
        public void SetUp()
        {
            surfaceOrigin = Vector3.zero;
            surfaceNormal = Vector3.up;
            surfaceUp = Vector3.forward;
            surface = new PlaneSurface(surfaceOrigin, surfaceNormal, surfaceUp);

            initialCameraDistance = 100f;
            initialLookTarget = Vector3.zero;
            timeDelta = .25f;
            camera = Substitute.For<LookAtSurfaceCamera>();
            camera.GetDistanceToTarget().Returns(initialCameraDistance);
            camera.GetLookTarget().Returns(initialLookTarget);

            zoomSpeed = 5f;
            panSpeed = 10f;
            testObj = new PlaneMover(camera, surface, zoomSpeed, panSpeed);
        }

        [Test]
        public void MoveInDecreasesCameraDistanceBySpeedTimesTimeDelta()
        {
            var expectedDelta = -zoomSpeed * timeDelta;
            testObj.MoveIn(timeDelta);
            camera.Received().SetDistanceToTarget(initialCameraDistance + expectedDelta);
        }

        [Test]
        public void MoveOutIncreasesCameraDistanceBySpeedTimesTimeDelta()
        {
            var expectedDelta = zoomSpeed * timeDelta;
            testObj.MoveOut(timeDelta);
            camera.Received().SetDistanceToTarget(initialCameraDistance + expectedDelta);
        }

        [Test]
        public void MoveUpMovesCamerasLookTargetAlongUpVectorBySpeedTimesTimeDelta()
        {
            var expectedDelta = surfaceUp * panSpeed * timeDelta;
            testObj.MoveUp(timeDelta);
            camera.Received().TranslateLookTargetTo(initialLookTarget + expectedDelta);
        }

        [Test]
        public void MoveDownMovesCamerasLookTargetAlongUpVectorBySpeedTimesTimeDelta()
        {
            var expectedDelta = -surfaceUp * panSpeed * timeDelta;
            testObj.MoveDown(timeDelta);
            camera.Received().TranslateLookTargetTo(initialLookTarget + expectedDelta);
        }

        [Test]
        public void MoveLeftMovesCamerasLookTargetAlongRightVectorBySpeedTimesTimeDelta()
        {
            var rightVector = Vector3.Cross(surfaceNormal, surfaceUp);
            var expectedDelta = -rightVector * panSpeed * timeDelta;
            testObj.MoveLeft(timeDelta);
            camera.Received().TranslateLookTargetTo(initialLookTarget + expectedDelta);
        }

        [Test]
        public void MoveRightMovesCamerasLookTargetAlongRightVectorBySpeedTimesTimeDelta()
        {
            var rightVector = Vector3.Cross(surfaceNormal, surfaceUp);
            var expectedDelta = rightVector * panSpeed * timeDelta;
            testObj.MoveRight(timeDelta);
            camera.Received().TranslateLookTargetTo(initialLookTarget + expectedDelta);
        }
    }
}
