using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fl_Chest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FLGameController.levelEnded = true;
            StartCoroutine(WaitAndLoadNextLevel(3f));
        }
    }
    IEnumerator WaitAndLoadNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
}
