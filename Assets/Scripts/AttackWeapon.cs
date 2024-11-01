using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AttackWeapon : MonoBehaviour
{


    public float attackRange = 5.0f;

    public float lastAttackTime = 0.0f;

    public float attackDelay = 2.0f;

    public float attackTime = 1.5f;


    [SerializeField]
    public GameObject attackPrefab;

    Animator animator;

    private GameObject attackPrefabInstance;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void StartAttackPrefab(Vector3 destination)
    {
        attackPrefabInstance = Instantiate(attackPrefab, destination, Quaternion.identity);
        attackPrefabInstance.transform.localScale *= 0.1f;
    }

    private void CheckAttackTime()
    {
        if (Time.time - lastAttackTime >= attackTime) {
            Destroy(attackPrefabInstance);
        }   
    }

    public void Attack(GameObject target)
    {
        if (!animator.GetBool("IsAttacking")) {
            animator.SetBool("IsAttacking", true);
        }

        CheckAttackTime();

        if (Time.time > lastAttackTime + attackDelay) {
            StartAttackPrefab(target.transform.position);
            //target.GetComponent<EnemyUnit>().GetDamage(10.0f);
            lastAttackTime = Time.time;
        }
    }

    public void StopAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
