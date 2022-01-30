using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject boosterParticleEffect;


    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BoosterParticleInvoke(Vector3 thisPosition)
    {
        Destroy(Instantiate(boosterParticleEffect, thisPosition, Quaternion.identity), 1f);
    }

}
