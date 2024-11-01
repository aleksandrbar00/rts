using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 2000f, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.tag == "Enemy")
        {
            other.GetComponent<EnemyUnit>().GetDamage(5f);
            Destroy(gameObject);
        }
    }
}
