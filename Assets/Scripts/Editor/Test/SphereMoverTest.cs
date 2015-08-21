using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class SphereMoverTest
    {
        private SphereMover testObj;
        private LookAtSurfaceCamera camera;
        private Transform transform;
        private SphereSurface surface;
        private Vector3 surfaceOrigin;
        private Vector3 surfaceUp;
        private float surfaceRadius;
        private float zoomSpeed;
        private float rotateSpeed;

        private float initialCameraDistance;
        private float timeDelta;

        [SetUp]
        public void SetUp()
        {
            surfaceOrigin = Vector3.zero;
            surfaceUp = Vector3.up;
            surfaceRadius = 10f;
            surface = new SphereSurface(surfaceOrigin, surfaceRadius, surfaceUp);

            initialCameraDistance = 100f;
            initialLookTarget = surface.GetInitialPointOnSurface();
            timeDelta = .25f;

            var go = new GameObject();
            transform = go.transform;
            camera = new LookAtSurfaceCamera(transform, surface);
            camera.InitializeCamera();
            camera.SetDistanceToTarget(initialCameraDistance);

            zoomSpeed = 10f;
            rotateSpeed = Mathf.PI;
            testObj = new SphereMover(camera, surface, zoomSpeed, rotateSpeed);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(transform.gameObject);
            transform = null;
        }

        [Test]
        public void MoveInDecreasesCameraDistanceBySpeedTimesTimeDelta()
        {
            var expectedDelta = -zoomSpeed * timeDelta;
            testObj.MoveIn(timeDelta);
            TestUtil.AssertApproximatelyEqual(initialCameraDistance + expectedDelta, camera.GetDistanceToTarget());
        }

        [Test]
        public void MoveOutIncreasesCameraDistanceBySpeedTimesTimeDelta()
        {
            var expectedDelta = zoomSpeed * timeDelta;
            testObj.MoveOut(timeDelta);
            TestUtil.AssertApproximatelyEqual(initialCameraDistance + expectedDelta, camera.GetDistanceToTarget());
        }

        [Test]
        public void MoveRightRotatesCameraAroundUpAxisBySpeedTimesTimeDelta()
        {
            var azimuthDelta = rotateSpeed * timeDelta;
            var expectedLookTarget = surfaceOrigin + new Vector3(surfaceRadius * Mathf.Cos(azimuthDelta), 0f, surfaceRadius * Mathf.Sin(azimuthDelta));
            testObj.MoveRight(timeDelta);
            TestUtil.AssertApproximatelyEqual(expectedLookTarget, camera.GetLookTarget());
        }

        [Test]
        public void MoveRightTwiceRotatesCameraAroundUpAxisByTwiceSpeedTimesTimeDelta()
        {
            var azimuthDelta = rotateSpeed * timeDelta * 2f;
            var expectedLookTarget = surfaceOrigin + new Vector3(surfaceRadius * Mathf.Cos(azimuthDelta), 0f, surfaceRadius * Mathf.Sin(azimuthDelta));
            testObj.MoveRight(timeDelta);
            testObj.MoveRight(timeDelta);
            TestUtil.AssertApproximatelyEqual(expectedLookTarget, camera.GetLookTarget());
        }

        [Test]
        public void MoveLeftRotatesCameraAroundUpAxisByNegativeSpeedTimesTimeDelta()
        {
            var azimuthDelta = -rotateSpeed * timeDelta;
            var expectedLookTarget = surfaceOrigin + new Vector3(surfaceRadius * Mathf.Cos(azimuthDelta), 0f, surfaceRadius * Mathf.Sin(azimuthDelta));
            testObj.MoveLeft(timeDelta);
            TestUtil.AssertApproximatelyEqual(expectedLookTarget, camera.GetLookTarget());
        }

        [Test]
        public void MoveUpRotatesCameraAroundLeftAxisBySpeedTimesTimeDelta()
        {
            //TODO: this test is confusing and needs to be fixed so it makes sense
            var zenithDelta = rotateSpeed * timeDelta;
            var expectedLookTarget = surfaceOrigin + new Vector3(
                surfaceRadius * Mathf.Cos(0f) * Mathf.Sin(zenithDelta),
                surfaceRadius * Mathf.Cos(zenithDelta),
                surfaceRadius * Mathf.Sin(0f) * Mathf.Sin(zenithDelta));
            testObj.MoveUp(timeDelta);
            TestUtil.AssertApproximatelyEqual(expectedLookTarget, camera.GetLookTarget());
        }

        [Test]
        public void MoveDownRotatesCameraAroundLeftAxisByNegativeSpeedTimesTimeDelta()
        {
            //TODO: this test is confusing and needs to be fixed so it makes sense
            var zenithDelta = -rotateSpeed * timeDelta;
            var expectedLookTarget = surfaceOrigin + new Vector3(
                -surfaceRadius * Mathf.Cos(0f) * Mathf.Sin(zenithDelta),
                -surfaceRadius * Mathf.Cos(zenithDelta),
                surfaceRadius * Mathf.Sin(0f) * Mathf.Sin(zenithDelta));
            testObj.MoveDown(timeDelta);
            TestUtil.AssertApproximatelyEqual(expectedLookTarget, camera.GetLookTarget());
        }

        [Test]
        public void RotatingAzimuthAndZenithHasExpectedResult()
        {
            var azimuthDelta = rotateSpeed * timeDelta * 2f;
            var zenithDelta = rotateSpeed * timeDelta;
            var expectedLookTarget = surfaceOrigin + new Vector3(
                surfaceRadius * Mathf.Cos(azimuthDelta) * Mathf.Sin(zenithDelta),
                surfaceRadius * Mathf.Cos(zenithDelta),
                surfaceRadius * Mathf.Sin(azimuthDelta) * Mathf.Sin(zenithDelta));

            testObj.MoveRight(timeDelta);
            testObj.MoveUp(timeDelta);
            testObj.MoveRight(timeDelta);

            TestUtil.AssertApproximatelyEqual(expectedLookTarget, camera.GetLookTarget());
        }
    }
}
