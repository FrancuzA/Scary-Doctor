using UnityEngine;

public class PlayerJumpState : State
{
    private Rigidbody _rb;
    private float _timer;
    private Animator playerAnim;

    public PlayerJumpState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        playerAnim = _stateMachine.GetComponent<Animator>();
        _rb = _stateMachine.GetComponent<Rigidbody>();
        _rb.AddForce(Vector3.up * 6f, ForceMode.Impulse);
        _rb.AddForce(Vector3.forward *3f, ForceMode.Impulse);
    }

    public override void Update()
    {
        if (_timer < 0.2f)
        {
            _timer += Time.deltaTime;

            return;
        }

        if (Physics.Raycast(_stateMachine.transform.position, Vector3.down, 0.3f, Player_Movement.instance.obstacle))
        {
            playerAnim.SetTrigger("Land");
            Exit();
        }
        Debug.DrawRay(_stateMachine.transform.position, Vector3.down,Color.red, 0.3f);
    }

    public override void Exit()
    {
        _stateMachine.Begin(new PlayerGroundedState(_stateMachine));
    }
}
