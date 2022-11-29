using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float tspeed = 10f;
    public float fspeed = 15f;
    public float cspeed = 5f;

    bool faceRight = true;

    public Animator animator;

    public float groundCheckDistance = 0.1f;

    public ContactFilter2D groundCheckFilter;

    private Rigidbody2D rb;

    private Collider2D collider2d;

    private List<RaycastHit2D> groundHits = new List<RaycastHit2D>();

    //private float fspeed = 18f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float fmoveX = Input.GetAxisRaw("FHorizontal");
        transform.Translate(Vector2.right *Time.deltaTime *fspeed *fmoveX);
        bool isRunning = !Mathf.Approximately(fmoveX, 0f);
        animator.SetBool("isRunning", isRunning);
        if (fmoveX < 0 && faceRight)
        {
            Flip();
        }
        // Face right
        if (fmoveX > 0 && !faceRight)
        {
            Flip();
        }

        float wmoveX = Input.GetAxisRaw("WHorizontal");
       
        bool isAttacking = !Mathf.Approximately(wmoveX, 0f);
        animator.SetBool("isAttacking", isAttacking);
       

        float cmoveX = Input.GetAxisRaw("CHorizontal");
        transform.Translate(Vector2.right * Time.deltaTime * cspeed * cmoveX);
        bool isCrouchMoving = !Mathf.Approximately(cmoveX, 0f);
        animator.SetBool("isCrouchMoving", isCrouchMoving);
        if (cmoveX < 0 && faceRight)
        {
            Flip();
        }
        // Face right
        if (cmoveX > 0 && !faceRight)
        {
            Flip();
        }



        animator.SetBool("isRunning", isRunning);

        float moveX = Input.GetAxisRaw(PAP.axisXinput);
   
        animator.SetFloat(PAP.moveX, moveX);

        bool isMoving = !Mathf.Approximately(moveX, 0f);

        animator.SetBool(PAP.isMoving, isMoving);

        bool lastOnGround = animator.GetBool(PAP.isOnGround);
        bool newOnGround = CheckIfOnGround();
        animator.SetBool(PAP.isOnGround, newOnGround);


        if(lastOnGround == false && newOnGround == true)
        {
            animator.SetTrigger(PAP.landedOnGround);
        }
        else
        {
            animator.ResetTrigger(PAP.landedOnGround);
        }

        bool isJumpKeyPressed = Input.GetButtonDown(PAP.jumpKeyName);

        if (isJumpKeyPressed)
        {
            animator.SetTrigger(PAP.jumpTriggerName);
        }
        else
        {
            animator.ResetTrigger(PAP.jumpTriggerName);
        }
   
    }

    private void FixedUpdate()
    {
        float forceX = animator.GetFloat(PAP.forceX);

        if (forceX != 0) rb.AddForce(new Vector2(forceX,0) * Time.deltaTime);

        float impulseX = animator.GetFloat(PAP.impulseX);
        float impulseY = animator.GetFloat(PAP.impulseY);

        if (impulseY != 0 || impulseX != 0)
        {
            float xDirectionSign = Mathf.Sign(transform.localScale.x);
            Vector2 impulseVector = new Vector2(xDirectionSign * impulseX, impulseY);


            rb.AddForce(new Vector2(0, impulseY), ForceMode2D.Impulse);
            animator.SetFloat(PAP.impulseY, 0);
            animator.SetFloat(PAP.impulseX, 0);
        }

        animator.SetFloat(PAP.velocityY, rb.velocity.y);

        bool isStopVelocity = animator.GetBool(PAP.stopVelocity);

        if (isStopVelocity)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool(PAP.stopVelocity, false);
        }

           
       
    }

    bool CheckIfOnGround()
    {
        collider2d.Cast(Vector2.down, groundCheckFilter, groundHits, groundCheckDistance);

        if (groundHits.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
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
