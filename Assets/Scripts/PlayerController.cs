using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public float jumpForce = 10f;
    public float speed = 10;
    public float gravityModifier;

    public GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityModifier;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.isGameActive)
        {

            float horizontalInput = Input.GetAxis("Horizontal");

            transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            gameManagerScript.gameOver();
        }
    }
}
