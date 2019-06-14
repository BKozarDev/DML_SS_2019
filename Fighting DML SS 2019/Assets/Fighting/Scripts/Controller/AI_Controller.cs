using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{

    #region Variables

    StateManager states;
    public StateManager enStates;

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

    bool checkForBlocking;
    bool blocking;
    float blockMultiplier;

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
        states = GetComponent<StateManager>();

        AISnapshot.GetInstance().RequestAISnapshot(this);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
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
                ResetAI();
                break;
        }

        //Blocking();
        Jumping();
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
                Movement();
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

            StartCoroutine(OpenAttack(attackPatterns[attackNumber], 0));

            curNumAttacks++;
        }
    }

    void Movement()
    {
        if (!gotRandom)
        {
            storeRandom = ReturnRandom();
            gotRandom = true;
        }

        if(storeRandom < 90)
        {
            if (enStates.transform.position.x < transform.position.x)
                states.horizontal = -1;
            else
                states.horizontal = 1;
        } else
        {
            if (enStates.transform.position.x < transform.position.x)
                states.horizontal = 1;
            else
                states.horizontal = -1;
        }
    }

    void ResetAI()
    {
        aiTimer += Time.deltaTime;

        if(aiTimer > aiStateLife)
        {
            initiateAI = false;
            states.horizontal = 0;
            states.vertical = 0;
            aiTimer = 0;

            gotRandom = false;

            storeRandom = ReturnRandom();
            if (storeRandom < 50)
                aiState = AIState.normalState;
            else
                aiState = AIState.closeState;

            curNumAttacks = 1;
            randomizeAttack = false;
        }
    }

    void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, enStates.transform.position);

        if(distance < changeStateTolerance)
        {
            if (aiState != AIState.resetAI)
                aiState = AIState.closeState;

            closeCombat = true;
        } else
        {
            if (aiState != AIState.resetAI)
                aiState = AIState.normalState;

            if (closeCombat)
            {
                if (!gotRandom)
                {
                    storeRandom = ReturnRandom();
                    gotRandom = true;
                }

                if(storeRandom < 60)
                {
                    Movement();
                }
            }

            closeCombat = false;
        }
    }

    void Blocking()
    {
        if (states.gettingHit)
        {
            if (!gotRandom)
            {
                storeRandom = ReturnRandom();
                gotRandom = true;
            }

            if(storeRandom < 50)
            {
                blocking = true;
                states.gettingHit = false;
            }
        }

        if (blocking)
        {
            blTimer += Time.deltaTime;
            
            if(blTimer > blockingRate)
            {
                blTimer = 0;
            }
        }
    }

    void NormalState()
    {
        nrmTimer += Time.deltaTime;

        if(nrmTimer > normalRate)
        {
            initiateAI = true;
            nrmTimer = 0;
        }
    }

    void CloseState()
    {
        clTimer += Time.deltaTime;

        if(clTimer > closeRate)
        {
            clTimer = 0;
            initiateAI = true;
        }
    }

    void Jumping()
    {
        if (!enStates.onGround)
        {
            float ranValue = ReturnRandom();

            if(ranValue < 50)
            {
                jump = true;
            }
        }

        if (jump)
        {
            states.vertical = 1;
            jRate = ReturnRandom();
            jump = false;
        } else
        {
            states.vertical = 0;
        }

        jtimer += Time.deltaTime;

        if(jtimer > jumpRate * 10)
        {
            if(jRate < 50)
            {
                jump = true;
            } else
            {
                jump = false;
            }

            jtimer = 0;
        }
    }

    float ReturnRandom()
    {
        float retVal = Random.Range(0, 101);
        return retVal;
    }

    IEnumerator OpenAttack(AttackPatterns a, int i)
    {
        int index = i;
        float delay = a.attacks[index].delay;
        states.attack1 = a.attacks[index].attack1;
        states.attack2 = a.attacks[index].attack2;
        yield return new WaitForSeconds(delay);

        states.attack1 = false;
        states.attack2 = false;

        if(index < a.attacks.Length - 1)
        {
            index++;
            StartCoroutine(OpenAttack(a, index));
        }
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
