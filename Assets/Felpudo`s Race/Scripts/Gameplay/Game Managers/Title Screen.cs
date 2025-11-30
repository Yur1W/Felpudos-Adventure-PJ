using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void Controls()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Credits()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
