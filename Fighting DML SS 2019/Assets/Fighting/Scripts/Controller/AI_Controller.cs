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

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
