using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{

    public bool isMiner;
    public bool isFightUnit;

    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;
    public bool enableMovement { get; set; }
    Animator animator;
    Vector3 destination = Vector3.zero;
    TargetLine targetLine;
    AttackController attackController;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        targetLine = GetComponent<TargetLine>();
        attackController = GetComponent<AttackController>();
    }

    private bool CheckEnemySelectedToAttack(RaycastHit hit)
    {
        if (hit.collider.gameObject && hit.collider.gameObject.tag == "Enemy")
        {
            return true;
        }

        return false;
    }

    private bool CheckMineResourceToHandle(RaycastHit hit) {
        if (hit.collider.gameObject && hit.collider.gameObject.tag == "Resource")
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        var currentPosition = agent.transform.position;

        if (destination != Vector3.zero) {
            targetLine.StartLineRender(currentPosition, destination);
        }

        if (agent.remainingDistance < 1.0f) 
        {
            targetLine.StopLineRender();          
        
            if (agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                destination = Vector3.zero;
                animator.SetBool("IsWalking", false);
            }

        }


        if (Input.GetMouseButtonDown(1) && enableMovement) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground)) {
                if (isFightUnit && CheckEnemySelectedToAttack(hit))
                {
                    targetLine.StopLineRender();          
                    attackController.SetEnemyTarget(hit.collider.gameObject);
                } 
                else if (isMiner )
                {
                    if (CheckMineResourceToHandle(hit)) {
                        Debug.Log("Resource");
                        GetComponent<Miner>().SetTarget(hit.collider.gameObject);
                    } else {
                        GetComponent<Miner>().SetTarget(null);
                    }
                }
                else if (attackController) {
                    attackController.SetEnemyTarget(null);
                }

                agent.isStopped = false;
                agent.SetDestination(hit.point);
                destination = hit.point;
                
                if (animator) {
                    animator.SetBool("IsWalking", true);
                }
            }
        }
    }
}
