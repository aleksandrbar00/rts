using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotationToTarget : MonoBehaviour
{

    public Action<GameObject> OnTargetChanged;


    public bool holdTarget;
    private GameObject target;

    public float checkSphereDistance = 10.0f;
    public LayerMask checkSphereMask;

    private float lastLockTime;

    private TowerState towerState;

    // Start is called before the first frame update
    void Start()
    {
        towerState = GetComponent<TowerState>();
    }

    private Collider[] GetOverlapDistanceSphereColliders()
    {
        return Physics.OverlapSphere(transform.position, checkSphereDistance, checkSphereMask);
    }

    private bool IsTargetInOverlap()
    {
        List<GameObject> collidersGameObject = GetOverlapDistanceSphereColliders().Select(item => item.gameObject).ToList();

        return target && collidersGameObject.Contains(target);
    }

    private Collider CheckNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkSphereDistance, checkSphereMask);

        float dist = Mathf.Infinity;
        Collider currentCollider = null;


        foreach (Collider collider in colliders)
        {
            float currentDist = Vector3.Distance(transform.position, collider.transform.position);

            Debug.DrawLine(transform.position, collider.gameObject.transform.position, Color.blue);

            if (collider.tag == "Enemy" && currentDist < dist)
            {
                currentCollider = collider;
            }
        }


        return currentCollider;


    }

    private void Lock()
    {
        Collider enemy = CheckNearestEnemy();
        bool isTargetInOverlap = IsTargetInOverlap();

        if (isTargetInOverlap) {
            return;
        }

        if (Time.time - lastLockTime > 0.5f && enemy != target) 
        {
            if (enemy && enemy.gameObject)
            {
                target = enemy.gameObject;
                towerState.SetTarget(enemy.gameObject);
                OnTargetChanged?.Invoke(enemy.gameObject);
            }
            else
            {
                target = null;
            }
            lastLockTime = Time.time;
        }

        if (!isTargetInOverlap)
        {
            target = null;
        }
    }


    private void RotateToTarget()
    {
        Vector3 dir = target.transform.position - transform.position;

        float step = 8.0f * Time.deltaTime;

        Quaternion newLookup = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, newLookup, step);
    }

    private float GetAngleToTarget()
    {
        return Vector3.Angle(transform.forward, target.transform.forward);

    }

    private void DrawLineToTarget()
    {
        Debug.DrawLine(transform.position, target.gameObject.transform.position, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        Lock();

        if (target) 
        {
            RotateToTarget();

            bool isRotated = GetAngleToTarget() < 0.1f;

            DrawLineToTarget();
        } 
    }
}
