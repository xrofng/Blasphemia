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
    public bool isOnSlope = false;
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
        doublieJumpSpeed = 80 * (jumpSpeed / 100);
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
        }
        else
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
            if (Input.GetAxis("Horizontal") > 0)
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
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            if (isOnSlope == true)
            {
                transform.Translate(Vector3.right * speed * 4 * Time.deltaTime);
                transform.Translate(Vector3.up * speed * 4 * Time.deltaTime);
            } else
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
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
            if (Input.GetButtonDown("Jump"))
            {
                if (isOnGround == true || isOnSlope == true)
                {
                    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
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
                    print("nor");
                }
            }
            if (Input.GetButtonDown("Jump") && isOnGround != true && doublejump == false && isOnSlope == false)
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
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
                }
                else
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
                transform.Translate(Vector2.up * Time.deltaTime * climbVelo);

            }


        }
        if (isOnLadder == false)
        {
            rid2d.gravityScale = gravityStore;
        }
    }

    void Attack()
    {



        if (Input.GetButtonDown("Attack") && isOnGround == true && animator.GetInteger("isAttack") == 0 && isAttacking == false)
        {
            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                animator.SetInteger("isAttack", 1);
            }
            else
            {
                animator.SetInteger("isAttack", 2);
            }
            isAttacking = true;

        }

        if (isAttacking == true)
        {
            attackTimeCount -= Time.deltaTime;
            if (attackTimeCount < 0)
            {
                isAttacking = false;
                attackTimeCount = attackTime;
                animator.SetInteger("isAttack", 0);
            }
        }

    }



    void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Enemy")
        {
            countAJ = 0;
            jumping = false;
            doublejump = false;
            isOnGround = true;
            isOnSlope = false;
        }
        else if (other.gameObject.tag == "Slope")
        {
            isOnSlope = true;
            countAJ = 0;
            jumping = false;
            doublejump = false;
            isOnGround = true;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            isOnSlope = false;
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

        if (other.gameObject.tag == "Slope")
        {
            isOnGround = false;
            countAJ = 0;
            startCountTime = true;
            isOnSlope = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

    
}