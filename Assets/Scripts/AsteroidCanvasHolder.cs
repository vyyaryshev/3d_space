using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AsteroidCanvasHolder : MonoBehaviour
{
    Transform asteroidTransform;
    float distance;
    private void Start()
    {
        asteroidTransform = GetComponentInParent<Health>().transform;
        distance = transform.position.y - asteroidTransform.position.y;
    }
    private void LateUpdate()
    {
        if (Camera.main != null)
        {
            transform.position = asteroidTransform.position + Camera.main.transform.up * distance;
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}

