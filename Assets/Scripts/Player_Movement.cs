using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody PlayerRigidbody;
    [SerializeField] private float JumpForce;
    private bool IsJumping = false;

    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsJumping==false) 
        {
         StartCoroutine(Jumping());
        }
    }

    private void FixedUpdate()
    {
        PlayerRigidbody.linearVelocity = new Vector3(0f,0f,3.5f);
    }


    IEnumerator Jumping()
    {
        IsJumping = true;
        PlayerRigidbody.AddForce(new Vector3(0, JumpForce, 0));
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => Mathf.Abs(PlayerRigidbody.linearVelocity.y)<= 0.01);
        IsJumping = false;
    }
}
