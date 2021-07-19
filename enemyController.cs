using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    public NavMeshAgent Agent;
    public Animator anim;

    //set different states for the enemy
    public enum AIState
    {
        isIdle, isPatrolling, isChasing, isAttacking
    };

    public AIState currentState;

    public float waitAtPoint = 2f; //will make enemy wait for 2 seconds before moving to next point
    private float waitCounter;
    public float chaseRange;
    public float attackRange = 1f;
    public float timeBetweenAttack = 2f;
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint; //makes enemy wait at their current position

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        switch (currentState)
        {
            case AIState.isIdle:
                anim.SetBool("isMoving", false); //changes animation to idle when not moving
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                } else
                {
                    currentState = AIState.isPatrolling;
                    Agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }

                if(distanceToPlayer <= chaseRange) //how far the enemy will go after the player
                {
                    currentState = AIState.isChasing;
                    anim.SetBool("isMoving", true);
                }

                break;

            case AIState.isPatrolling:

                if (Agent.remainingDistance <= .2f)
                {
                    currentPatrolPoint++; //adds one to the patrol point
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0; //resets the patrol point to loop
                    }
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }

                anim.SetBool("isMoving", true);

                        break;

            case AIState.isChasing:

                Agent.SetDestination(PlayerController.instance.transform.position);

                //if player is close enough to enemy, they'll attack
                if(distanceToPlayer <= attackRange)
                {
                    currentState = AIState.isAttacking;
                    anim.SetTrigger("Attack");
                    anim.SetBool("isMoving", false);

                    Agent.velocity = Vector3.zero;
                    Agent.isStopped = true;

                    attackCounter = timeBetweenAttack;
                }

                //return to patrol when player is out of range
                if(distanceToPlayer > chaseRange)
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                    Agent.velocity = Vector3.zero;
                    Agent.SetDestination(transform.position);
                }
                
                break;

            case AIState.isAttacking:

                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;
                if(attackCounter <= 0)
                {
                    if(distanceToPlayer < attackRange)
                    {
                        anim.SetTrigger("Attack");
                        attackCounter = timeBetweenAttack;
                    }
                    else //return to patrolling
                    {
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;

                        Agent.isStopped = false;
                    }
                }

                break;
        }
    }
}
