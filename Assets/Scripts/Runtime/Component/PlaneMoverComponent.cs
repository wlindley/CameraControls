using UnityEngine;

namespace CamCon
{
    public class PlaneMoverComponent : MonoBehaviour, LookAtSurfaceCameraMover
    {
        public LookAtSurfaceCamera cam;
        public PlaneSurfaceComponent surface;
        public float zoomSpeed;
        public float panSpeed;

        private PlaneMover mover;

        public void Awake()
        {
            var planeSurface = surface.GetSurface() as PlaneSurface;
            mover = PlaneMover.GetInstance(cam, planeSurface, zoomSpeed, panSpeed);
        }

        public void MoveLeft(float dt)
        {
            mover.MoveLeft(dt);
        }

        public void MoveRight(float dt)
        {
            mover.MoveRight(dt);
        }

        public void MoveUp(float dt)
        {
            mover.MoveUp(dt);
        }

        public void MoveDown(float dt)
        {
            mover.MoveDown(dt);
        }

        public void MoveIn(float dt)
        {
            mover.MoveIn(dt);
        }

        public void MoveOut(float dt)
        {
            mover.MoveOut(dt);
        }
    }
}
