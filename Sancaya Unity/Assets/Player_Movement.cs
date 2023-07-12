using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Animator animator;

   private float horizontal;
   private float speed = 8f;
   private float jump_power = 14f;
   private bool menghadapKanan = true;

   //DASHING Click Detector
   private float lastPressedTime;
   private float timeBetweenClick = 0.25f;
   private bool pressedFirstTime = true;
   private int buttonCount = 0;

    // DASHING FUNCTION
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCD = 1f;

    //TouchScreen
    private float screenWidth;
    private float screenHeight;
    private int arahTouch = 0;
    private float timeBetweenTouch = 0.25f;



   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Transform groundCheck;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private TrailRenderer tr;


    void Start ()
    {
        screenWidth = Screen.width/2;
    }
   
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if ((Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)))
        {
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        arahMenghadap();
        if (horizontal == 0)
        {
            animator.SetBool("isRunning", false);
        }else
        {
            animator.SetBool("isRunning", true);
        }
        
        if (horizontal < 0f)
        {
            transform.eulerAngles = new Vector3 (0,180,0);
        }else if (horizontal > 0f)
        {
            transform.eulerAngles = new Vector3 (0,0,0);
        }
        
        if (Input.GetKeyDown("w") && diTanah())
        {
            animator.SetTrigger("takeOff");
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2 (rb.velocity.x, jump_power);
        } else
        {
            animator.SetBool("isJumping", true);
            animator.SetTrigger("fall");
        }
        if (Input.GetKeyUp("w") && rb.velocity.y > 0f)
        {
            animator.SetTrigger("fall");
            animator.SetBool("isJumping", false);
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * 0.4f);
        }
        if (diTanah() == true)
        {
            animator.SetBool("isJumping", false);
        }
       
        if (Input.GetKeyDown(KeyCode.D) && canDash)
        {
            if (pressedFirstTime)
            {
                bool isDoublePress = Time.time - lastPressedTime <= timeBetweenClick;

                if (isDoublePress)
                {
                    StartCoroutine(Dash());
                    pressedFirstTime = false;
                }
            } else
            {
                pressedFirstTime = true;
            }
            lastPressedTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.A) && canDash)
        {
            if (pressedFirstTime)
            {
                bool isDoublePress = Time.time - lastPressedTime <= timeBetweenClick;

                if (isDoublePress)
                {
                    StartCoroutine(Dash());
                    pressedFirstTime = false;
                }
            } else
            {
                pressedFirstTime = true;
            }
            lastPressedTime = Time.time;
        }
        ///// Touch control
        touchControl();
        arahMenghadapTouch();
        if (arahTouch < 0f)
        {
            transform.eulerAngles = new Vector3 (0,180,0);
        }else if (arahTouch> 0f)
        {
            transform.eulerAngles = new Vector3 (0,0,0);
        }
        if (arahTouch == 0)
        {
            animator.SetBool("isRunning", false);
        }else
        {
            animator.SetBool("isRunning", true);
        }
                    ////Lompat in Touch Control
                    Debug.Log(Input.touchCount);

        
        
        

    }

     private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);
        rb.velocity = new Vector2(arahTouch*speed, rb.velocity.y);
    }


    private bool diTanah()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void arahMenghadap()
    {
        if (menghadapKanan && horizontal < 0f || !menghadapKanan && horizontal > 0f )
        {
            menghadapKanan = !menghadapKanan;
            Vector3 localScale = transform.localScale;
            localScale.x = localScale.x * -1f;
            transform.localScale = localScale;
        }
        
    }

    //DASHING
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (horizontal < 0f)
        {
            rb.velocity = new Vector2 (transform.localScale.x * dashingPower * -1f, 0f);
        }else
        {
            rb.velocity = new Vector2 (transform.localScale.x * dashingPower, 0f);
        }
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCD);
        canDash = true;
    }
    
    private IEnumerator DashTouch()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (arahTouch < 0f)
        {
            rb.velocity = new Vector2 (transform.localScale.x * dashingPower * -1f, 0f);
        }else
        {
            rb.velocity = new Vector2 (transform.localScale.x * dashingPower, 0f);
        }
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        arahTouch = 0;
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCD);
        canDash = true;
    }

    //TOUCH CONTROL
    private void touchControl()
    {
        if (Input.touchCount > 0)
        {
             for (int i =0; i<Input.touchCount; i++ )
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.position.x > screenWidth)
                     {
                        arahTouch = 1;
                        if (touch.phase == TouchPhase.Ended)
                           {
                             arahTouch = 0;
                             break;
                           }
                    }  
                    if (touch.position.x < screenWidth)
                     {
                        arahTouch = -1;
                        if (touch.phase == TouchPhase.Ended)
                           {
                             arahTouch = 0;
                             break;
                           }
                    }
                    if (Input.touchCount == 2 && diTanah())
                    {
                        if(diTanah())
                        {
                        animator.SetTrigger("takeOff");
                        animator.SetBool("isJumping", true);
                        rb.velocity = new Vector2 (rb.velocity.x, jump_power);
                        }else
                        {
                        animator.SetBool("isJumping", true);
                        animator.SetTrigger("fall");
                        }
                    }
                    if ((touch.position.x > screenWidth) && canDash && Input.GetTouch(0).tapCount==2)
                    {
                        if (pressedFirstTime)
                            {
                                bool isDoublePress = Time.time - lastPressedTime <= timeBetweenTouch;

                                if (isDoublePress)
                                    {
                                        StartCoroutine(DashTouch());
                                        
                                        pressedFirstTime = false;
                                    }
                            } else
                            {
                                pressedFirstTime = true;
                            }
                            lastPressedTime = Time.time;
                    }
                    if ((touch.position.x < screenWidth) && canDash && Input.GetTouch(0).tapCount==2)
                    {
                        if (pressedFirstTime)
                            {
                                bool isDoublePress = Time.time - lastPressedTime <= timeBetweenTouch;

                                if (isDoublePress)
                                    {
                                        StartCoroutine(DashTouch());
                                        pressedFirstTime = false;
                                    }
                            } else
                            {
                                pressedFirstTime = true;
                            }
                            lastPressedTime = Time.time;
                    }    
                    break;
                }
        }
    }

    private void lompatTouch()
    {
        if (Input.touchCount == 2)
         { 
            if (diTanah())
            {
                animator.SetTrigger("takeOff");
                animator.SetBool("isJumping", true);
                rb.velocity = new Vector2 (rb.velocity.x, jump_power);
            } else
            {
                animator.SetBool("isJumping", true);
                animator.SetTrigger("fall");
            }
            
         }
    }


    private void arahMenghadapTouch()
    {
        if (menghadapKanan && arahTouch < 0f || !menghadapKanan && arahTouch > 0f )
        {
            menghadapKanan = !menghadapKanan;
            Vector3 localScale = transform.localScale;
            localScale.x = localScale.x * -1f;
            transform.localScale = localScale;
        }
        
    }
}
