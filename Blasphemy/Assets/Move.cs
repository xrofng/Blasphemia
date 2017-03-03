using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{    
    public float speed;
    public float speed2;
    public float smooth;

    public float jumpSpeed;
    public float jumpDistance;

    // private Vector3 moveDirection = Vector3.zero;
    public bool isOnGround;
    public bool isOnLadder;
    public float climbSpeed;
    private float climbVelo;
    private float gravityStore;

    private bool doublejump = false;

    public bool isAttacking;
    public float attackTime;
    private float attackTimeCount;

    private Rigidbody2D rid2d;
    private Animator animator;


   
    void Start()
    {
        speed2 = speed;
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



    void Walk()
    {
        
        if (Input.GetButton("Horizontal"))
        {
            //smoothy accel
            speed = speed2 * Input.GetAxis("Horizontal");
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
                doublejump = true;
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

            doublejump = false;
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
        
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isOnGround = false;
        }
       

    }


}