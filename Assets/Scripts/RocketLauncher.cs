using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject explFab;
    public float damage = 10.0f;
    public float speed = 5.0f;
    public float launchSpeed = 3.0f;
    public bool isLaunching = true;
    public bool isAiming = false;
    public bool isFlying = false;

    private Vector3 targetPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator Launch(GameObject target)
    {
        targetPos = target.transform.position;

        while (Vector3.Distance(targetPos, transform.position) > 0.1f && target) {
            if (transform.position.y > 15.0f && isLaunching) {
                isLaunching = false;
                isAiming = true;
            }

            if (isLaunching) {
                transform.position += new Vector3(0, launchSpeed, 0) * Time.deltaTime;
            } 
            else if (isAiming) {
                yield return new WaitForSeconds(1.5f);
                transform.LookAt(targetPos);

                isAiming = false;
                isFlying = true;
            }
            else if (isFlying) {
                transform.position += (targetPos - transform.position).normalized * speed * Time.deltaTime;
                Debug.DrawRay(transform.position, transform.position);
            }

            yield return null;
        }  


        var explObj = Instantiate(explFab, transform.position, explFab.transform.rotation);
        
        Destroy(gameObject); 

        if (target && target.GetComponent<EnemyUnit>()) {
            target.GetComponent<EnemyUnit>().GetDamage(damage);
        }

        yield return new WaitForSeconds(1f);
        Destroy(explObj);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
