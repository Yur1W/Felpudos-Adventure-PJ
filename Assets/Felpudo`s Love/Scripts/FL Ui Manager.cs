using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FLUiManager : MonoBehaviour
{   
    [SerializeField]
    Animator LifeHudAnimator;
    [SerializeField]
    GameObject WinScreen;
    [SerializeField]
    GameObject LoseScreen;
    [SerializeField]
    TextMeshProUGUI ScoreText;
    [SerializeField] 
    TextMeshProUGUI TimerText;
    [SerializeField]
    GameObject TutorialPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int lives)
    {
        LifeHudAnimator.SetInteger("Lifes", lives);
    }
    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
    }
    public void ShowLoseScreen()
    {
        LoseScreen.SetActive(true);
    }
    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }
    public void UpdateTimer(float time)
    {
        TimerText.text = time.ToString("F0");
    }
    public void HideWinScreen()
    {
        WinScreen.SetActive(false);
    }   
    public void HideLoseScreen()
    {
        LoseScreen.SetActive(false);
    }
    IEnumerator HideTutorialPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TutorialPanel.SetActive(false);
    }
    public void HideTutorialPanel(float delay)
    {
        StartCoroutine(HideTutorialPanelAfterDelay(delay));
    }
}
