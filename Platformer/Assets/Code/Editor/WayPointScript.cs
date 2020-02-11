﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointScript : MonoBehaviour
{
    public Queue<GameObject> waypoints = new Queue<GameObject>();
    
    public int num = 0;

    public float minDist;
    public float speed;

    public bool rand = false;
    public bool active = true;

    private void Start()
    {
        waypoints.Enqueue(gameObject);
    }

    void Update()
    {
        if (active)
        {
            if (Vector3.Distance(transform.position, waypoints.Peek().transform.position) > minDist)
            {
                gameObject.transform.LookAt(waypoints.Peek().transform.position);
                gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
            }
            else
            {
                waypoints.Enqueue(waypoints.Dequeue());
            }
        }
    }
}
