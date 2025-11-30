using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFLesmaAmarelaOld : MonoBehaviour {

  bool comecou;
  bool acabou;
  Rigidbody2D corpoLesma;
  [SerializeField]
  float timerDuration;
  float timer;
  float velocidade = 2f;
  float limiteDestruicaoX = -12f;
  bool jumpTime = true;
  Vector2 forcaImpulso = new Vector2(0, 600f);
  Vector2 gravityDirection = Vector2.down;
  float gravity = 9.8f;

  void Start () { 
    corpoLesma = GetComponent<Rigidbody2D> ();
  }

  void Update()
  {
    
    Timer();
    if (jumpTime)
    {
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
    timerDuration = Random.Range(1.40f, 2.50f);
    timer += Time.deltaTime;
    if (timer >= timerDuration)
    {
      jumpTime = true;
      timer = 0;
    }
  }
  void Mover()
    {    
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);

            if (transform.position.x < limiteDestruicaoX)
            {
                Destroy(gameObject);
            }
    }
}
