using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * 2f * Time.deltaTime);
        StartCoroutine(destroyPlane());
    }
    IEnumerator destroyPlane()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
