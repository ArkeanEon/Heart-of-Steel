using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float rotationDamp = 2.5f;
    [SerializeField] float speed = 100f;

    [SerializeField] float haltDistance = 100f;
    [SerializeField] float rayCastOffset = 5f;
    [SerializeField] float detectionRange = 20f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!SecuredTarget()) {
            return;
        }
        PathFinding();
        MoveForwards();
    }

    bool SecuredTarget() {
        if(target == null) {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if (target == null) {
            return false;
        }
        return true;
    }


    void TurnTowardsPlayer() {
        Vector3 direction = target.transform.position - transform.position;

        Quaternion newRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationDamp * Time.deltaTime);

        //Debug.DrawLine(transform.position, target.transform.position, Color.blue);
    }

    void MoveForwards() {
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
        if(distanceToPlayer > haltDistance) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    void PathFinding() {
        RaycastHit hit;
        Vector3 offset = Vector3.zero;

        Vector3 toLeft = transform.position - transform.right * rayCastOffset;
        Vector3 toRight = transform.position + transform.right * rayCastOffset;
        Vector3 upwards = transform.position + transform.up * rayCastOffset;
        Vector3 downwards = transform.position - transform.up * rayCastOffset;

        Debug.DrawRay(toLeft, transform.forward * detectionRange, Color.cyan);
        Debug.DrawRay(toRight, transform.forward * detectionRange, Color.cyan);
        Debug.DrawRay(upwards, transform.forward * detectionRange, Color.cyan);
        Debug.DrawRay(downwards, transform.forward * detectionRange, Color.cyan);

        if(Physics.Raycast(toLeft, transform.forward, out hit, detectionRange)){
            offset = Vector3.right;
        } else if(Physics.Raycast(toRight, transform.forward, out hit, detectionRange)){
            offset -= Vector3.right;
        }

        if(Physics.Raycast(upwards, transform.forward, out hit, detectionRange)){
            offset -= Vector3.up;
        } else if(Physics.Raycast(downwards, transform.forward, out hit, detectionRange)){
            offset += Vector3.up;
        }

        if(offset != Vector3.zero) {
            transform.Rotate(offset * 5f * Time.deltaTime);
        } else {
            TurnTowardsPlayer();
        }
    }

}
