using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRPedra : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private Transform bossIruca;
    Rigidbody2D rb;
    
    [SerializeField]
    private bool movingToBoss = false;
    
    void Start()
    {
        movingToBoss = false;
        rb = GetComponent<Rigidbody2D>();
        bossIruca = GameObject.FindGameObjectWithTag("Uruca").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToBoss && bossIruca != null)
        {
            // Move towards BossIruca
            rb.simulated = false;
            transform.position = Vector2.MoveTowards(transform.position, bossIruca.position, moveSpeed * Time.deltaTime);
            
        }
        else
        {
            // Move left
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            rb.simulated = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            movingToBoss = true;
            StartCoroutine(BossHitDelay());
            IEnumerator BossHitDelay()
            { 
                yield return new WaitForSeconds(1.2f);
                GameController.bossLife -= 1;
                
            }
            
        }
    } 
}
