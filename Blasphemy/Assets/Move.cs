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
    public int speedExitLadder;

    //untilities
    private float gravityStore;

    //weapon
    public GameObject weapon;

    private bool IngamePut;
    private float countAJ;
    private bool doublejump = false;
    private float supportForce;
    public bool isAttacking;
    
    public float attackTime;
    public float jattackTime;
    private float attackTimeCount;


    //skill
    private bool doubleJumpAbilities;
    private bool cameraPeekAbilities;

    private Rigidbody2D rid2d;
    private Animator animator;
   
    private Ouros Ouros;
    
    public void setIngamePut(bool i)
    {
        IngamePut = i;
    }

    void Start()
    {
        doubleJumpAbilities = false;
        cameraPeekAbilities = false;
        doublieJumpSpeed = 80 * (jumpSpeed / 100);
        maxSpeed = speed;        
        rid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityStore = rid2d.gravityScale;
        Ouros = GetComponent<Ouros>();
        IngamePut = true;
    }

    void Update()
    {   
        if (IngamePut == true)
        {
            if (isOnLadder == true && isOnGround == false)
            {

            }
            else
            {               
                Walk();
                checkIdling();
                Jump();
                Attack();                
            }
            
            Ladder();
            OurosUse();
        }      
        
    }

    void FixedUpdate()
    {

        if (startCountTime == true)
        {
            JumpCountTime();
        }
    }

    //animator
    public static int state_Idle = 0; //
    public static int state_Walk = 1; //
    public static int state_Jump = 2; //
    public static int state_GroundSlash = 3; //
    public static int state_AirSlash = 4;//
    public static int state_GroundMagic = 5;//
    public static int state_AirMagic = 6;
    //public static int state_GroundThrust = 7;
    //public static int state_AirThrust = 8;
    public static int state_Ladder = 9; //
    public static int state_Item = 10; //

    public int _currentAnimationState = state_Idle;
    public void changeState(int stateI)
    {
        if (_currentAnimationState == stateI)
        {
            return;
        }
        else
        {
            animator.SetInteger("state", stateI);
            print(stateI);
            _currentAnimationState = stateI;
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
            if (isOnGround == false)
            {
                if(isAttacking == true)
                {
                    
                } else
                {
                    changeState(state_Jump);
                }               
            } else
            {
                if (isAttacking == true)
                {

                }
                else
                {
                    changeState(state_Walk);
                }                
            }            
            speed = maxSpeed * Input.GetAxis("Horizontal");
            weaponCollision(GetComponent<SpriteRenderer>().flipX);
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            changeState(state_Idle);
            float direction = Input.GetAxis("Horizontal");
            if (Input.GetAxis("Horizontal") > 0 && jumping == false)
            {
                rid2d.AddForce(new Vector2(speed * direction * smooth, rid2d.velocity.y));
            }
            else if (Input.GetAxis("Horizontal") < 0 && jumping == false)
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
            } else
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                weapon.GetComponent< SpriteRenderer > ().flipX = false;
                weaponCollision(false);
                GetComponent<SpriteRenderer>().flipX = false;
                
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            if (isOnSlope == true)
            {
                transform.Translate(Vector3.right * speed * 4 * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                weapon.GetComponent<SpriteRenderer>().flipX = true;
                weaponCollision(true);
                GetComponent<SpriteRenderer>().flipX = true;
               
            }
        }

    }

    void weaponCollision(bool flipX)
    {
        if (flipX == true)
        {
            if (weapon.GetComponent<BoxCollider2D>().offset.x > 0)
            {
                weapon.GetComponent<BoxCollider2D>().offset = new Vector2(-2f, weapon.GetComponent<BoxCollider2D>().offset.y);
            }
        } else if (flipX == false)
        {
            if (weapon.GetComponent<BoxCollider2D>().offset.x < 0)
            {
                weapon.GetComponent<BoxCollider2D>().offset = new Vector2(2f, weapon.GetComponent<BoxCollider2D>().offset.y);
            }
        }
        

    }

    void checkIdling()
    {
        if (isOnGround == true && Input.GetButton("Horizontal") == false)
        {
            jumping = false;
            changeState(state_Idle);
        }
        else if (isOnGround == false && Input.GetButton("Horizontal") == false)
        {
            changeState(state_Jump);
        }
        else if (isOnGround == true && Input.GetButton("Horizontal") == true)
        {
            jumping = false;
            changeState(state_Walk);
        }
    }

    void Jump()
    {
        
        if (isOnLadder == false && isAttacking == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                changeState(state_Jump);
                if (isOnGround == true || isOnSlope == true)
                {                                       
                    countAJ = 0;
                    startCountTime = true;

                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        changeState(state_Jump);
                        rid2d.AddForce(new Vector2(jumpDistance, jumpSpeed));
                    }
                    else if (Input.GetAxisRaw("Horizontal") < 0)
                    {
                        changeState(state_Jump);
                        rid2d.AddForce(new Vector2(-jumpDistance, jumpSpeed));
                    }
                    else
                    {
                        rid2d.AddForce(new Vector2(0f, jumpSpeed));
                    }
                   
                }
            }
            if (doubleJumpAbilities == true)
            {
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
                }
            }
           
        }

    }

    void Ladder()
    {
        if (isOnLadder == true)
        {
            if (Input.GetButton("Vertical"))
            {
                changeState(state_Ladder);
                isOnGround = false;
               
                rid2d.gravityScale = 0f;
                climbVelo = climbSpeed * Input.GetAxisRaw("Vertical");
                transform.Translate(Vector2.up * Time.deltaTime * climbVelo);
            }
            float direction = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                rid2d.gravityScale = 0f;
                if (Input.GetAxis("Horizontal") > 0)
                {
                    rid2d.AddForce(new Vector2(speedExitLadder * direction  , rid2d.velocity.y));
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    rid2d.AddForce(new Vector2(speedExitLadder * direction , rid2d.velocity.y));
                }
            }          
        }
        if (isOnLadder == false)
        {
            rid2d.gravityScale = gravityStore;
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack") && isOnGround == true && animator.GetInteger("state") != 3 && isAttacking == false)
        {
            isAttacking = true;
            changeState(state_GroundSlash);             
            attackTimeCount = attackTime;
           
        } else if (Input.GetButtonDown("Attack") && isOnGround == false && animator.GetInteger("state") != 3 && isAttacking == false)
        {
            isAttacking = true;
            changeState(state_AirSlash);            
            attackTimeCount = jattackTime;
            
        }
       
        if (isAttacking == true)
        {
            attackTimeCount -= Time.deltaTime;
            if (attackTimeCount < 0)
            {
                isAttacking = false;                
               
            }
        }

    }

    void OurosUse()
    {
        if (Input.GetButtonDown("Special") && Ouros.magicActive == true && isOnGround == true)
        {
            changeState(state_GroundMagic);
        }
        if (Input.GetButtonDown("Special") && Ouros.magicActive == true && isOnGround != true)
        {
            changeState(state_AirMagic);
        }
        if (Input.GetButtonDown("Item") && isOnGround == true)
        {
            

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
            changeState(state_Idle);
        }
        else if (other.gameObject.tag == "Slope")
        {
            isOnSlope = true;
            countAJ = 0;
            jumping = false;
            doublejump = false;
            isOnGround = true;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            changeState(state_Idle);
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
            if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Enemy")
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
        }

    }

    
}