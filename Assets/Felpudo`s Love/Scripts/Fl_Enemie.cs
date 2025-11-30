using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fl_Enemie : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 10f;
    
    [Header("Player")]
    [SerializeField] private Transform player;
    [SerializeField]
    private MoveFelpudo playerScript;
    
    [Header("Detections")]
    [SerializeField] private GameObject wallCheckPoint;
    [SerializeField] private float checkDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpCooldown = 0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Reset jump cooldown
        if (jumpCooldown > 0f)
        {
            jumpCooldown -= Time.deltaTime;
        }
        
        // Check grounded
        CheckGrounded();
        
        // Move towards player 
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            animator.Play("Walk");
            
            // Flip sprite 
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
                wallCheckPoint.transform.localPosition = new Vector3(Mathf.Abs(wallCheckPoint.transform.localPosition.x), wallCheckPoint.transform.localPosition.y, wallCheckPoint.transform.localPosition.z);
            }
            else if (player.position.x < transform.position.x)
            {
                spriteRenderer.flipX = false;
                wallCheckPoint.transform.localPosition = new Vector3(-Mathf.Abs(wallCheckPoint.transform.localPosition.x), wallCheckPoint.transform.localPosition.y, wallCheckPoint.transform.localPosition.z);
            }
        
        // Jump if wall & grounded & cooldown over
        if (IsWallAhead() && isGrounded && jumpCooldown <= 0f)
        {
            Jump();
        }
        if (playerScript.currentState == MoveFelpudo.PlayerState.jumping && jumpCooldown <= 0f)
        {
            Jump();
        }
    }
    
    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);
        isGrounded = hit.collider != null;
    }
    
    private bool IsWallAhead()
    {
        Vector2 direction = spriteRenderer.flipX ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(wallCheckPoint.transform.position, direction, checkDistance, groundLayer);
        return hit.collider != null;
    }
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpCooldown = 0.5f;
    }
}
