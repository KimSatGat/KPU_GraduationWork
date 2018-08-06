﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Vector3 launchPos;
    public void SetLaunchPos(Vector3 _launchPos)
    {
        launchPos = _launchPos;
    }

    private Quaternion launchRot;
    public void SetLaunchRot(Quaternion _launchRot)
    {
        Debug.Log(_launchRot);
        launchRot = _launchRot;
    }

    float speed = 1000.0f;
    // Use this for initialization


    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();         //성능이슈를 위해 미리 받아놓을뿐
    }
	void Start () {
        rb.position = launchPos;
        transform.Rotate(launchRot.eulerAngles);
        rb.AddForce(transform.forward * speed);
    }
	
	
}
