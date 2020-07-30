﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制相机视角的脚本
public class ViewController : MonoBehaviour
{
    public float speed=1;
    public float mouseSpeed = 60;
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h, mouse*mouseSpeed, v) * Time.deltaTime*speed,Space.World);
    }
}
