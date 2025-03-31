using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacker : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float detectionRange = 200f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileLauncherTransform;
    [SerializeField] [Range(0f, 5f)] float coolDownTime = 1f;
    float coolDown;
    
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
        coolDown -= Time.deltaTime;
        IsAtFront();
        IsInLineOfSight();
        if(IsAtFront() & IsInLineOfSight() & coolDown <= 0f) {
            Fire();
        }
    }

    void Fire() {
        coolDown = coolDownTime;
        Instantiate(projectilePrefab, projectileLauncherTransform.position, transform.rotation);
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
    
    //Whether target in front 180° range
    bool IsAtFront() {
        Vector3 directionToTarget = transform.position - target.transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        if(Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270) {
            //Debug.DrawLine(transform.position, target.transform.position, Color.green);
            return true;
        }

        return false;
    }

    bool IsInLineOfSight() {
        Vector3 directionTowardsTarget = target.transform.position - transform.position;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, directionTowardsTarget, out hit, detectionRange)) {
            //Debug.Log(hit.transform.tag);
            if(hit.transform.CompareTag("Player")) {
                Debug.DrawRay(transform.position, directionTowardsTarget, Color.red);
                return true;
            }
        }
        return false;
    }
}
