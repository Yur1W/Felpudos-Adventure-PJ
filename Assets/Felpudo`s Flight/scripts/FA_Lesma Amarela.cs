using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFLesmaAmarela : MonoBehaviour 
{
  Rigidbody2D corpoLesma;
  [SerializeField]
  float timerDuration;
  float timer;
  [SerializeField]
  float velocidade = 10f;
  float limiteDestruicaoX = -12f;
  bool jumpTime = true;
  Vector2 forcaImpulso = new Vector2(0, 580f);
  Vector2 gravityDirection = Vector2.down;
  float gravity = 9.8f;
  [SerializeField]
  public float vida = 2f;
  Collider2D coll;
  Animator animator;

  void Start () 
  { 
    corpoLesma = GetComponent<Rigidbody2D> ();
    coll = GetComponent<Collider2D>();
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    Mover();
    Timer();
    if (jumpTime)
    { 
      corpoLesma.velocity = Vector2.zero;
      corpoLesma.AddForce(forcaImpulso);
      jumpTime = false;

    }

  }
  
    void FixedUpdate()
  {
      gravityDirection.Normalize();
      corpoLesma.AddForce(gravityDirection * gravity, ForceMode2D.Force);
    }
    void Timer()
  {
    timerDuration = Random.Range(1.20f, 2f);
    timer += Time.deltaTime;
    if (timer >= timerDuration)
    {
      jumpTime = true;
      timer = 0;
    }
  }
  void Damage()
  {
      vida --;
      StartCoroutine(FlashRed());
      if (vida <= 0)
      {
          FFGameController.enemiesKilled ++;
          coll.enabled = false;
          animator.Play("Death");
          if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
          Destroy(gameObject);
      }
      IEnumerator FlashRed()
      {
          SpriteRenderer sprite = GetComponent<SpriteRenderer>();
          Color originalColor = sprite.color;
          sprite.color = Color.red;
          yield return new WaitForSeconds(0.1f);
          sprite.color = originalColor;
      }
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Damage();
        }
    }

}
