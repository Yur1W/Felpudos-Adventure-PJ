using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FFGameController : MonoBehaviour
{
    [SerializeField]
    FFUIManager uiManager;
    [SerializeField]
    FFControlaJogadorMouseEsquerdo playerController;
    [SerializeField]
    GameObject spawner;
    public static float enemiesKilled;
    public static bool PlayerAlive = true;
    bool gameEnded = false;
    [SerializeField]
    float winCondition = 25f;
    
    void Start()
    {
        uiManager.UpdateLifes(playerController.lifes);
        uiManager.UpdateEnemiesKilled(enemiesKilled);
        uiManager.HideGameOver();
        uiManager.HideVictory();
        uiManager.ShowTutorial();
        gameEnded = false;
        PlayerAlive = true;
        enemiesKilled = 0;
        StartCoroutine(uiManager.HideTutorialAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesKilled >= winCondition && !gameEnded)
        {
            uiManager.ShowVictory();
            spawner.SetActive(false); 
            DestroyAllEnemies();
            playerController.enabled = false; // Disable player controls
            playerController.corpoJogador.velocity = Vector2.right * 2f;
            playerController.corpoJogador.gravityScale = 2f; // Increase gravity
            playerController.GetComponent<CircleCollider2D>().enabled = false; // Disable collisions
            gameEnded = true;
        }
        if (!PlayerAlive && !gameEnded)
        {
            uiManager.ShowGameOver();
            spawner.SetActive(false);
            DestroyAllEnemies();
            gameEnded = true;
        }
        uiManager.UpdateLifes(playerController.lifes);
        uiManager.UpdateEnemiesKilled(enemiesKilled);

    }
    
    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
