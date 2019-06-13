using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    private Movement_Controller move;

    #region Variables
    public float changeStateTolerance = 3; //How close is considered close combat

    public float normalRate = 1; //How fast will his AI decide state (normal state)
    private float nrmTimer;

    public float closeRate = 0.5f; //How fast will his AI decide state (close state)
    private float clTimer;

    public float blockingRate = 1.5f; //How long will he block
    private float blTimer;

    public float aiStateLife = 1; //How much time does it take to reset the AI state
    private float aiTimer;

    bool initiateAI; //When it has an AI state to run
    bool closeCombat; //If we are in close combat

    bool gotRandom;
    float storeRandom;

    bool randomizeAttack;
    int numberOfAttacks;
    int curNumAttacks;

    public float jumpRate = 1;
    float jRate;
    bool jump;
    float jtimer;
    #endregion

    public AttackPatterns[] attackPatterns;

    public enum AIState
    {
        closeState,
        normalState,
        resetAI
    }

    public AIState aiState;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckDistance();
        States();
        AIAgent();
    }

    void States()
    {
        switch (aiState)
        {
            case AIState.closeState:
                CloseState();
                break;
            case AIState.normalState:
                NormalState();
                break;
            case AIState.resetAI:
                //ResetAI();
                break;
        }

        //Blocking();
        //Jumping();
    }

    void AIAgent()
    {
        if (initiateAI)
        {
            aiState = AIState.resetAI;
            //Create multiplier
            float multiplier = 0;

            //Get random value;
            if (!gotRandom)
            {
                storeRandom = ReturnRandom();
                gotRandom = true;
            }

            //If we are not in close combat
            if (!closeCombat)
            {
                //We have 30% more chances of moving
                multiplier += 30;
            } else
            {
                //... we have 30% more chances to attack
                multiplier -= 30;
            }

            //Compare random value with the added modifiers
            if(storeRandom + multiplier < 50)
            {
                Attack(); //Attack
            } else
            {
                //Movement();
            }
        }
    }

    void Attack()
    {
        if (!gotRandom)
        {
            storeRandom = ReturnRandom();
            gotRandom = true;
        }

        //75 chances of doing a normal attack...
        //if(storeRandom < 75)
        //{
        //See how many attacks he will do...
        if (!randomizeAttack)
        {
            numberOfAttacks = (int)Random.Range(1, 4);
            randomizeAttack = true;
        }

        if(curNumAttacks < numberOfAttacks)
        {
            int attackNumber = Random.Range(0, attackPatterns.Length);

            //StartCoroutine(OpenAttack(attackPatterns[attackNumber], 0));

            curNumAttacks++;
        }
    }

    void NormalState()
    {

    }

    void CloseState()
    {

    }

    void Jumping()
    {

    }

    float ReturnRandom()
    {
        float retVal = Random.Range(0, 101);
        return retVal;
    }

    [System.Serializable]
    public class AttackPatterns
    {
        public AttackBase[] attacks;
    }

    [System.Serializable]
    public class AttackBase
    {
        public bool attack1;
        public bool attack2;
        public float delay;
    }
}
