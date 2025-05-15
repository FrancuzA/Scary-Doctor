using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using System.Collections;

public class PlayerSlideState : State
{
    private Rigidbody playerRigidbody;
    private float Timer = 1f;
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
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, playerRigidbody.linearVelocity.y, Player_Manager.instance.Speed);
        IsSomethingBlocking = Physics.Raycast(Player_Manager.instance.SlidingCollider.transform.position + new Vector3(0,0.1f,0) , Vector3.up, 1, Player_Manager.instance.obstacle);
        Debug.Log(IsSomethingBlocking);
        Timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W) & IsSomethingBlocking == false) 
        {
            Player_Manager.instance.SlideInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Exit("Jump", new PlayerJumpState(_stateMachine));
        }
        if (Timer <= 0 & IsSomethingBlocking==false)
        {
            Exit("GetUp", new PlayerGroundedState(_stateMachine));
        }
        if (Input.GetKeyUp(KeyCode.S) & IsSomethingBlocking == false)
        {
            Player_Manager.instance.SlideInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Exit("GetUp", new PlayerGroundedState(_stateMachine));
        }
    }

    public void Exit(string AnimName, State state)
    {
        playerAnim.SetTrigger(AnimName);
        Player_Manager.instance.PlayerCollider.enabled = true;
        Player_Manager.instance.SlidingCollider.enabled = false;
        _stateMachine.StartCoroutine(DelayedStateTransition(state));
    }

    private IEnumerator DelayedStateTransition(State state)
    {
        yield return new WaitForSeconds(0.2f);
        _stateMachine.Begin(state);
    }
}
