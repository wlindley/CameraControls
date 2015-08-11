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
        private float currentHeight;
        private Vector3 currentLook;

        private void Awake()
        {
            cam = GetComponent<LookAtSurfaceCamera>();
        }

        private void Start()
        {
            currentHeight = 0f;
            currentLook = cameraSurface.GetOrigin();
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
            currentHeight += verticalSpeed * Time.deltaTime;
            cam.SetHeight(currentHeight);
        }

        private void MoveDown()
        {
            currentHeight -= verticalSpeed * Time.deltaTime;
            cam.SetHeight(currentHeight);
        }

        private void MoveForward()
        {
            currentLook += cameraSurface.GetWorldUpVector() * horizontalSpeed * Time.deltaTime;
            cam.TranslateLookTargetTo(currentLook);
        }

        private void MoveBackward()
        {
            currentLook -= cameraSurface.GetWorldUpVector() * horizontalSpeed * Time.deltaTime;
            cam.TranslateLookTargetTo(currentLook);
        }

        private void MoveLeft()
        {
            var leftVector = Vector3.Cross(cameraSurface.GetWorldUpVector(), cameraSurface.GetNormalAtPoint(transform.position));
            currentLook += leftVector * horizontalSpeed * Time.deltaTime;
            cam.TranslateLookTargetTo(currentLook);
        }

        private void MoveRight()
        {
            var leftVector = Vector3.Cross(cameraSurface.GetWorldUpVector(), cameraSurface.GetNormalAtPoint(transform.position));
            currentLook -= leftVector * horizontalSpeed * Time.deltaTime;
            cam.TranslateLookTargetTo(currentLook);
        }
    }
}
