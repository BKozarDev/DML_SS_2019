using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMovement : MonoBehaviour
{
    Rigidbody rb;
    StateManager states;
    HandleAnimations anim;

    public float acceleration = 30;
    public float airAcceleration = 15;
    public float maxSpeed = 60;
    public float jumpSpeed = 5;
    public float jumpDuration = 5;

    float actualSpeed;
    bool justJumped;
    bool canVariableJump;
    float jmpTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        states = GetComponent<StateManager>();
        anim = GetComponent<HandleAnimations>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (!states.dontMove)
        {
            HorizontalMovement();
            Jump();
        }
    }

    void HorizontalMovement()
    {
        actualSpeed = this.maxSpeed;
        if (states.onGround)
        {
            rb.AddForce(new Vector3((states.horizontal * actualSpeed) - rb.velocity.x * this.acceleration, 0));

            if(states.horizontal > 0.5)
            {
                anim.anim.SetBool("Move_R", true);
            } else if(states.horizontal < -0.5)
            {
                anim.anim.SetBool("Move_L", true);
                states.blocking = true;
            } else
            {
                states.blocking = false;
                anim.anim.SetBool("Move_R", false);
                anim.anim.SetBool("Move_L", false);
            }
        }

        if(states.horizontal == 0 && states.onGround)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void Jump()
    {
        if(states.vertical > 0)
        {
            if (!justJumped)
            {
                justJumped = true;
                if (states.onGround)
                {
                    anim.JumpAnim();

                    rb.velocity = new Vector3(rb.velocity.x, this.jumpSpeed);
                    jmpTimer = 0;
                    canVariableJump = true;
                }
            } else
            {
                if (canVariableJump)
                {
                    jmpTimer += Time.deltaTime;
                    if(jmpTimer < this.jumpDuration / 1000)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, this.jumpSpeed);
                    }
                }
            }
        } else
        {
            justJumped = false;
        }
    }

    public void AddVelocityOnCharacter(Vector3 direction, float timer)
    {
        StartCoroutine(AddVelocity(timer, direction));
    }

    IEnumerator AddVelocity(float timer, Vector3 direction)
    {
        float t = 0;

        while(t < timer)
        {
            t += Time.deltaTime;

            rb.velocity = direction;
            yield return null;
        }
    }
}
