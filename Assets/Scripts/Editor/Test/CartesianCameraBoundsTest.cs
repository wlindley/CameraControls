using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class CartesianCameraBoundsTest
    {
        private CartesianCameraBounds testObj;
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
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(transform.gameObject);
            transform = null;
        }
    }
}
