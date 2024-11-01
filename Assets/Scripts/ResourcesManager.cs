using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{


    public event Action OnEventTriggered;
    
    public float stoneCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IncrementCount(float count)
    {
        stoneCount += count;
        OnEventTriggered.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
