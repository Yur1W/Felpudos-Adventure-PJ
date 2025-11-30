using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{   
   [Header("UI Texts")]
    public Text livesText;
    public Text scoreText;
    public Text distanceText;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject bossLifePanel;
    public GameObject TutorialPanel;

    public void UpdateLives(int lives)
    {
        livesText.text = "❤️ x" + lives;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateDistance(float distance)
    {
        distanceText.text = distance.ToString("0000") + " m";
    }
    public void ShowBossLife(bool show)
    {
        bossLifePanel.SetActive(show);
    }
    public void UpdateBossLife(int life)
    {
        bossLifePanel.GetComponent<Slider>().value = life;  
    }

    public void ShowGameOver(bool show)
    {
        gameOverPanel.SetActive(show);
    }

    public void ShowVictory(bool show)
    {
        victoryPanel.SetActive(show);
    }
    public IEnumerator HideTutorial()
    {
        yield return new WaitForSeconds(8f);
        TutorialPanel.SetActive(false);
    }
}

