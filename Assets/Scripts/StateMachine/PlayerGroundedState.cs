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
        Vector3 newPosition = _rb.position + Vector3.forward * Player_Manager.instance.Speed * Time.deltaTime;
        _rb.MovePosition(newPosition);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Player_Manager.instance.BoyRunInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            playerAnim.SetTrigger("Jump");
            _stateMachine.Begin(new PlayerJumpState(_stateMachine));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Player_Manager.instance.BoyRunInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _stateMachine.Begin(new PlayerSlideState(_stateMachine));
            playerAnim.SetTrigger("Slide");
        }
    }
}
