using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterCtrl : MonoBehaviour
{
    [SerializeField] bool canDoubleJump, canSneak, canDash, canRun, canJump, isDashing, isFalling, isWalking, isGrounded;
    [SerializeField] float walkingSpeed, runningSpeed, jumpForce, dashPower, dashDuration;
    [SerializeField] int jumpCounter = 0;
    Rigidbody2D rb;
    SpriteRenderer sr;
    AttackArea AttackArea = default;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); 
        isDashing = false;
        isGrounded = false;
        AttackArea = gameObject.transform.GetChild(0).GetComponent<AttackArea>();
    }

    // Update is called once per frame
    void Update()
    {
        fallChecker();
        wholeWalkMechanism();
        wholeDashMechanism();
        wholeJumpMechanism();
        wholeRunMechanism();
        wholeSneakMechanism();

        if(isWalking)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if(rb.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isFalling", false);
        }
        else if(rb.velocity.y < 0)
        {
            anim.SetBool("isFalling", true);
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isFalling", false);
            anim.SetBool("isJumping", false);
        }
    }



    /* --------------- COLLISION FUNCTIONS --------------- */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("jumpableGround"))
        {
            canJump = true;
            jumpCounter = 0;
        }

        if (collision.gameObject.CompareTag("jumpableGround"))
        {
            isGrounded = true;
        }
    } /*Ground Check*/

    private void OnCollisionExit2D(Collision2D collision) /* Ground Check */
    {
        if(collision.gameObject.CompareTag("jumpableGround"))
        {
            isGrounded = false;
        }
    }
    /* --------------------------------------------------- */




    /* -------------- FUNCTION DEFINITIONS ------------- */
    void Dash()
    {
        if (!sr.flipX)
        {
            rb.velocity = new Vector2(dashPower, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-dashPower, rb.velocity.y);
        }
        Invoke("stopDash", dashDuration);
    } /*Makes character dash*/

    void stopDash()
    {
        isDashing = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
    } /*Controls duration of dash*/

    void wholeDashMechanism()
    {
        if (canDash && Input.GetKeyDown(KeyCode.C))
        {
            isDashing = true;
        }

        if (isDashing)
        {
            Dash();
        }
    } /*Entire dash mechanism*/
    void wholeJumpMechanism() /*Entire jump mechanism*/
    {
        if (canDoubleJump)
        {
            if (jumpCounter == 0 && isFalling)
            {
                jumpCounter++;
            }

            if (Input.GetKeyDown(KeyCode.Space) && canJump && (jumpCounter < 2))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCounter++;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump && !isFalling)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canJump = false;
            }
        }
    }
    
    void wholeRunMechanism()
    {
        if (canRun && isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftControl) && isWalking)
            {
                if (sr.flipX)
                {
                    rb.velocity = new Vector2(-runningSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(runningSpeed, rb.velocity.y);
                }
            }
        }
    } /*Entire run mechanism*/

    void wholeWalkMechanism()
    {
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            isWalking = false;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !isDashing && !AttackArea.beingKnockedBack)
        {
            rb.velocity = new Vector2(walkingSpeed, rb.velocity.y);
            sr.flipX = false;
            isWalking = true;
        } /* Walking right */

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        } /* Stop walking */

        if (Input.GetKey(KeyCode.LeftArrow) && !isDashing && !AttackArea.beingKnockedBack)
        {
            rb.velocity = new Vector2(-walkingSpeed, rb.velocity.y);
            sr.flipX = true;
            isWalking = true;
        } /* Walking left */
    } /*Entire walk mechanism*/

    void wholeSneakMechanism()
    {
        if (canSneak)
        {
        }
    } /*Entire sneak mechanism*/

    void fallChecker()
    {
        if (rb.velocity.y < 0)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    } /*Controls isFalling boolean variable*/

    /* -------------------------------------------------------- */
}

