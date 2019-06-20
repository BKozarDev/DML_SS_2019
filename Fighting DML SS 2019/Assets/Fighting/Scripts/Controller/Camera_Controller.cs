using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera_Controller : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = 5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    StateManager p1, p2;

    bool win;

    private void Start()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");

        p1 = player1.GetComponent<StateManager>();
        p2 = player2.GetComponent<StateManager>();

        targets.Add(player1.transform);
        targets.Add(player2.transform);

        cam = GetComponent<Camera>();

        win = false;
    }

    private void LateUpdate()
    {

        if (targets.Count == 0)
            return;

        UpdateMove();
        UpdateZoom();

        if (p1.win && !win)
        {
            targets.RemoveAt(0);
            win = true;
        } else if(p2.win && !win)
        {
            targets.RemoveAt(1);
            win = true;
        }

        if (win)
        {
            offset = new Vector3(0, 2, -5.8f);
            maxZoom = 10;
        }
    }

    void UpdateZoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    void UpdateMove()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
