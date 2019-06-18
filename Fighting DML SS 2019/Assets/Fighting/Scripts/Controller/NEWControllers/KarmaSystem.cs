using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaSystem : MonoBehaviour
{
    public bool boss1, boss2, boss3;
    private bool ques;

    private void Awake()
    {
        if(boss1 && boss2)
        {
            BadEnd();
        } else if((!boss1 && boss2) || (boss1 && !boss2))
        {
            Debug.Log("fight!");
            ques = true;
        } else if(!boss1 && !boss2)
        {
            GoodEnd();
        }

        if (ques)
        {
            if(boss3)
            {
                BadEnd();
            }
        }
    }

    void BadEnd()
    {
        Debug.Log("first ending (BAD)");
    }

    void GoodEnd()
    {
        Debug.Log("second ending (GOOD)");
    }
}
