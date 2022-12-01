using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    public Collider2D headCollider;

    //public float jumpForce = 1600;
    private float jumpPower = 25;
    public float speed = 10;
    public float gravityModifier;
    public float dashPower = 1600;
    public float dashStrength = 50;

    public bool doubleJump;
    public bool running;
    public bool isOnGround = true;
    public bool isFaceRight = true;
    public bool dashReady = true;
    public bool isDashing;
    public bool isCrouch;
    public bool isCrouching;

    private GameManager gameManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.isGameActive)
        {

            float horizontalInput = Input.GetAxis("Horizontal");
            playerAnim.SetFloat("Speed", Mathf.Abs(horizontalInput));
            if (running)
            {
                transform.Translate(Vector2.right * horizontalInput * (speed * 2.1f) * Time.deltaTime);
            }
            else if (!running && !isCrouch)
            {
                transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);
            }
            else if (isCrouching)
            {
                transform.Translate(Vector2.right * horizontalInput * (speed * 0.5f) * Time.deltaTime);
            }

            // jump function
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isDashing)
            {
                headCollider.enabled = true;
                playerAnim.SetBool("isJumping", true);
                isOnGround = false;
                //playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
                doubleJump = false;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJump && !isDashing)
            {
                playerAnim.SetBool("isDoublejumping", true);
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower * 0.8f);
                //playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = true;
            }

            //dash function
            if (Input.GetKeyDown(KeyCode.J) && isOnGround && dashReady)
            {
                headCollider.enabled = true;
                playerAnim.SetTrigger("dashing");
                dashReady = false;
                isDashing = true;
                StartCoroutine(dashTime());
                StartCoroutine(dashing());
                if (isFaceRight)
                {
                    playerRb.AddForce(Vector2.right * dashPower, ForceMode2D.Impulse);
                }
                else
                {
                    playerRb.AddForce(Vector2.left * dashPower, ForceMode2D.Impulse);
                }
            }


            // speeding function
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (horizontalInput != 0)
                {
                    headCollider.enabled = true;
                }
                running = true;
                playerAnim.SetBool("isRunning", running);
                //playerAnim.SetFloat("speed_multiplier", 2.0f);
            }
            else if (running)
            {
                running = false;
                playerAnim.SetBool("isRunning", running);
                //playerAnim.SetFloat("speed_multiplier", 1.0f);
            }

            // flip detect  
            if (!isDashing)
            {
                if (horizontalInput > 0 && !isFaceRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (horizontalInput < 0 && isFaceRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if ((isOnGround && horizontalInput == 0) || (isOnGround && horizontalInput != 0 && !running))
                {
                    headCollider.enabled = false;
                }
                

                isCrouch = true;
                playerAnim.SetBool("isCrouch", isCrouch);
                if (horizontalInput != 0 && !running)
                {
                    isCrouching = true;
                    playerAnim.SetBool("isCrouching", isCrouching);
                }

            }
            else if (isCrouch)
            {
                isCrouch = false;
                playerAnim.SetBool("isCrouch", isCrouch);
                isCrouching = false;
                playerAnim.SetBool("isCrouching", isCrouching);
                headCollider.enabled = true;
            }

        }
    }

    IEnumerator dashTime()
    {
        yield return new WaitForSeconds(1f);
        dashReady = true;
    }
    IEnumerator dashing()
    {
        yield return new WaitForSeconds(.5f);
        isDashing = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            playerAnim.SetBool("isJumping", false);
            playerAnim.SetBool("isDoublejumping", false);

        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            if (!isDashing)
            {
                gameManagerScript.gameOver();
                //playerAnim.SetBool("isGameOver",true);
                playerAnim.SetTrigger("die");
            }
        }

        //dashing fly enemy 
        if (collision.gameObject.CompareTag("enemy") && isDashing)
        {
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            // detect away direction 
            Vector2 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * dashStrength, ForceMode2D.Impulse);

        }
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