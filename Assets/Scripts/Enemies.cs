using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float enemySpeed = 10f;

    private Rigidbody2D enemyRb;
    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;
    private Animator enemyAnim;
    public Collider2D turtleShell;
    public Collider2D turtleCol1;
    

    private Vector2 startPos;
    public float enemyWalkDistance = 20;

    private bool moveRight;
    private Vector3 localScale;
    private bool isFaceRight;
    

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        enemyRb = GetComponent<Rigidbody2D>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyAnim = GetComponent<Animator>();
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //if (gameManagerScript.isGameActive)
        //{
        //    Vector2 lookDirection = (playerControllerScript.transform.position - transform.position).normalized;
        //    enemyRb.AddForce(lookDirection * enemySpeed, ForceMode2D.Impulse);
        //}

        if (gameManagerScript.isGameActive)
        {
            if ((transform.position.x - startPos.x) < -enemyWalkDistance)
            {
                moveRight = true;
                isFaceRight = true;
                Flip();
                //enemyRb.velocity = new Vector2(1 * enemySpeed, enemyRb.velocity.y);
            }
            if ((transform.position.x - startPos.x) > 1)
            {
                moveRight = false;
                isFaceRight = false;
                Flip();
                //enemyRb.velocity = new Vector2(1 * enemySpeed, enemyRb.velocity.y);
            }

            if (moveRight)
            {
                movingRight();
            }
            else
            {
                movingLeft();
            }

            if (Mathf.Abs(transform.position.x - startPos.x)> 200)
            {
                Destroy(gameObject);
                gameManagerScript.updateScore(5);
            }
        }

        if (!gameManagerScript.isGameActive)
        {
            enemyAnim.enabled = false;
        }
          
    }
    void movingLeft()
    {
        moveRight = false;
        transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
    }
    void movingRight()
    {
        moveRight = true;
        transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (turtleShell)
        {
            enemyAnim.SetBool("inShell", true);
            StartCoroutine(inShellTime());
            turtleCol1.enabled = false;
            
            
        }
    }

    IEnumerator inShellTime()
    {
        yield return new WaitForSeconds(3);
        enemyAnim.SetBool("inShell", false);
        turtleCol1.enabled = true;
        
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFaceRight = !isFaceRight;

        // Multiply the player's x local scale by -1.
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
