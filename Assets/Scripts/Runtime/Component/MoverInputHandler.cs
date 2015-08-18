using UnityEngine;

namespace CamCon
{
    public class MoverInputHandler : MonoBehaviour
    {
        public MoverComponent mover;
        private LookAtSurfaceCameraMover cameraMover;

        private void Awake()
        {
            cameraMover = mover.GetMover();
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
            cameraMover.MoveOut(Time.deltaTime);
        }

        private void MoveDown()
        {
            cameraMover.MoveIn(Time.deltaTime);
        }

        private void MoveForward()
        {
            cameraMover.MoveUp(Time.deltaTime);
        }

        private void MoveBackward()
        {
            cameraMover.MoveDown(Time.deltaTime);
        }

        private void MoveLeft()
        {
            cameraMover.MoveLeft(Time.deltaTime);
        }

        private void MoveRight()
        {
            cameraMover.MoveRight(Time.deltaTime);
        }
    }
}
