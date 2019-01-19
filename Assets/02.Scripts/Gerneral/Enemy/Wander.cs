﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Wander : MonoBehaviour
{
    int pointIndex = 0;
    public List<Transform> wayPoints;

    private NavMeshAgent agent;
    bool check_Destination = false;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
    }

    void Start()
    {
        
    }

    public void Init(float _speed)      //상태변경시 마다 한번들어옴
    {
        agent.speed = _speed;
        agent.destination = wayPoints[pointIndex].position;
        check_Destination = false;
    }


    public RESULT Work()
    {
        if (check_Destination) return RESULT.RUNNING;
        check_Destination = false;
        agent.destination = wayPoints[pointIndex].position;
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f && !check_Destination)
        {
            pointIndex = pointIndex == 0 ? 1 : 0;
            check_Destination = true;
            Change_After_Time_Bool( 3.0f);
            return RESULT.SUCCESS;
        }
        return RESULT.FAIL;
    }
    public void Change_After_Time_Bool(float _time)
    {
        StartCoroutine(Reset_Bool(_time));
       
    }

    IEnumerator Reset_Bool(float _time)
    {
        yield return new WaitForSeconds(_time);
        check_Destination = false;
    }
}
