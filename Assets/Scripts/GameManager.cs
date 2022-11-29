using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public bool isGameActive;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button exitButton;


    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void startGame()
    {

        //titleScreen.gameObject.SetActive(false);
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
     
    }

    public void exitGame()
    {
        SceneManager.LoadScene("StartPage", LoadSceneMode.Single);

    }
}
