using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FFControlaJogadorMouseEsquerdo : MonoBehaviour
{
  [SerializeField]
  GameObject Bullet;

  [Header("Input Settings")]
  [SerializeField]
  Vector3 move;
  [SerializeField]
  bool shoot;

  [Header("Player Settings")]
  [SerializeField]
  float speed = 5f;
  public int lifes = 8;

  float timerDuration;
  float timer;
  bool ShootTime = true;

  enum playerState { Idle, Moving, Shooting }
  [Header("State Machine")]
  [SerializeField]
  playerState state = playerState.Idle;

  // codigo de murilo
  
  bool comecou;
  bool acabou;
  public Rigidbody2D corpoJogador;
  SpriteRenderer sprite;
  Vector2 forcaImpulso = new Vector2(0, 500f);
  
  void Start()
  {
    lifes = 8;
    corpoJogador = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    FFGameController.PlayerAlive = true;
  }

  void Update()
  {
    // codigo de murilo
    /*if (Input.GetButtonDown("Fire1"))
    {

      if (!comecou)
      {
        comecou = true;
        corpoJogador.isKinematic = false;
      }

      corpoJogador.velocity = new Vector2(0, 0);
      corpoJogador.AddForce(forcaImpulso);
    }
    */
    move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
    shoot = Input.GetButton("Fire1");
    if (lifes <= 2)
    while (true)
    {
      StartCoroutine(FlashRedDanger());
      break;
    }
    IEnumerator FlashRedDanger()
    {
      Color originalColor = sprite.color;
      sprite.color = Color.red;
      yield return new WaitForSeconds(0.5f);
      sprite.color = originalColor;
    }
  }
    void FixedUpdate()
    {
    switch (state)
    {
      case playerState.Idle: Idle(); break;
      case playerState.Moving: Movement(); break;
      case playerState.Shooting: Shoot(); break;
    }
    // Move
		move.Normalize();
		transform.position += move * speed * Time.deltaTime;
    corpoJogador.velocity = Vector2.zero;
    }
  void Movement()
  {
    if (shoot)
    {
      state = playerState.Shooting;
    }
    if (move == Vector3.zero)
    {
      state = playerState.Idle;
    }
  }
  void Idle()
  {
    state = playerState.Idle;
    if (move != Vector3.zero)
    {
      state = playerState.Moving;
    }
    if (shoot)
    {
      state = playerState.Shooting;
    }
  }
  void Damage()
  {
      lifes --;
      StartCoroutine(BlinkEffect());
      if (lifes <= 0)
      {   
          FFGameController.PlayerAlive = false;
          Destroy(gameObject);
      }
  }
  IEnumerator BlinkEffect()
  {
      SpriteRenderer sprite = GetComponent<SpriteRenderer>();
      Color originalColor = sprite.color;
      sprite.enabled = false;
      yield return new WaitForSeconds(0.1f);
      sprite.enabled = true;  
  }
  void Shoot()
  { if (ShootTime)
      {
        Instantiate(Bullet, transform.position + transform.right + new Vector3(1f, 0, 0), Quaternion.identity);
        ShootTime = false;
        StartCoroutine(ShootCooldown());
      }
    shoot = false;
    if (!shoot && move != Vector3.zero)
    {
      state = playerState.Moving;
    }
    if (!shoot && move == Vector3.zero)
    {
      state = playerState.Idle;
    }

  }
  IEnumerator ShootCooldown()
  { 
    yield return new WaitForSeconds(0.3f);
    ShootTime = true;
  }
void OnCollisionEnter2D(Collision2D other)
  {
      if (other.gameObject.CompareTag("Enemy"))
      {
          Damage();
      }
  }
}
