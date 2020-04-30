public interface IPlayable
{
    void Move(float direction);
    void Jump(float power);
    void AirJump();
    bool IsGrounded();
    bool WasGrounded();
}