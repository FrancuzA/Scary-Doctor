using FMOD.Studio;
using UnityEngine;
using FMODUnity;
public class PlayerSlideState : State
{
    private Rigidbody playerRigidbody;
    private float Timer = 3f;
    private bool IsSomethingBlocking;
    private Animator playerAnim;
    public PlayerSlideState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Player_Manager.instance.SlideInstance.start();
        playerAnim = _stateMachine.GetComponent<Animator>();
        playerRigidbody = _stateMachine.GetComponent<Rigidbody>();
        Player_Manager.instance.PlayerCollider.enabled = false;
        Player_Manager.instance.SlidingCollider.enabled = true;
    }


    public override void Update()
    {
        Vector3 newPosition = playerRigidbody.position + Vector3.forward * Player_Manager.instance.Speed * Time.deltaTime;
        playerRigidbody.MovePosition(newPosition);
        IsSomethingBlocking = Physics.Raycast(Player_Manager.instance.SlidingCollider.transform.position + new Vector3(0,0.1f,0) , Vector3.up, 1, Player_Manager.instance.obstacle);
        Debug.Log(IsSomethingBlocking);
        Timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W) & IsSomethingBlocking == false) 
        {
            Player_Manager.instance.SlideInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            playerAnim.SetTrigger("Jump");
            Player_Manager.instance.PlayerCollider.enabled = true;
            Player_Manager.instance.SlidingCollider.enabled = false;
            _stateMachine.Begin(new PlayerJumpState(_stateMachine));
        }
        if (Timer <= 0 & IsSomethingBlocking==false)
        {
            playerAnim.SetTrigger("GetUp");
            Player_Manager.instance.PlayerCollider.enabled = true;
            Player_Manager.instance.SlidingCollider.enabled = false;
            _stateMachine.Begin(new PlayerGroundedState(_stateMachine));
        }
        if (Input.GetKeyUp(KeyCode.S) & IsSomethingBlocking == false)
        {
            Player_Manager.instance.SlideInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            playerAnim.SetTrigger("GetUp");
            Player_Manager.instance.PlayerCollider.enabled = true;
            Player_Manager.instance.SlidingCollider.enabled = false;
            _stateMachine.Begin(new PlayerGroundedState(_stateMachine));
        }
    }
}
