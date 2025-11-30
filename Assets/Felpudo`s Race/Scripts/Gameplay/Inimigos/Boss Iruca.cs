using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIruca : MonoBehaviour
{
    public int pastLifes;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pastLifes = GameController.bossLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (pastLifes > GameController.bossLife)
        {
            IEnumerator FlashRed()
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = Color.white;
            }
            StartCoroutine(FlashRed());
            pastLifes = GameController.bossLife;
        } 
    }
}
