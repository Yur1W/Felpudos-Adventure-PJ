using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLInfiniteScrolling : MonoBehaviour
{
    float startPosition;
    float length;
    [SerializeField]
    GameObject Cam;
    [SerializeField]
    float parallaxEffect;
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Cam.transform.position.x * parallaxEffect;
        float movement = Cam.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
        if (movement > startPosition + length)
        {
            startPosition += length;
        }
        else if (movement < startPosition - length)
        {
            startPosition -= length;
        }
    }
}
