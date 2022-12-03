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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HPText;
    public Button restartButton;
    public Button exitButton;
    //public Button startGame;
    

    private float score;
    public float HP;
    private float maxHP = 3;



    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        updateScore(0);
        updateHP(3);
  
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

    public void updateHP(int addToHP)
    {

        if (isGameActive)
        {
            HP += addToHP;
            // make HP not over maxHP 3;
            if (HP >= maxHP)
            {
                HP = maxHP;
            }
            HPText.text = "HP. " + HP;
            if (HP <= 0)
            {
                gameOver();
            }
        }

    }

    public void updateScore(int addToScore)
    {
        score += addToScore;
        scoreText.text = "Scores: " + score;
    }

    

    public void exitGame()
    {
        SceneManager.LoadScene("StartPage", LoadSceneMode.Single);

    }
}
