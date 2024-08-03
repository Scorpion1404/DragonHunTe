using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]private float speed;
    [SerializeField]private float jumpPower;
    [SerializeField]private LayerMask groundLayer;

    [SerializeField]private LayerMask wallLayer;
   private Rigidbody2D body;
   private Animator anim;

   private BoxCollider2D boxCollider;

   private float wallJumpCoolDown;
   private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;



    private void Awake() {

    //refernce
    body= GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    boxCollider= GetComponent<BoxCollider2D>();
   }

   private void Update() {

    horizontalInput= Input.GetAxis("Horizontal");
    
    
    //flip player left and right
    if(horizontalInput > 0.01f){
        transform.localScale = Vector3.one;
    }
    else if(horizontalInput < -0.01f){
        transform.localScale=new Vector3(-1,1,1);
    }

    //jump
  
    //animator parameters
    anim.SetBool("run",horizontalInput !=0);
    anim.SetBool("grounded",isGrounded());

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //adjustable jump
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0) {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        
        }
        if (OnWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {

                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }

   }

   private void Jump(){


        if(coyoteCounter < 0 && !OnWall() && jumpCounter <= 0) return;
        SoundManager.instance.PlaySound(jumpSound);

        if (OnWall())
        {

            WallJump();
        }
        else
        {
            if (isGrounded())
            {

                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                if(coyoteCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
                else
                {
                    if(jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }
            coyoteCounter = 0;

        }
   }


    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)*wallJumpX,wallJumpY));
        wallJumpCoolDown = 0;

    }



   private bool isGrounded()
   {
    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
    return raycastHit.collider != null;
   }
   private bool OnWall()
   {
    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f,wallLayer);
    return raycastHit.collider != null;
   }

   public bool canAttack(){
     return horizontalInput == 0 &&isGrounded() && !OnWall() ;

   }

   
}
