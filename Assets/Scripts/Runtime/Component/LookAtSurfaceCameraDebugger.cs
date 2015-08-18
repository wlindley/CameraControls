using UnityEngine;

namespace CamCon
{
    public class LookAtSurfaceCameraDebugger : MonoBehaviour
    {
        public LookAtSurfaceCameraMover mover;

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
            mover.MoveOut(Time.deltaTime);
        }

        private void MoveDown()
        {
            mover.MoveIn(Time.deltaTime);
        }

        private void MoveForward()
        {
            mover.MoveUp(Time.deltaTime);
        }

        private void MoveBackward()
        {
            mover.MoveDown(Time.deltaTime);
        }

        private void MoveLeft()
        {
            mover.MoveLeft(Time.deltaTime);
        }

        private void MoveRight()
        {
            mover.MoveRight(Time.deltaTime);
        }
    }
}
