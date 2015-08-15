using UnityEngine;

namespace CamCon
{
    [RequireComponent(typeof(LookAtSurfaceCamera))]
    public class LookAtSurfaceCameraDebugger : MonoBehaviour
    {
        [Tooltip("Must be the same Surface this camera is connected to.")]
        public SurfaceComponent cameraSurface;
        public float verticalSpeed = 50f;
        public float horizontalSpeed = 50f;

        private LookAtSurfaceCamera cam;

        private void Awake()
        {
            cam = GetComponent<LookAtSurfaceCamera>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
                MoveUp();
            if (Input.GetKey(KeyCode.DownArrow))
                MoveDown();
            if (Input.GetKey(KeyCode.W))
                MoveForward();
            if (Input.GetKey(KeyCode.S))
                MoveBackward();
            if (Input.GetKey(KeyCode.A))
                MoveLeft();
            if (Input.GetKey(KeyCode.D))
                MoveRight();
        }

        private void MoveUp()
        {
            var currentHeight = cam.GetDistanceToTarget();
            currentHeight += verticalSpeed * Time.deltaTime;
            cam.SetDistanceToTarget(currentHeight);
        }

        private void MoveDown()
        {
            var currentHeight = cam.GetDistanceToTarget();
            currentHeight -= verticalSpeed * Time.deltaTime;
            cam.SetDistanceToTarget(currentHeight);
        }

        private void MoveForward()
        {
            var currentLook = cam.GetLookTarget();
            currentLook += cameraSurface.GetWorldUpVector() * horizontalSpeed * Time.deltaTime;
            cam.TranslateLookTargetTo(currentLook);
        }

        private void MoveBackward()
        {
            var currentLook = cam.GetLookTarget();
            currentLook -= cameraSurface.GetWorldUpVector() * horizontalSpeed * Time.deltaTime;
            cam.TranslateLookTargetTo(currentLook);
        }

        private void MoveLeft()
        {
            var currentLook = cam.GetLookTarget();
            var leftVector = Vector3.Cross(cameraSurface.GetWorldUpVector(), cameraSurface.GetNormalAtPoint(transform.position));
            currentLook += leftVector * horizontalSpeed * Time.deltaTime;
            cam.TranslateLookTargetTo(currentLook);
        }

        private void MoveRight()
        {
            var currentLook = cam.GetLookTarget();
            var leftVector = Vector3.Cross(cameraSurface.GetWorldUpVector(), cameraSurface.GetNormalAtPoint(transform.position));
            currentLook -= leftVector * horizontalSpeed * Time.deltaTime;
            cam.TranslateLookTargetTo(currentLook);
        }
    }
}
