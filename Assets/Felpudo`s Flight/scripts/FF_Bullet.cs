using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFBullet : MonoBehaviour
{
    public float speed = 10f;
    public float limiteDestruicaoX = 12f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }
    void FixedUpdate()
    {
        Movement();
    }
    void CheckDistance()
    {
        if (transform.position.x > limiteDestruicaoX)
        {
            Destroy(this.gameObject);
        }
    }
    void Movement()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
