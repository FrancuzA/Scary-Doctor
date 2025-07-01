using FMOD.Studio;
using UnityEngine;
using FMODUnity;
public class PlayerGroundedState : State
{
    private Rigidbody _rb;
    private Animator playerAnim;
    public PlayerGroundedState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        playerAnim = _stateMachine.GetComponent<Animator>();
        _rb = _stateMachine.GetComponent<Rigidbody>();
        Player_Manager.instance.BoyRunInstance.start();
    }
    public override void Update()
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, Player_Manager.instance.Speed);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Player_Manager.instance.BoyRunInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            playerAnim.SetTrigger("Jump");
            _stateMachine.Begin(new PlayerJumpState(_stateMachine));
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Player_Manager.instance.BoyRunInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _stateMachine.Begin(new PlayerSlideState(_stateMachine));
            playerAnim.SetTrigger("Slide");
        }
    }
}
