using UnityEngine;

class TowerState: MonoBehaviour 
{
    public GameObject towerTarget;

    public bool isAttacking;

    public void SetIsAttacking(bool state)
    {
        isAttacking = state;
    }

    public void ChangeToNoTarget()
    {
        SetIsAttacking(false);
    }

    public void SetTarget(GameObject target)
    {
        if (!target) 
        {
            ChangeToNoTarget();
        }
     
        towerTarget = target;
    }
}