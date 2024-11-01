using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour
{

    public bool isThrower;
    public float offset = 10.0f;

    public GameObject bulletFab;
    public LayerMask checkMask;

    public bool isLaser;

    private GameObject target;

    private float currRate = 0f;

    private bool isLaserAttackEnding = true;
    private float lastLockTime;
    public GameObject bulletSpawner;
    public GameObject laserFab;

    public GameObject laserObj;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private Collider CheckNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, offset, checkMask);

        float dist = Mathf.Infinity;
        Collider currentCollider = null;


        foreach (Collider collider in colliders)
        {
            float currentDist = Vector3.Distance(transform.position, collider.transform.position);

            Debug.DrawLine(bulletSpawner.transform.position, collider.gameObject.transform.position, Color.blue);

            if (collider.tag == "Enemy" && currentDist < dist)
            {
                currentCollider = collider;
            }
        }


        return currentCollider;


    }

    private void BulletsAttack()
    {
        if (target) {
            currRate += Time.deltaTime;
            
            if (currRate > 0.05f) {
                currRate = 0;

                var bullet = Instantiate(bulletFab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
                Destroy(bullet, 3);
            }
        }
    }

    private IEnumerator AttackLaserEnemy()
    {
        if (target && isLaserAttackEnding) {
            isLaserAttackEnding = false;
            /*
            var laser = GetComponent<LineRenderer>();
            laser.enabled = true;

            laser.SetPosition(0, bulletSpawner.transform.position);
            laser.SetPosition(1, target.transform.position);

            target.GetComponent<EnemyUnit>().GetDamage(100.0f);

            yield return new WaitForSeconds(1f);

            laser.enabled = false;*/

            laserObj.SetActive(true);

            yield return new WaitForSeconds(1f);
            laserObj.SetActive(false);
            isLaserAttackEnding = true;
        }
    }

    private void Lock()
    {
        Collider enemy = CheckNearestEnemy();

        if (Time.time - lastLockTime > 0.5f && enemy != target) {
            target = enemy.gameObject;
            lastLockTime = Time.time;
        }

        if (!enemy && target) {
            target = null;
        }
    }

    private bool RotateToTarget()
    {
        if (!target) return false;

        Vector3 dir = target.transform.position - transform.position;

        float step = 8.0f * Time.deltaTime;

        Quaternion newLookup = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, newLookup, step);

        return Vector3.Angle(bulletSpawner.transform.forward, target.transform.forward) < 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Lock();

        if (target) {
            bool isRotated = RotateToTarget();

            Debug.DrawLine(bulletSpawner.transform.position, target.gameObject.transform.position, Color.red);

            if (!isThrower) {
                if (isLaser) {
                    StartCoroutine(AttackLaserEnemy());
                } else if (!isLaser) {
                    BulletsAttack();
                }
            }
        } 
    }
    
}
