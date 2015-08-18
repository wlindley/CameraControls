namespace CamCon
{
    public interface LookAtSurfaceCameraMover
    {
        void MoveLeft(float dt);
        void MoveRight(float dt);
        void MoveUp(float dt);
        void MoveDown(float dt);
        void MoveIn(float dt);
        void MoveOut(float dt);
    }
}
