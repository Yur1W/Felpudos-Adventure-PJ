using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLGameController : MonoBehaviour
{
    public static int Lives = 8;
    public static int Score = 0;
    static float Timer = 200f;
    [SerializeField]
    FLUiManager uiManager;
    [SerializeField]
    float initialTimer = 200f;
    [SerializeField]
    GameObject player;
    public static bool levelEnded = false;
    // Start is called before the first frame update
    void Start()
    {   
        Timer = initialTimer;
        levelEnded = false;
        Lives = 8;
        Score = 0;
        uiManager.HideLoseScreen();
        uiManager.HideWinScreen();
        uiManager.UpdateLives(Lives);
        uiManager.UpdateScore(Score);
        uiManager.HideTutorialPanel(8f);
    }

    // Update is called once per frame
    void Update()
    {   if (player != null)
        {
            
        
            if (player.GetComponent<MoveFelpudo>().enabled == true)
            {
                Timer -= Time.deltaTime;
            }
        
            uiManager.UpdateLives(Lives); 
            uiManager.UpdateScore(Score);
            uiManager.UpdateTimer(Timer);
            if (Lives <= 0 || Timer <= 0f)
            {
                uiManager.ShowLoseScreen();
                player.GetComponent<MoveFelpudo>().enabled = false;
                Destroy(player);
            }
            if (levelEnded)
            {
                uiManager.ShowWinScreen();
            }
        }
    }
}
