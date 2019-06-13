using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDamageColliders : MonoBehaviour
{
    public GameObject[] damageColliders;

    public enum DamageType
    {
        light,
        heavy
    }

    public enum DCtype
    {
        bottom,
        up
    }
    StateManager states;

    private void Start()
    {
        states = GetComponent<StateManager>();
        CloseColliders();
    }

    public void OpenCollider(DCtype type, float delay, DamageType damageType)
    {
        switch (type)
        {
            case DCtype.bottom:
                StartCoroutine(OpenCollider(damageColliders, 0, delay, damageType));
                break;
            case DCtype.up:
                StartCoroutine(OpenCollider(damageColliders, 1, delay, damageType));
                break;
        }
    }

    IEnumerator OpenCollider(GameObject[] array, int index, float delay, DamageType damageType)
    {
        yield return new WaitForSeconds(delay);
        array[index].SetActive(true);
        array[index].GetComponent<DoDamage>().damageType = damageType;
    }

    public void CloseColliders()
    {
        for(int i = 0; i < damageColliders.Length; i++)
        {
            damageColliders[i].SetActive(false);
        }
    }
}
