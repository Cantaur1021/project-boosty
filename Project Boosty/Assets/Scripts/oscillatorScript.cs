using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class oscillatorScript : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] [Range(0,1)] private float movementFactor;
    [SerializeField] private float period = 2f;
    

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if(period <= Mathf.Epsilon)
        {
            return;
            
        }
        else
        {
            float cycles = Time.time / period;
            const float tau = Mathf.PI * 2;

            float rawSineWave = Mathf.Sin(cycles * tau);

            movementFactor = (rawSineWave + 1f) / 2f;

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
        
    }
}
