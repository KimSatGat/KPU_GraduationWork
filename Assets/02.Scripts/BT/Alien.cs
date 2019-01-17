﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Alien : MonoBehaviour
{    
    Animator animator;
    BehaviorTree bt;
    public int vitality = 5;   // 체력
    private readonly int hashAttack = Animator.StringToHash("isAttack");
    private readonly int hashDie = Animator.StringToHash("isDie");
    
    private void Awake()
    {
        animator = GetComponent<Animator>();                
    }

    // Start is called before the first frame update
    void Start()
    {
        Build_BT();
        
    }

    // Update is called once per frame
    void Update()
    {
        bt.Run();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            vitality--;
        }
    }

    public bool isVitalityZero()    // 체력이 0이하인가?
    {
        if(vitality<=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die() // Die 액션
    {
        animator.SetBool(hashDie, true);
    }                   
    
    public bool Attack()   
    {
        animator.SetBool(hashAttack, true);
        return true;
    }

    void Build_BT() // 행동트리 생성
    {
        Sequence root = new Sequence();
        Sequence Death = new Sequence();
        Selector behaviour = new Selector();
        Leaf_Node isVitalityZero_Node = new Leaf_Node(isVitalityZero);
        Leaf_Node Attack_Node = new Leaf_Node(Attack);

        root.AddChild(behaviour);
        behaviour.AddChild(Attack_Node);

        bt = new BehaviorTree(root);    // 트리가 완성되면 Alien 행동트리 멤버변수에 적용
    }
    
}
