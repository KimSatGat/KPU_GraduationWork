﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour {
    private float r = 0.0f;

    private Transform playerTr;
    private Rigidbody playerRb;
    private Jump jump;
    private Move move;
    public float rotSpeed = 250.0f; //회전 속도

    [SerializeField]
    private STATE eState = STATE.STAND;
    private STATE ePreState = STATE.STAND;
    private bool isDoubleJump = false;
    private bool isRun = false;


    [SerializeField]
    private float bombPower;

    
    void Start()
    {

        eState = STATE.STAND;
        ePreState = STATE.STAND;

        playerRb = GetComponent<Rigidbody>();
        playerTr = GetComponent<Transform>(); //Player Transform 컴포넌트 할당


        jump = GetComponent<Jump>();
        isDoubleJump = false;


        move = GetComponent<Move>();
        isRun = false;

        bombPower = 15.0f;
    }


    void Update()
    {
        r = Input.GetAxis("Mouse X");

        playerTr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r); // Y축을 기준으로 rotSpeed 만큼 회전

        move.Horizontal = Input.GetAxis("Horizontal");
        move.Vertical = Input.GetAxis("Vertical");

        KeyboardManual();
        MoveManual();

        LogicManual();
    }


    private void KeyboardManual()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (eState == STATE.JUMP && isDoubleJump == true)
            {
                playerRb.AddForce(new Vector3(0, 1.6f, 0) * 5.0f, ForceMode.Impulse);
                isDoubleJump = false;
            }
            else if(eState != STATE.JUMP)
            {
                ePreState = eState;
                eState = STATE.JUMP;
                jump.Action(1.6f, 5.0f);     //점프력,점프스피드
                isDoubleJump = true;
            }
        }
       
        if(Input.GetKeyDown(KeyCode.Return))
        {
         //뭐를쓸까낭?
        }

        Running();

        if(Input.GetKeyDown(KeyCode.E))
        {
           
          
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground" && eState == STATE.JUMP)
        {
            eState = ePreState;
            isDoubleJump = false;
        }
    }

    void State()
    {
        
    }

    private void MoveManual()
    {
        if (eState == STATE.JUMP) return;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (eState == STATE.RUN) return;
            eState = STATE.WALK;
        }
        else
        {
            eState = STATE.STAND;

        }

        
    }

    private void LogicManual()
    {
        switch(eState)
        {
            case STATE.RUN:
                move.SetMoveSpeed(20.0f);
                break;
            case STATE.WALK:
                move.SetMoveSpeed(10.0f);
                break;
        }
    }

    private void Running()
    {
        if (Input.GetKey(KeyCode.W) && isRun == true)       //두번누르면 여기로들어옴
        {
            isRun = false;
            eState = STATE.RUN;
        }
        else if(Input.GetKeyUp(KeyCode.W) && eState != STATE.RUN)       
        {
            isRun = true;
            StartCoroutine(RunningStart());
        }
    }

    IEnumerator RunningStart()
    {
        yield return new WaitForSeconds(0.2f);          //0.2초안에 두번눌러야 달리기
        isRun = false;
    }
}
 