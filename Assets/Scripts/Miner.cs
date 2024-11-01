using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Miner : MonoBehaviour
{


    public GameObject resourceManagerObj;
    public ResourcesManager resourcesManager;
    public GameObject target;
    public GameObject miningFab;
    public GameObject miningFabObj;
    public float mineActionDelay = 2.0f;
    public float mineActionBaseValue = 3.0f;

    private float lastActionTime = 0.0f;

    private bool isMining = false;

    NavMeshAgent agent;



    // Start is called before the first frame update
    void Start()
    {
        resourcesManager = resourceManagerObj.GetComponent<ResourcesManager>();
        agent = GetComponent<NavMeshAgent>();
    }


    public void SetTarget(GameObject newTarget)
    {  
        target = newTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMining && !target) {
            target = null;
            isMining = false;
            lastActionTime = 0;
            Destroy(miningFabObj);
        }

        if (isMining && Time.time - lastActionTime >= mineActionDelay) {
            var resourceItem = target.GetComponent<ResourceItem>();
            var gainValue = Math.Min(resourceItem.capacity, mineActionBaseValue);

            resourceItem.DecreaseCapacity(mineActionBaseValue);
            resourcesManager.IncrementCount(gainValue);

            lastActionTime = Time.time;
        }

        if(target && agent.remainingDistance < 0.1f && !isMining) {
            isMining = true;

            agent.isStopped = true;
            miningFabObj = Instantiate(miningFab, new Vector3(transform.position.x, transform.position.y, transform.position.z), miningFab.transform.rotation);
            miningFabObj.transform.localScale *= 0.2f;
        }

    }
}
