using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 500;
    [SerializeField] Detonator detonator;
    [SerializeField] protected int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update() {
    }

    public virtual void TakeDamage(int damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            Destroy(gameObject);  
        }
    }

    public virtual void OnDestroy() {
        Instantiate(detonator, transform.position, Quaternion.identity);        
    }
}
