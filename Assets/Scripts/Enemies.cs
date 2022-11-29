using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float enemySpeed = 10;
    private Rigidbody2D enemyRb;
    private PlayerController playerControllerScript;
    public GameManager gameManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.isGameActive)
        {
            Vector2 lookDirection = (playerControllerScript.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * enemySpeed, ForceMode2D.Impulse);
        }
        
    }
}
