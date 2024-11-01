using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackController : MonoBehaviour
{
    GameObject target = null;
    AttackWeapon attackWeapon;
    NavMeshAgent agent;
    public GameObject rocket;

    public float attackRange = 50.0f;

    public bool isAttacking = false;

    float lastAttackTime = 0.0f;


    public void SetEnemyTarget(GameObject enemyTarget)
    {
        target = enemyTarget;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        attackWeapon = GetComponent<AttackWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            isAttacking = true;
            //Debug.Log(target);

            if (Time.time - lastAttackTime > 5.0f && isAttacking) {
                Debug.Log("ATTACK GO");
                lastAttackTime = Time.time;
                Vector3 rocketPost = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                GameObject newRocket = Instantiate(rocket, rocketPost, rocket.transform.rotation);
                StartCoroutine(newRocket.GetComponent<RocketLauncher>().Launch(target));
            }

            if (agent.remainingDistance <= attackRange)
            {
                agent.isStopped = true;
                //attackWeapon.Attack(target);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
            }
        }
        else if (isAttacking) {
            isAttacking = false;
            agent.ResetPath();
            //attackWeapon.StopAttack();
        }
    }
}
