using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    

    [Header("State")]
    public float MaxHP;
    public float CurHP;


    private void Awake()
    {
        
        CurHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
