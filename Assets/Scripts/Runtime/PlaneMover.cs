using UnityEngine;

namespace CamCon
{
    public class PlaneMover : LookAtSurfaceCameraMover
    {
        private LookAtSurfaceCamera camera;
        private PlaneSurface surface;
        private float zoomSpeed;
        private float panSpeed;

        public PlaneMover(LookAtSurfaceCamera camera, PlaneSurface surface, float zoomSpeed, float panSpeed)
        {
            this.camera = camera;
            this.surface = surface;
            this.zoomSpeed = zoomSpeed;
            this.panSpeed = panSpeed;
        }

        public void MoveLeft(float dt)
        {
            var lookTarget = camera.GetLookTarget();
            var left = Vector3.Cross(surface.GetNormalAtPoint(lookTarget), surface.GetWorldUpVector());
            MoveLookTargetBy(left * panSpeed * dt);
        }

        public void MoveRight(float dt)
        {
            var lookTarget = camera.GetLookTarget();
            var left = Vector3.Cross(surface.GetNormalAtPoint(lookTarget), surface.GetWorldUpVector());
            MoveLookTargetBy(-left * panSpeed * dt);
        }

        public void MoveUp(float dt)
        {
            var up = surface.GetWorldUpVector();
            MoveLookTargetBy(up * panSpeed * dt);
        }

        public void MoveDown(float dt)
        {
            var up = surface.GetWorldUpVector();
            MoveLookTargetBy(-up * panSpeed * dt);
        }

        public void MoveIn(float dt)
        {
            IncreaseDistanceBy(-zoomSpeed * dt);
        }

        public void MoveOut(float dt)
        {
            IncreaseDistanceBy(zoomSpeed * dt);
        }

        private void MoveLookTargetBy(Vector3 delta)
        {
            var lookTarget = camera.GetLookTarget();
            camera.TranslateLookTargetTo(lookTarget + delta);
        }

        private void IncreaseDistanceBy(float delta)
        {
            var distance = camera.GetDistanceToTarget();
            distance += delta;
            camera.SetDistanceToTarget(distance);
        }
    }
}
