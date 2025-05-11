using UnityEngine;

public class PlayerGroundedState : State
{
    private Rigidbody _rb;
    private Animator playerAnim;
    public PlayerGroundedState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        playerAnim = _stateMachine.GetComponent<Animator>();
        _rb = _stateMachine.GetComponent<Rigidbody>();
    }
    public override void Update()
    {
        Vector3 newPosition = _rb.position + Vector3.forward * Player_Movement.instance.Speed * Time.deltaTime;
        _rb.MovePosition(newPosition);

        if (Input.GetKeyDown(KeyCode.W))
        {
            _stateMachine.Begin(new PlayerJumpState(_stateMachine));
            playerAnim.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _stateMachine.Begin(new PlayerSlideState(_stateMachine));
            playerAnim.SetTrigger("Slide");
        }
    }
}
