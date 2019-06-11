using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox_Collider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        transform.parent.GetComponent<Movement_Controller>().CollisionDetected(this);
    }
}
