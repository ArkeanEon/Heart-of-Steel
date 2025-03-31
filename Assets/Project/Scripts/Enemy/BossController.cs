using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject endpoint;
    [SerializeField] GameObject target;
    [SerializeField] float patrolRadius = 10f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationDamp = 2.5f;
    [SerializeField] float haltDistance = 200f;
    [SerializeField] float detectionRange = 200f;

    private Vector3 nextPatrolPoint;
    private bool isMovingToNextPoint = false;
    private bool isChasingPlayer = false;

     bool SecuredTarget() {
        if(target == null) {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if (target == null) {
            return false;
        }
        return true;
    }

    bool SecuredEndpoint() {
        if(endpoint == null) {
            endpoint = GameObject.FindGameObjectWithTag("Endpoint");
        }
        if (endpoint == null) {
            return false;
        }
        return true;
    }

    void Start()
    {
        GenerateNextPatrolPoint();
    }

    void Update()
    {
        if (!SecuredTarget()) {
            return;
        }

        if (!SecuredEndpoint()) {
            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) <= detectionRange)
        {
            isChasingPlayer = true;
        }
        else
        {
            isChasingPlayer = false;
        }

        if (isChasingPlayer)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (!isMovingToNextPoint)
        {
            GenerateNextPatrolPoint();
        }

        MoveTowardsNextPatrolPoint();

        if (Vector3.Distance(transform.position, nextPatrolPoint) < 1f)
        {
            isMovingToNextPoint = false; // Reached the patrol point, generate a new one
        }
    }

    void MoveTowardsNextPatrolPoint()
    {
        // Move towards the next patrol point
        transform.position = Vector3.MoveTowards(transform.position, nextPatrolPoint, moveSpeed * Time.deltaTime);
    }

    void GenerateNextPatrolPoint()
    {
        // Generate a random point within the patrol radius
        Vector2 randomPoint = Random.insideUnitCircle.normalized * patrolRadius;
        nextPatrolPoint = new Vector3(randomPoint.x, 0, randomPoint.y) + endpoint.transform.position;
        isMovingToNextPoint = true;
    }

    void MoveForwards() {
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
        if(distanceToPlayer > haltDistance) {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    void MoveTowardsPlayer()
    {
        // Ensure the boss does not move outside the patrol range when chasing the player
        Vector3 directionToPlayer = target.transform.position - transform.position;
        Vector3 targetPosition = transform.position + directionToPlayer.normalized * moveSpeed * Time.deltaTime;
        
        // Check if the target position is within the patrol range
        if (Vector3.Distance(endpoint.transform.position, targetPosition) <= patrolRadius)
        {
            //transform.position = targetPosition;
            MoveForwards();
        }
        
        TurnTowardsPlayer();
    }

    void TurnTowardsPlayer() {
        Vector3 direction = target.transform.position - transform.position;

        Quaternion newRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationDamp * Time.deltaTime);

        //Debug.DrawLine(transform.position, target.transform.position, Color.blue);
    }
}
