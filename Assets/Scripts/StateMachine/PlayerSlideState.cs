using UnityEngine;

public class PlayerSlideState : State
{
    private Rigidbody playerRigidbody;
    private float Timer = 3f;
    private bool IsSomethingBlocking;
    private Animator playerAnim;
    public PlayerSlideState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        playerAnim = _stateMachine.GetComponent<Animator>();
        playerRigidbody = _stateMachine.GetComponent<Rigidbody>();
        Player_Movement.instance.PlayerCollider.enabled = false;
        Player_Movement.instance.SlidingCollider.enabled = true;
    }


    public override void Update()
    {
        Vector3 newPosition = playerRigidbody.position + Vector3.forward * Player_Movement.instance.Speed * Time.deltaTime;
        playerRigidbody.MovePosition(newPosition);
        IsSomethingBlocking = Physics.Raycast(Player_Movement.instance.SlidingCollider.transform.position , Vector3.up, Player_Movement.instance.playerHeight, Player_Movement.instance.obstacle);
        Debug.Log(IsSomethingBlocking);
        Timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W) & IsSomethingBlocking == false) 
        {
            playerAnim.SetTrigger("Jump");
            Player_Movement.instance.PlayerCollider.enabled = true;
            Player_Movement.instance.SlidingCollider.enabled = false;
            _stateMachine.Begin(new PlayerJumpState(_stateMachine));
        }
        if (Timer <= 0 & IsSomethingBlocking==false)
        {
            playerAnim.SetTrigger("GetUp");
            Player_Movement.instance.PlayerCollider.enabled = true;
            Player_Movement.instance.SlidingCollider.enabled = false;
            _stateMachine.Begin(new PlayerGroundedState(_stateMachine));
        }
        if (Input.GetKeyUp(KeyCode.S) & IsSomethingBlocking == false)
        {
            playerAnim.SetTrigger("GetUp");
            Player_Movement.instance.PlayerCollider.enabled = true;
            Player_Movement.instance.SlidingCollider.enabled = false;
            _stateMachine.Begin(new PlayerGroundedState(_stateMachine));
        }
    }
}
