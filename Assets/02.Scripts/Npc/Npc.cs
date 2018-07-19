﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    private Transform npcTr;
    public bool isTalk = false;

    public void Init()
    {
        player = GameObject.Find("Player");
        npcTr = GetComponent<Transform>();
        StartCoroutine(PlayerPosition());
        isTalk = false;
    }

// Update is called once per frame
    void Update () {
        
    }

    IEnumerator PlayerPosition()
    {
        float dis = Check.Distance(player.transform, npcTr);
        if (dis < 4.0f) isTalk = true;
        else isTalk = false;

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PlayerPosition());
    }

 

    protected void Work( )
    {
        if (isTalk == false) return;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("안녕하세요?");
        }
    }
}
