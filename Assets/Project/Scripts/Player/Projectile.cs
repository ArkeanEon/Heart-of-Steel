using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject damageFire;
    [SerializeField][Range(15000f, 30000f)] float launchForce = 25000f;
    [SerializeField] int damage = 50;
    [SerializeField] float existTime = 5f;
    float duration;
    Rigidbody rb;
    AudioSource audioSource;



    // Start is called before the first frame update
    void Awake() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() {
        rb.AddForce(launchForce * transform.forward);
        duration = existTime;
        audioSource.Play();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if(duration <= 0f)
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        //Debug.Log("Projectile collides");

        foreach (ContactPoint contact in other.contacts) {
            GameObject temp = Instantiate(damageFire, contact.point, Quaternion.identity);
            Destroy(temp, 1f);
        }
        DamageManager target = other.gameObject.GetComponent<DamageManager>();
        if(target != null) {
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
