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
            camera.TranslateLookTargetTo(lookTarget + (left * panSpeed * dt));
        }

        public void MoveRight(float dt)
        {
            var lookTarget = camera.GetLookTarget();
            var left = Vector3.Cross(surface.GetNormalAtPoint(lookTarget), surface.GetWorldUpVector());
            camera.TranslateLookTargetTo(lookTarget - (left * panSpeed * dt));
        }

        public void MoveUp(float dt)
        {
            var lookTarget = camera.GetLookTarget();
            var up = surface.GetWorldUpVector();
            camera.TranslateLookTargetTo(lookTarget + (up * panSpeed * dt));
        }

        public void MoveDown(float dt)
        {
            var lookTarget = camera.GetLookTarget();
            var up = surface.GetWorldUpVector();
            camera.TranslateLookTargetTo(lookTarget - (up * panSpeed * dt));
        }

        public void MoveIn(float dt)
        {
            var distance = camera.GetDistanceToTarget();
            distance -= zoomSpeed * dt;
            camera.SetDistanceToTarget(distance);
        }

        public void MoveOut(float dt)
        {
            var distance = camera.GetDistanceToTarget();
            distance += zoomSpeed * dt;
            camera.SetDistanceToTarget(distance);
        }
    }
}
