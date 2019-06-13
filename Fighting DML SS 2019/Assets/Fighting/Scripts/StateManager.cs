using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    public int health = 100;

    public float horizontal;
    public float vertical;
    public bool attack1;
    public bool attack2;
    public bool attack3;
    public bool crouch;

    public bool canAttack;
    public bool gettingHit;
    public bool currentlyAttacking;

    public bool dontMove;
    public bool onGround;
    public bool lookRight;

    //public Slider healthSlider;
    Animator anim;

    //[HideInInspector]
    //public HandleDamageColliders handleDC;
    //[HideInInspector]
    //public HandleAnimations handleAnim;
    //[HideInInspector]
    //public HandleMovement handleMovement;

    public GameObject[] movementColliders;

    ParticleSystem blood;

    // Start is called before the first frame update
    void Start()
    {
        //handleDC = GetComponent<Han>
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
