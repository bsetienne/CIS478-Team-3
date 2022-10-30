//B.E.
//Created: 09/09/22
//Modified: 09/11/22
//Team 3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    static private float walkSpeed = 400f;
    bool grounded = false;
    bool faceRight = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {   //Player movement 
        float hMove = Input.GetAxis("Horizontal");
        float speed = walkSpeed;
        rb.velocity = new Vector2(hMove * speed * Time.deltaTime, rb.velocity.y);


        
        // Face left
        if (hMove < 0 && faceRight)
        {
//             rb.transform.Rotate(0f, 180f, 0, Space.Self);
//             faceRight = false;
                Flip();
        }
        // Face right
        if (hMove > 0 && !faceRight)
        {
//             rb.transform.Rotate(0f, 180f, 0, Space.Self);
//             faceRight = true;
                Flip();

        }
        //Run Animation 
        if (hMove == 0)
            anim.SetBool("Run", false);
        else
            anim.SetBool("Run", true);
            
        // crouch
        



        // Jump
        float jump = Input.GetAxis("Jump");
        if (jump == 1 && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump * 8);
            grounded = false;
        }

    }
 
        //Detect if on gorund, if true allow jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            grounded = true;
        }
    }
    
    // we can creat a new method called filp to make the player turn around. 
    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		faceRight = !faceRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
