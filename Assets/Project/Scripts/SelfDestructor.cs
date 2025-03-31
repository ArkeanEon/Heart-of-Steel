using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] float destroyTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterDelay(destroyTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
