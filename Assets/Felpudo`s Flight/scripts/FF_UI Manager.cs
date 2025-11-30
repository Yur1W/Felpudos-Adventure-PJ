using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FFUIManager : MonoBehaviour
{   [SerializeField]
    GameObject gameOverUI;
    [SerializeField]
    GameObject victoryUI;
    [SerializeField]
    TextMeshProUGUI enemiesKilledText;
    [SerializeField]
    Animator LifesAnimator;
    [SerializeField]
    TextMeshProUGUI lifesText;
    [SerializeField]
    GameObject TutorialUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateLifes(int lifes)
    {
        LifesAnimator.SetInteger("Lifes", lifes);
        lifesText.text = lifes.ToString("0");
    }
    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void HideGameOver()
    {
        gameOverUI.SetActive(false);
    }

    public void ShowVictory()
    {
        victoryUI.SetActive(true);
        StartCoroutine(HideVictoryAfterDelay());
        IEnumerator HideVictoryAfterDelay()
        {
            yield return new WaitForSeconds(3.5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void HideVictory()
    {
        victoryUI.SetActive(false);
    }


    public void UpdateEnemiesKilled(float enemiesKilled)
    {
        enemiesKilledText.text =  enemiesKilled.ToString("0");
    }
    public IEnumerator HideTutorialAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        TutorialUI.SetActive(false);
    }
    public void ShowTutorial()
    {
        TutorialUI.SetActive(true);
    }
}
