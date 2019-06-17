using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    public GameObject[] players;

    public float distance = 3f;
    public float moveSpeed = 5f;
    public bool isActive;

    private GameObject nearP;
    private Vector3 startPos;

    private void Start()
    {
        nearP = players[0];
        startPos = this.transform.position;
    }

    private Vector3 direction;

    private void Update()
    {
        if (DistanceCheck(gameObject, players[0]) > DistanceCheck(gameObject, players[1]))
            nearP = players[1];
        else
            nearP = players[0];

        transform.LookAt(nearP.transform);

        if (DistanceCheck(transform.position, startPos) < 0.02)
        {
            isActive = false;
        }
    }

    private bool near;

    private void FixedUpdate()
    {
        if(DistanceCheck(gameObject, nearP) < distance)
        {
            near = true;
            BackAway();
            StartCoroutine(Returning());
        } else
        {
            near = false;
        }

        if (isActive)
        {
            Forward();
        }
    }

    IEnumerator Returning()
    {
        yield return new WaitForSeconds(5);
        if (!near)
        {
            isActive = true;
        }
    }

    void BackAway()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    void Forward()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * moveSpeed);
    }

    float DistanceCheck(GameObject crowd, GameObject player)
    {
        return Vector3.Distance(crowd.transform.position, player.transform.position);
    }
    float DistanceCheck(Vector3 crowd, Vector3 player)
    {
        return Vector3.Distance(crowd, player);
    }
}
