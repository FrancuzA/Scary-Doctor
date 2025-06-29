using FMOD.Studio;
using UnityEngine;
using FMODUnity;
public class PlayerJumpState : State
{
    private Rigidbody _rb;
    private float _timer;
    private Animator playerAnim;

    public PlayerJumpState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Player_Manager.instance.JumpStartInstance.start();
        playerAnim = _stateMachine.GetComponent<Animator>();
        _rb = _stateMachine.GetComponent<Rigidbody>();
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 5, _rb.linearVelocity.z);
    }

    public override void Update()
    {
        if (_timer < 0.2f)
        {
            _timer += Time.deltaTime;

            return;
        }
        if (Physics.Raycast(_stateMachine.transform.position, Vector3.down, 0.3f, Player_Manager.instance.obstacle))
        {
            playerAnim.SetTrigger("Land");
            Player_Manager.instance.JumpEndInstance.start();
            Exit();
        }
    }

    public override void Exit()
    {
        _stateMachine.Begin(new PlayerGroundedState(_stateMachine));
    }
}
