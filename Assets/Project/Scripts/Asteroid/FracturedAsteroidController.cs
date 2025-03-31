using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturedAsteroidController : MonoBehaviour
{
    [SerializeField][Range(0f, 60f)] float duration = 10f;

    void OnEnable() {
        Destroy(gameObject, duration);
    }
}
