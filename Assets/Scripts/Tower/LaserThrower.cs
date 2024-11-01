using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserThrower : MonoBehaviour
{
    private TowerState towerState;

    [SerializeField]
    private GameObject laserSpawner;

    public float delay = 3.0f;
    public float damage = 20.0f;

    private float lastAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        towerState = GetComponentInParent<TowerState>();
    }

    private IEnumerator LaserAttack()
    {
        towerState.SetIsAttacking(true);
        laserSpawner.SetActive(true);

        yield return new WaitForSeconds(1f);

        towerState?.towerTarget.GetComponent<EnemyUnit>().GetDamage(damage);

        lastAttackTime = Time.time;
        laserSpawner.SetActive(false);
        towerState.SetIsAttacking(false);
    }

    private bool CanStartNewAttack()
    {
        return towerState.towerTarget && !towerState.isAttacking
         && (lastAttackTime == 0 || Time.time - lastAttackTime > delay);
    }

    // Update is called once per frame
    void Update()
    {
        bool canStartNewAttack = CanStartNewAttack();

        if (canStartNewAttack)
        {
            lastAttackTime = 0;
            StartCoroutine(LaserAttack());
        }
    }
}
