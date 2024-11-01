using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowerPointerHandler : MonoBehaviour
{

    public GameObject previewObj;
    public BuilderManager builderManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HandleClick()
    {
        Debug.Log("OnPointerClick called.");
        builderManager.StartBuilding(previewObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
