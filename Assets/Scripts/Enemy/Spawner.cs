using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject instantiateObj;
    public int wavesCount = 3;
    public int waveInstantiateCount = 3;
    public float spawnDistance = 10.0f;

    public float wavesDelay = 10.0f;
    private float lastWaveStart = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (wavesCount > 0) 
        {
            if (Time.time - lastWaveStart > wavesDelay)
            {
                lastWaveStart = Time.time;
                wavesCount--;

                for (int i = 0; i < waveInstantiateCount; i++)
                {
                    var v2 = Random.insideUnitCircle * spawnDistance;
                    Instantiate(instantiateObj, transform.position + new Vector3(v2.x, 0, v2.y), Quaternion.identity, transform);
                }
            }

        }

    }
}
