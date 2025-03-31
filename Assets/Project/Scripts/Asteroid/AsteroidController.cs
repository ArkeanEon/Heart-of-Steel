using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    //[SerializeField] FracturedAsteroidController fracturedAsteroid;
    //[SerializeField] Detonator detonator;
    //Transform asteroid;

    // Start is called before the first frame update
    void Awake() {
        //asteroid = transform;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*protected override void OnDestroy()
    {
       DestroyAsteroid();
    }

    void DestroyAsteroid() {
         if (lastHit == null) 
    {
        Debug.LogError("lastHit is null in DestroyAsteroid");
        return;
    }
        //instantiate fractured asteroid
        //Instantiate(fracturedAsteroid, transform.position, transform.rotation);
        
        //instantiate explosion
        Instantiate(detonator, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }*/
}
