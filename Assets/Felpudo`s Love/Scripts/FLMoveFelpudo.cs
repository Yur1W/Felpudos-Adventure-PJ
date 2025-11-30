using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFelpudo : MonoBehaviour
{

	// Scripts
	[SerializeField]
	FLGameController gameController;
	SpriteRenderer sprite;
	Rigidbody2D rb;
	BoxCollider2D boxCollider;
	Animator anim;
	[SerializeField]
	public float currentSpeed;
	[SerializeField]
	float jumpSpeed = 8f;
	[SerializeField]
	float moveSpeed = 8f;
	public float jumpForce = 6f;
	[SerializeField]
	public float moveInput;
	public bool jumpInput;
	public bool jumpedEnemy = false;
	Vector3 movement = new Vector3();
	[SerializeField]
	float groundCheckDistance = 0.75f;


	public enum PlayerState{idle,running,jumping,falling}
	public PlayerState currentState = PlayerState.idle;

	// Use this for initialization
	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		moveInput = Input.GetAxisRaw("Horizontal");
		jumpInput = Input.GetKey(KeyCode.Space);
		
		Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);
		ChangeDirection();
	}
	bool GroundCheck()
	{
		return Physics2D.CircleCast(transform.position, 0.12f, Vector2.down, groundCheckDistance, LayerMask.GetMask("Ground"));
	}
	bool BoxCheck()
	{
		return Physics2D.CircleCast(transform.position, 0.12f, Vector2.down, groundCheckDistance, LayerMask.GetMask("Box"));
	}
	void EnemyCheck()
    {
		 RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.12f, Vector2.down, groundCheckDistance, LayerMask.GetMask("KillEnemy"));
		 if (hit.collider != null)
        {	
             // Destroy the enemy (parent if exists, otherwise the hit object)
            if (hit.collider.transform.parent != null)
            {
                Destroy(hit.collider.transform.parent.gameObject);
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }          
			
        }
    }
    void FixedUpdate()
    {
        switch (currentState)
		{
			case PlayerState.idle:
				Idle();
				break;
			case PlayerState.running:
				Run();
				break;
			case PlayerState.jumping:
				Jump();
				break;
			case PlayerState.falling:
				Fall();
				break;
			default: Idle(); break;	
		}
    }

	void Run()
	{
		currentSpeed = moveSpeed;
		//movement = new Vector3(moveInput, 0, 0);
		//movement.Normalize();
		//transform.position += movement * currentSpeed * Time.deltaTime;
		rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);
		anim.Play("Run");


		//transições
		if (jumpInput && (GroundCheck() || BoxCheck()))
		{
			currentState = PlayerState.jumping;
		}
		if (moveInput == 0)
		{
			currentState = PlayerState.idle;
		}
		if (!GroundCheck() && !BoxCheck())
		{
			currentState = PlayerState.falling;
		}
	}
	void Jump()
	{
		rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		anim.Play("Jump");
		//transições

		currentState = PlayerState.falling;
		
	}
	bool JumpCheck()
    {
      if (jumpInput && (GroundCheck() || BoxCheck()))
        {
			return true;
        }
		else
		{
			return false;
		}
    }
	void TakeDamage()
	{	
		IEnumerator DamageCoroutine()
		{
			// Flash red
			sprite.color = Color.red;
			yield return new WaitForSeconds(0.2f);
			sprite.color = Color.white;
		}
		StartCoroutine(DamageCoroutine());
		FLGameController.Lives -= 1;
	}

	void Fall()
    {	
		anim.Play("Fall");
		rb.velocity = new Vector2(moveInput * jumpSpeed, rb.velocity.y);
		//if (EnemyCheck())

		//transições
		if (GroundCheck() || BoxCheck())
        { 
			anim.Play("Landing");
			if (Mathf.Abs(moveInput) > 0.1f)
			{
				currentState = PlayerState.running;
			}
			else
			currentState = PlayerState.idle;
        }
        
    }

	void Idle()
    {
		anim.Play("Idle");
		movement = Vector3.zero;

		//transições
		if (Mathf.Abs(moveInput) > 0.1f)
		{currentState = PlayerState.running;}
		if (JumpCheck())
		{currentState = PlayerState.jumping;}
		if (!GroundCheck() && !BoxCheck())
		{currentState = PlayerState.falling;}

    }
	void ChangeDirection()
    {
        if (moveInput > 0)
		{
			sprite.flipX = false;
		}
		else if (moveInput < 0)
		{
			sprite.flipX = true;
		}
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Pedra"))
        {
			TakeDamage();
        }
    }
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// Check if player is falling and hit enemy from above
			if (rb.velocity.y < 0)
			{
				return;
			}
			else
			{
				// Player hit enemy from side/below - take damage
				TakeDamage();
			}
		}
		if (collision.gameObject.CompareTag("Pedra"))
		{
			TakeDamage();
		}
	}

}