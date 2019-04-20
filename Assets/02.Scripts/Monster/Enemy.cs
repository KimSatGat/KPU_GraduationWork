﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum ENEMY_STATE { IDLE, WALK, RUN ,ATTACK ,RETRACE,ROLL,LOOKAROUND,DIE}

public abstract class Enemy : MoveObject
{

    protected GameSystem system;
    protected Animator animator;
    protected NavMeshAgent agent;
    [SerializeField]
    protected ENEMY_STATE eEnemy_State = ENEMY_STATE.IDLE;
    protected void Init()        //하위에서 호출해야함       //그리고 웬만하면 모든 멤버변수를 여기서 초기화해야함
    {
        system = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
    }

    public delegate ENEMY_STATE GetTraceState( ENEMY_STATE _Enemy_State );

 
 
}
