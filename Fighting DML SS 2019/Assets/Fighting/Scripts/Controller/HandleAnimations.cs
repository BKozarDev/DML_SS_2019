using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator anim;
    StateManager states;

    public float attackRate = .3f;
    //public AttackBase[] attacks = new AttackBase[2];

    // Start is called before the first frame update
    void Start()
    {
        states = GetComponent<StateManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        states.dontMove = anim.GetBool("DontMove");

        anim.SetBool("TakeHit", states.gettingHit);
        anim.SetBool("OnAir", !states.onGround);
        anim.SetBool("Crouch", states.crouch);

        float movement = (states.lookRight) ? states.horizontal : -states.horizontal;
        anim.SetFloat("Movement", movement);

        if (states.vertical < 0)
        {
            states.crouch = true;
        }
        else
        {
            states.crouch = false;
        }

        HandleAttacks();
    }

    void HandleAttacks()
    {
        if (states.canAttack)
        {
            if (states.attack1)
            {
                //attacks[0].attack = true;
                //attacks[0].attackTimer = 0;
                //attacks[0].timesPressed++;
            }

            //if(attacks[0])
        }
    }
}
