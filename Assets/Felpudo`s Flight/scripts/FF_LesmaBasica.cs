using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFLesmaBasica : MonoBehaviour
{   
    [SerializeField]
    float life = 3f;
    [SerializeField]
    float velocidade = 2f;
    float limiteDestruicaoX = -12f;
    SpriteRenderer sprite;
    Animator animator;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
    }
    void Damage()
    {
        life -= 1f;
        StartCoroutine(FlashRed());
        if (life <= 0)
        {
            FFGameController.enemiesKilled ++;
            animator.Play("Death");
            coll.enabled = false;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            Destroy(gameObject);
        }
    }
    IEnumerator FlashRed()
    {
        Color originalColor = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
    }
    void Mover()
    {    
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
            animator.Play("Lesmão");

            if (transform.position.x < limiteDestruicaoX)
            {
                Destroy(gameObject);
              
            }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Damage();
        }
    }
}
