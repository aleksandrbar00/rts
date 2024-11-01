using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{

    public float damageRate = 1f;
    public float damage = 0.5f;
    private float currRate = 0f;


    private TowerState towerState;
    public List<GameObject> attackTargets = new List<GameObject>(); 

    void Start()
    {
        towerState = GetComponentInParent<TowerState>();
        towerState.SetIsAttacking(true);
    }

    // Update is called once per frame
    void Update()
    {
        currRate += Time.deltaTime;

        if (currRate > damageRate)
        {
            currRate = 0;

            foreach (GameObject target in attackTargets)
            {
                Debug.Log(target);
                target.GetComponent<EnemyUnit>()?.GetDamage(damage);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") 
        {
            attackTargets.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (attackTargets.Contains(other.gameObject)) 
        {
            attackTargets.Remove(other.gameObject);
        }
    }
}
