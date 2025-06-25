using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using System.Collections;
using TMPro;

public class PlayerSlideState : State
{
    private Rigidbody playerRigidbody;
    private float Timer = 2f;
    private bool IsSomethingBlocking;
    private bool OnExit = false;
    private Animator playerAnim;
    public PlayerSlideState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        OnExit = false;
        Player_Manager.instance.SlideInstance.start();
        playerAnim = _stateMachine.GetComponent<Animator>();
        playerRigidbody = _stateMachine.GetComponent<Rigidbody>();
        Player_Manager.instance.PlayerCollider.enabled = false;
        Player_Manager.instance.SlidingCollider.enabled = true;

        foreach(GameObject Mesh in Player_Manager.instance.CollisionMesh)
        {
            Mesh.SetActive(false);
        }
    }


    public override void Update()
    {
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, playerRigidbody.linearVelocity.y, Player_Manager.instance.Speed);
        IsSomethingBlocking = Physics.Raycast(Player_Manager.instance.SlidingCollider.transform.position + new Vector3(0,0.1f,0) , Vector3.up, 1, Player_Manager.instance.obstacle);
        Timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W) && IsSomethingBlocking == false && OnExit == false) 
        {
            Player_Manager.instance.SlideInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Exit("Jump", new PlayerJumpState(_stateMachine));
            OnExit = true;
            return;
        }
        if (Timer <= 0 && IsSomethingBlocking==false && OnExit == false)
        {
            Exit("GetUp", new PlayerGroundedState(_stateMachine));
            OnExit = true;
            return;
        }
        if (Input.GetKeyUp(KeyCode.S) && IsSomethingBlocking == false && OnExit == false)
        {
            Player_Manager.instance.SlideInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Exit("GetUp", new PlayerGroundedState(_stateMachine));
            OnExit = true;
            return;
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

        foreach (GameObject Mesh in Player_Manager.instance.CollisionMesh)
        {
            Mesh.SetActive(true);
        }
        _stateMachine.Begin(state);
    }
}
