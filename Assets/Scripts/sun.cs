using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;
    //private float distanceFromPlayer = 13;
    public GameObject[] balls;
    private float stayDelay = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SpawnBall), stayDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(playerControllerScript.transform.position.x, transform.position.y);
    }

    void SpawnBall()
    {
        float spawnIntervalTime = Random.Range(1.5f, 3.5f);
        int ballsIndex = Random.Range(0, balls.Length);

        if (gameManagerScript.isGameActive)
        {
            Instantiate(balls[ballsIndex], transform.position, balls[ballsIndex].transform.rotation);
            Invoke(nameof(SpawnBall), spawnIntervalTime);
        }
    }
    
}
