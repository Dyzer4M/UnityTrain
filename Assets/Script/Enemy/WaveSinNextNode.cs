using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Wave))]
[RequireComponent(typeof(SphereCollider))]
public class WaveSinNextNode : SingNextNode
{
    public bool isInfectd = false;
    private void Awake()
    {
        SphereCollider collider = gameObject.GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = 2;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInfectd)
        {
            isInfectd = true;
            Wave wave = gameObject.GetComponent<Wave>();
            wave.StartWave();
        }
    }
}
