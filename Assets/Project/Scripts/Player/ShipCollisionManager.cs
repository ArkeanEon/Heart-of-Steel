using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionManager : MonoBehaviour
{
    [SerializeField] float collisionDamage = 2.5f;
    DamageManager damageManager;
    private void Awake() {
        damageManager = GetComponent<DamageManager>();
    }
    private void OnCollisionEnter(Collision other) {

        float impactForce = other.impulse.magnitude;
        float damage = collisionDamage * impactForce;
        int damageToInt = Mathf.FloorToInt(damage);

        damageManager.TakeDamage(damageToInt);
    }
}
