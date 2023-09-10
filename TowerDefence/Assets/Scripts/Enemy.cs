using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;

    private Transform target;

    private int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0]; //ilk hedef noktasý.
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position,target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }

        if (GameManager.gameOver)
        {
            Destroy(gameObject);
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            PlayerState.Live -= 1;
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }
}
