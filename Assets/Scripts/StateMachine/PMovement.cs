
public class PMovement : StateMachine
{
    private void Start()
    {
        Begin(new PlayerGroundedState(this));
    }
}
