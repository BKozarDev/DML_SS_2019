using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox_Collider : MonoBehaviour
{
    GameObject p;

    private void Start()
    {
        p = GameObject.FindWithTag("Player1");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAHH");
        Debug.Log(other.gameObject.tag);

        
            Debug.Log("ASD");
            p.GetComponent<Movement_Controller>().Hit();
        
    }
}
