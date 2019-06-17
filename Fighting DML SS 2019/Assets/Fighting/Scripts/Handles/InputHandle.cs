using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    float horizontal;
    float vertical;
    bool attack1;
    bool attack2;
    bool attack3;

    StateManager states;

    private void Start()
    {
        states = GetComponent<StateManager>();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        attack1 = Input.GetKey(KeyCode.G);
        attack2 = Input.GetKey(KeyCode.H);
        attack3 = Input.GetKey(KeyCode.J);

        states.horizontal = horizontal;
        states.vertical = vertical;
        states.attack1 = attack1;
        states.attack2 = attack2;
        states.attack3 = attack3;
    }
}
