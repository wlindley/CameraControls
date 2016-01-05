using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class AABoxCameraBoundsTest
    {
        private AABoxCameraBounds testObj;
        private Vector3 boundsCenter;
        private Vector3 boundsSize;
        private PlaneSurface surface;
        private Vector3 surfaceOrigin;
        private Vector3 surfaceNormal;
        private Vector3 surfaceUp;
        private LookAtSurfaceCamera camera;
        private Transform transform;

        [SetUp]
        public void SetUp()
        {
            surfaceOrigin = Vector3.zero;
            surfaceNormal = Vector3.up;
            surfaceUp = Vector3.forward;
            surface = new PlaneSurface(surfaceOrigin, surfaceNormal, surfaceUp);

            var cameraGO = new GameObject();
            transform = cameraGO.transform;
            camera = new LookAtSurfaceCamera(transform, surface);

            boundsCenter = new Vector3(10f, 5f, 10f);
            boundsSize = new Vector3(5f, 5f, 5f);
            testObj = new AABoxCameraBounds(surface, boundsCenter, boundsSize);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(transform.gameObject);
            transform = null;
        }

        [Test]
        public void ClampCameraToBoundsDoesNothingIfCameraIsInsideBounds()
        {
            PositionCameraAtCenterOfBounds();
            var expectedPosition = transform.position;
            var expectedForward = transform.forward;
            testObj.ClampCameraToBounds(camera);
            TestUtil.AssertApproximatelyEqual(expectedPosition, transform.position);
            TestUtil.AssertApproximatelyEqual(expectedForward, transform.forward);
        }

        [Test]
        public void ClampCameraToBoundsDecreasesCameraHeightUntilCameraIsInBounds()
        {
            PositionCameraAtCenterOfBounds();
            var expectedPosition = transform.position + new Vector3(0f, boundsSize.y * .5f, 0f);
            var expectedForward = transform.forward;
            camera.SetDistanceToTarget(camera.GetDistanceToTarget() + 10f);

            testObj.ClampCameraToBounds(camera);

            TestUtil.AssertApproximatelyEqual(expectedPosition, transform.position);
            TestUtil.AssertApproximatelyEqual(expectedForward, transform.forward);
        }

        [Test]
        public void ClampCameraToBoundsIncreasesCameraHeightUntilCameraIsInBounds()
        {
            PositionCameraAtCenterOfBounds();
            var expectedPosition = transform.position - new Vector3(0f, boundsSize.y * .5f, 0f);
            var expectedForward = transform.forward;
            camera.SetDistanceToTarget(camera.GetDistanceToTarget() - 10f);

            testObj.ClampCameraToBounds(camera);

            TestUtil.AssertApproximatelyEqual(expectedPosition, transform.position);
            TestUtil.AssertApproximatelyEqual(expectedForward, transform.forward);
        }

        [Test]
        public void ClampCameraToBoundsMovesCameraLeftUntilCameraIsInBounds()
        {
            PositionCameraAtCenterOfBounds();
            var expectedPosition = transform.position - new Vector3(boundsSize.x * .5f, 0f, 0f);
            var expectedForward = transform.forward;
            camera.TranslateLookTargetTo(camera.GetLookTarget() - new Vector3(10f, 0f, 0f));

            testObj.ClampCameraToBounds(camera);

            TestUtil.AssertApproximatelyEqual(expectedPosition, transform.position);
            TestUtil.AssertApproximatelyEqual(expectedForward, transform.forward);
        }

        private void PositionCameraAtCenterOfBounds()
        {
            camera.TranslateLookTargetAndDistance(new Vector3(boundsCenter.x, 0f, boundsCenter.z), boundsCenter.y);
        }
    }
}
