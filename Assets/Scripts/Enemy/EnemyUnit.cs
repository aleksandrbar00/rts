using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{

    [SerializeField]
    GameObject deathEffectFab;
    public float hp = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playDeathEffects(Vector3 pos)
    {
        if (!deathEffectFab) return; 

        var effect = Instantiate(deathEffectFab, pos, deathEffectFab.transform.rotation);
        effect.transform.localScale *= 0.1f;
        Destroy(effect, 1.9f);
    }

    public void GetDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0) {
            playDeathEffects(transform.position);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.grey);
        }
    }
}
