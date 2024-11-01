using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsThrower : MonoBehaviour
{

    [SerializeField]
    public GameObject bulletFab;
    private TowerState towerState;
    private float currRate;


    // Start is called before the first frame update
    void Start()
    {
        towerState = GetComponentInParent<TowerState>();
    }

    private void BulletsAttack()
    {

        if (towerState.towerTarget) 
        {
            if (!towerState.isAttacking)
            {
                towerState.SetIsAttacking(true);
            }

            currRate += Time.deltaTime;
            
            if (currRate > 0.05f) 
            {
                currRate = 0;

                var bullet = Instantiate(bulletFab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                Destroy(bullet, 3);
            }
        }

        if (!towerState.isAttacking)
        {
            currRate = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        BulletsAttack();
    }
}
