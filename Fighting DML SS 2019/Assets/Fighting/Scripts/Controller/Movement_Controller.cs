using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Controller : MonoBehaviour
{

    public bool canMove;
    public bool canAttack;

    private bool player;
    private bool jump;
    private bool onGround;

    public float health;
    [Range(0,4)]
    public float speed;

    public GameObject p1;
    public GameObject p2;

    private Rigidbody rig;

    Animator anim;

    [Header("Colliders")]
    public GameObject hand_L;
    public GameObject hand_R;
    public GameObject toe_L;
    public GameObject toe_R;



    // Start is called before the first frame update
    void Start()
    {
        //p1 = GameObject.FindGameObjectWithTag("Player1");
        //p2 = GameObject.FindGameObjectWithTag("Player2");

        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        if(gameObject.tag == "Player1")
        {
            player = true;
            p1 = this.gameObject;
            p2 = GameObject.FindGameObjectWithTag("Player2");
        } else if(gameObject.tag == "Player2")
        {
            p2 = this.gameObject;
            p1 = GameObject.FindGameObjectWithTag("Player1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAction();
        UpdateInput();
    }

    private float lastClickTime;
    private const float DOUBLE_CLICK_TIME = .2f;

    private bool cooldown = false;
    private bool hit_L = false;
    private bool hit_R = false;
    private bool hit = false;

    void UpdateInput()
    {

        if (player && canMove)
        {
            //Jump
            if (Input.GetKeyDown(KeyCode.W))
            {
                jump = true;
            }

            //Dash

            //Left
            if (Input.GetKeyDown(KeyCode.A) && onGround)
            {
                float timeSinceLastClick = Time.time - lastClickTime;

                if(timeSinceLastClick <= DOUBLE_CLICK_TIME)
                {
                    if (Input.GetKeyDown(KeyCode.A)){
                        Dash(-1);
                        cooldown = true;
                    }
                }

                lastClickTime = Time.time;

            }
            //Right
            if (Input.GetKeyDown(KeyCode.D) && onGround)
            {
                float timeSinceLastClick = Time.time - lastClickTime;

                if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        cooldown = true;
                        Dash(1);
                    }
                }

                lastClickTime = Time.time;
            }

            //coolDown Timer
            if (cooldown)
            {
                var timer = Time.time;
                var time = Time.time - timer;

                if(time > 2f)
                {
                    cooldown = false;
                    timer = 0;
                    time = 0;
                }

            }

            if (Input.GetKey(KeyCode.A) && onGround)
            {
                anim.SetBool("Move_L", true);
            } else
            {
                anim.SetBool("Move_L", false);
            }
            if (Input.GetKey(KeyCode.D) && onGround)
            {
                anim.SetBool("Move_R", true);
            } else
            {
                anim.SetBool("Move_R", false);
            }

            //Animation Hit

            //Hit R
            if (Input.GetKeyDown(KeyCode.G) && !hit)
            {
                hit = true;
                anim.SetBool("Hit_R", true);
                Debug.Log("Hit_R");
                
            }
            if (Input.GetKeyUp(KeyCode.G))
            {
                hit_R_time = Time.time;
                hit_R = true;
            }
            if (hit_R)
            {
                var time = Time.time - hit_R_time;
                if (time <= 0.5f)
                {
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        Debug.Log("Double hit");
                        anim.SetBool("Hit_L", true);
                        hit_L_time = Time.time;
                        hit_L = true;
                    }
                }
                else if(time >= 0.5f)
                {
                    anim.SetBool("Hit_R", false);
                    hit_R = false;
                    hit = false;
                }
                Debug.Log(time);
            }
            
            //Hook L
            if (Input.GetKeyDown(KeyCode.H))
            {
                anim.SetBool("Hit_L", true);
                hit_L_time = Time.time;
                hit_L = true;
                hit = true;
            } 
            if (hit_L)
            {
                var time = Time.time - hit_L_time;
                if (time >= 0.5f)
                {
                    anim.SetBool("Hit_L", false);
                    hit_L = false;
                    hit = false;
                }
            }
        }
    }

    private float hit_R_time;
    private float hit_L_time;

    private Vector3 newPos;

    void UpdateAction()
    {
        if (player && canMove)
        {
            transform.LookAt(p1.transform.position);
            var horizontal = Input.GetAxis("Horizontal");

            Vector3 newPos = transform.position;

            newPos.x += horizontal;

            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
            if (jump && onGround)
            {
                Jump();
                onGround = false;
            }
        } else
        {
            transform.LookAt(p1.transform.position);
            //if(Vector3.Distance(p1.transform.position, p2.transform.position) < 3f)
            //{
            //    if(p1.transform.position.x > p2.transform.position.x)
            //    {
            //        MoveLeft();
            //    } else
            //    {
            //        MoveRight();
            //    }
            //}
        }

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }

        
    }

    private float velocity = 8f;

    void Jump()
    {
        rig.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
        jump = false;
    }

    void MoveRight()
    {
        float dir = 1;

        Vector3 newPos = transform.position;
        newPos.x += dir;

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
    }

    void MoveLeft()
    {
        float dir = -1;

        Vector3 newPos = transform.position;
        newPos.x += dir;

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
    }

    private float slide_Vel = 3f;

    void Dash(int dir)
    {
            rig.AddForce(new Vector3(dir, 0, 0) * slide_Vel, ForceMode.VelocityChange);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jump = false;
            onGround = true;
        }
    }

    public Collider[] colliders; 
    
}
