using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : MonoBehaviour
{

    public float capacity = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DecreaseCapacity(float value)
    {
        capacity -= value;

        if (capacity <= 0) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
