using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SelectionManager.Instance.allUnitsObjects.Add(gameObject);
    }

    void OnDestroy()
    {
        SelectionManager.Instance.allUnitsObjects.Remove(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableIndicator(bool isActive)
    {
        transform.GetChild(0).gameObject.SetActive(isActive);
    }
}
