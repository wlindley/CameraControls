using UnityEngine;

namespace CamCon
{
    public class SphereMover : LookAtSurfaceCameraMover
    {
        private LookAtSurfaceCamera camera;
        private SphereSurface surface;
        private float zoomSpeed;
        private float rotateSpeed;

        public SphereMover(LookAtSurfaceCamera camera, SphereSurface surface, float zoomSpeed, float rotateSpeed)
        {
            this.camera = camera;
            this.surface = surface;
            this.zoomSpeed = zoomSpeed;
            this.rotateSpeed = rotateSpeed;
        }

        public void MoveLeft(float dt)
        {
            RotateBy(-rotateSpeed * dt, 0f);
        }

        public void MoveRight(float dt)
        {
            RotateBy(rotateSpeed * dt, 0f);
        }

        public void MoveUp(float dt)
        {
            RotateBy(0f, -rotateSpeed * dt);
        }

        public void MoveDown(float dt)
        {
            RotateBy(0f, rotateSpeed * dt);
        }

        public void MoveIn(float dt)
        {
            IncreaseDistanceBy(-zoomSpeed * dt);
        }

        public void MoveOut(float dt)
        {
            IncreaseDistanceBy(zoomSpeed * dt);
        }

        private void IncreaseDistanceBy(float distanceDelta)
        {
            var distance = camera.GetDistanceToTarget();
            distance += distanceDelta;
            camera.SetDistanceToTarget(distance);
        }

        /*
         * Azimuth is the rotation around the up axis relative to right axis.
         * Zenith is the angle between the up vector and the position.
         * */
        private void RotateBy(float azimuthDelta, float zenithDelta)
        {
            var azimuth = GetAzimuth();
            var zenith = GetZenith();
            azimuth += azimuthDelta;
            zenith += zenithDelta;
            SetRotation(azimuth, zenith);
        }

        private float GetAzimuth()
        {
            var diff = camera.GetLookTarget() - surface.Origin;
            return Mathf.Atan2(diff.z, diff.x);
        }

        private void SetRotation(float azimuth, float zenith)
        {
            var radius = surface.Radius;
            var delta = new Vector3(
                radius * Mathf.Cos(azimuth) * Mathf.Sin(zenith),
                radius * Mathf.Cos(zenith),
                radius * Mathf.Sin(azimuth) * Mathf.Sin(zenith));
            camera.TranslateLookTargetTo(surface.Origin + delta);
        }

        private void RotateZenithBy(float angleDelta)
        {
            var zenith = GetZenith();
            zenith += angleDelta;
            SetZenith(zenith);
        }

        private float GetZenith()
        {
            var diff = camera.GetLookTarget() - surface.Origin;
            return Mathf.Acos(diff.y / surface.Radius);
        }

        private void SetZenith(float zenith)
        {
            var delta = new Vector3(0f, surface.Radius * Mathf.Cos(zenith), surface.Radius * Mathf.Sin(zenith));
            camera.TranslateLookTargetTo(surface.Origin + delta);
        }
    }
}
