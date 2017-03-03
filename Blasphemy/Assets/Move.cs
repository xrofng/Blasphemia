using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{    
    public float speed;
    public float maxSpeed;
    public float smooth;
    //aboutjump
    public float jumpSpeed;
    public float jumpDistance;
    private float doublieJumpSpeed;
    
    //status bool
    public bool jumping;
    public bool isOnGround;
    public bool isOnLadder;
    public bool isOnSlope;
    private bool startCountTime;

    //ladder
    public float climbSpeed;
    private float climbVelo;
    
    //untilities
    private float gravityStore;
    
    private float countAJ;
    private bool doublejump = false;
    private float supportForce;
    public bool isAttacking;
    public float attackTime;
    private float attackTimeCount;
    
    private Rigidbody2D rid2d;
    private Animator animator;


   
    void Start()
    {
        doublieJumpSpeed = 80*(jumpSpeed/100);
        maxSpeed = speed;
        attackTimeCount = attackTime;
        rid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityStore = rid2d.gravityScale;
    }
    
    void Update()
    {
        Attack();
        Walk();
        Jump();
        Ladder();
    }

    void FixedUpdate()
    {
        
        if (startCountTime == true)
        {
            JumpCountTime();
        }
    }



    void JumpCountTime()
    {
        if (countAJ > 0.3)
        {
            jumping = true;
        } else
        {
            jumping = true;
        }
        countAJ += Time.deltaTime;
    }

    void Walk()
    {
        
        if (Input.GetButton("Horizontal"))
        {
            //smoothy accel
            speed = maxSpeed * Input.GetAxis("Horizontal");
        } 
        if (Input.GetButtonUp("Horizontal"))
        {   
            //smoothy stop
            float direction = Input.GetAxis("Horizontal");
            if(Input.GetAxis("Horizontal") > 0)
            {
                rid2d.AddForce(new Vector2(speed * direction * smooth, rid2d.velocity.y));
            }
           else if (Input.GetAxis("Horizontal") < 0)
            {
                rid2d.AddForce(new Vector2(-speed * direction * smooth, rid2d.velocity.y));
            }
        }
        //move
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            
            transform.Translate(Vector3.right*speed * Time.deltaTime);
            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate(Vector3.right*speed * Time.deltaTime);
            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    void Jump()
    {
        
        if (isOnLadder == false)
        {
            if (Input.GetButtonDown("Jump") && isOnGround == true)
            {
                countAJ = 0;
                startCountTime = true;
               
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    rid2d.AddForce(new Vector2(jumpDistance, jumpSpeed));
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    rid2d.AddForce(new Vector2(-jumpDistance, jumpSpeed));
                }
                else
                {
                    rid2d.AddForce(new Vector2(0f, jumpSpeed));
                }
            }
            if (Input.GetButtonDown("Jump") && isOnGround != true && doublejump == false)
            {
                
                startCountTime = false;
                if (countAJ > 0.44)
                {
                    float ajPercent = countAJ * (100 / 0.44f);
                    supportForce = doublieJumpSpeed * (ajPercent / 100);
                }
                else if (countAJ < 0.44)
                {
                    float ajPercent = countAJ * (100 / 0.44f);
                    supportForce = doublieJumpSpeed * (ajPercent / 100);
                } else
                {

                    supportForce = doublieJumpSpeed;
                }
               
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    rid2d.AddForce(new Vector2(jumpDistance, supportForce));
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    rid2d.AddForce(new Vector2(-jumpDistance, supportForce));
                }
                else
                {
                    rid2d.AddForce(new Vector2(0f, supportForce));
                }
                doublejump = true;
                print(supportForce);
            }
        }

    }

    void Ladder()
    {
        if (isOnLadder == true)
        {
            if (Input.GetButton("Vertical"))
            {
                Debug.Log("Ladder");
                rid2d.gravityScale = 0f;
                climbVelo = climbSpeed * Input.GetAxisRaw("Vertical");
                transform.Translate(Vector2.up* Time.deltaTime* climbVelo);

            }
            
            
        }
        if (isOnLadder == false)
        {
            rid2d.gravityScale = gravityStore;
        }
    }

    void Attack()
    {
        if (animator.GetInteger("isAttack") == 1)
        {
            animator.SetInteger("isAttack", 0);
     
        }


        if (Input.GetButtonDown("Attack") && isOnGround == true && animator.GetInteger("isAttack") == 0 && isAttacking == false)
        {
            animator.SetInteger("isAttack", 1);
            isAttacking = true;

        }

        if (isAttacking == true)
        {
            attackTimeCount -= Time.deltaTime;
            if (attackTimeCount < 0)
            {
                isAttacking = false;
                attackTimeCount = attackTime;
            }
        }
               
    }


    void OnCollisionStay2D(Collision2D other)
    {
       
        if (other.gameObject.tag == "Platform")
        {
            countAJ = 0;
            jumping = false;
            doublejump = false;
            isOnGround = true;
        } else
        {
            
        }
        
    }


    void OnCollisionExit2D(Collision2D other)
    {
        if (jumping == true)
        {
            if (other.gameObject.tag == "Platform")
            {
                isOnGround = false;
                countAJ = 0;
                startCountTime = true;
            }
        }
    

    }


}