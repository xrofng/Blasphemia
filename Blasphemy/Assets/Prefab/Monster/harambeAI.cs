using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harambeAI : MonoBehaviour
{
    public string NAME;
    public string type;
    public int ATK;
    public int HP;
    public int DEF;
    public float mySpeed;

    public float ranTime;
    private float ranCount;
    private int ran;

    public float sightXM;
    public float sightYM;

    public float sightXR;
    public float sightYR;
        
    public bool inSight;

    public bool range = true;
    public bool melee = true;

    private float timecount = 0.0f;
    public float delayMelee = 3.0f;
    public float delayRange = 4.0f;
    private Vector2 offetX;
    public float faceDelay;
    private float faceCount;
    public int direction = 1;
    private bool summoned = false;
    private Ouros Ouros;
    public enemySpell snowball;
    public Transform spawnPointR;
    public Transform spawnPointL;
    private Rigidbody2D rid2d;
    private Animator animator;
    public BoxCollider2D meleeCollider;

    public bool red = false;
    public bool isInvisible;
    public bool isDead;
    public float deadCount;
    public float deadTime;
    private float invicount;
    public float invitime;
    public Color inviColor;
    public Color normalColor;
    public Color damagedColor;

    public int _currentAnimationState = 0;
    public void changeState(int stateI)
    {
        if (_currentAnimationState == stateI)
        {
            return;
        }
        else
        {
            animator.SetInteger("state", stateI);
            _currentAnimationState = stateI;
        }
    }

   

    void checkRange()
    {
        if (Ouros.GetComponent<Transform>().position.y < this.GetComponent<Transform>().position.y + sightYR)
        {
            if (Ouros.GetComponent<Transform>().position.y > this.GetComponent<Transform>().position.y - sightYR)
            {
                if (Ouros.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x - sightXR)
                {
                    if (Ouros.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x + sightXR)
                    {
                        range = true;
                        checkMelee();
                        return;
                    }
                }
            }
        }
        range = false;
     //   melee = false;
    }

    void checkMelee()
    {
        if (Ouros.GetComponent<Transform>().position.y < this.GetComponent<Transform>().position.y + sightYM)
        {
            if (Ouros.GetComponent<Transform>().position.y > this.GetComponent<Transform>().position.y - sightYM)
            {
                if (Ouros.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x - sightXM)
                {
                    if (Ouros.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x + sightXM)
                    {
                        melee = true;
                        //range = false;
                        return;
                    }
                }
            }
        }
        melee = false;
    }
    void fistFlip()
    {
        if (this.GetComponent<SpriteRenderer>().flipX == true)
        {
            if (meleeCollider.offset.x > 0)
            {
                meleeCollider.offset = new Vector2(offetX.x*-1, offetX.y);
            }
        }
        else if (this.GetComponent<SpriteRenderer>().flipX == false)
        {
            if (meleeCollider.offset.x < 0)
            {
                meleeCollider.offset = new Vector2(offetX.x, offetX.y);
            }
        }
    }

    void ranAction()
    {
        ranCount += Time.deltaTime;
        if (ranCount > ranTime)
        {
            ranCount = 0;
            ran = Random.Range(0, 5);
        }
        
    }

    void EnemyAction()
    {
        fistFlip();
        //facePlayer();
        checkRange();
        //checkMelee();
        ranAction();
        if (ran == 0)
        {
            if(GetComponent<SpriteRenderer>().flipX == true)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                direction = 1;
            } else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                direction = -1;
            }
            ran = Random.Range(0, 5);
        }
        else if (ran == 2 ) 
        {
            changeState(0);

        } else if (ran == 4 || ran == 5 ||  ran == 3)
        {
            attackPattern();
        }
        
        transform.Translate(Vector3.right * mySpeed * direction * Time.deltaTime);
    }
    void attackPattern()
    {
        facePlayer();
        if (melee == true)
        {
            changeState(1);
            timecount += Time.deltaTime;
            if (timecount > 2.0f)
            {
                // changeState(1);
                meleeCollider.enabled = true;
                timecount = 0.0f;
            }
            else if (timecount < 2.0f)
            {
                meleeCollider.enabled = false;
            }
        }
        else if (range == true && melee == false)
        {
            changeState(2);
            timecount += Time.deltaTime;
            if (timecount > delayRange)
            {
                if (summoned == false)
                {
                    //changeState(2);
                    snowball.Dmg = this.ATK;
                    if (direction == -1)
                    {
                        Instantiate(snowball, spawnPointL.transform.position, spawnPointL.transform.rotation);
                    }
                    if (direction == 1)
                    {
                        Instantiate(snowball, spawnPointR.transform.position, spawnPointR.transform.rotation);
                    }
                    snowball.setDirection(direction);
                    timecount = 0.0f;
                }
            }
        }
    }



    // Use this for walkDurationialization
    void Start()
    {
        Ouros = FindObjectOfType<Ouros>();
        rid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
      
        meleeCollider.GetComponent<enemyMelee>().ATK = this.ATK;
        offetX = meleeCollider.offset;
        timecount = 0;
    }

    void checkDie()
    {
        if (this.HP <= 0)
        {
            isDead = true;
        }

        if (Ouros.red == true)
        {
            ran = 2;
        }
    }
    void die()
    {
        if (isDead == true)
        {
            deadCount += Time.deltaTime;
            if (deadCount > deadTime)
            {
                this.GetComponent<itemDrop>().enabled = true;
            }
        }
    }

    void facePlayer()
    {
        faceCount += Time.deltaTime;
        if (faceCount > faceDelay)
        {
            if (Ouros.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x)
            {
                direction = -1;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Ouros.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x)
            {
                direction = 1;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            faceCount = 0;
        }

    }

    void checkInsight()
    {
        if (Ouros.GetComponent<Transform>().position.y < this.GetComponent<Transform>().position.y + sightYR)
        {
            if (Ouros.GetComponent<Transform>().position.y > this.GetComponent<Transform>().position.y - sightYR)
            {
                if (Ouros.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x - sightXR)
                {
                    if (Ouros.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x + sightXR)
                    {
                        inSight = true;
                        return;
                    }
                }
            }
        }
        inSight = false;
    }
    public void blinking()
    {
        if (isInvisible == true)
        {

            if (invicount > invitime - 0.50 && red == false)
            {
                if (GetComponent<SpriteRenderer>().material.color == damagedColor)
                {
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", normalColor);
                    red = true;
                }
            }
            invicount += Time.deltaTime;
            if (red == true)
            {
                if (invicount > invitime)
                {
                    invicount = 0;
                    red = false;
                    isInvisible = false;
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", normalColor);
                    return;
                }

                if (GetComponent<SpriteRenderer>().material.color == inviColor)
                {
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", normalColor);
                }
                else if (GetComponent<SpriteRenderer>().material.color == normalColor)
                {
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", inviColor);
                }
            }


        }
    }
    public void knockBack(float dis)
    {
        GetComponent<SpriteRenderer>().material.SetColor("_Color", damagedColor);
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            // this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * dis);
            transform.Translate(Vector3.right * dis);
        }
        else if (GetComponent<SpriteRenderer>().flipX == false)
        {
            // this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.left * dis);
            transform.Translate(Vector3.left * dis);
        }
        isInvisible = true;
    }

    void Update()
    {
        checkInsight();
        checkDie();
        die();
        blinking();
        if (inSight == true && isInvisible == false)
        {
            EnemyAction();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "PlayerWeapon" && isInvisible == false)
        {
            HP -= Ouros.ATK - this.DEF / Ouros.ATK;
            Instantiate(Ouros.attackPar, other.transform.position, other.GetComponent<Transform>().rotation);
            other.enabled = false;
            knockBack(1.0f);
        }
        else if (other.gameObject.tag == "Magic" && isInvisible == false)
        {
            HP -= Ouros.ATK - this.DEF / Ouros.ATK;
            Instantiate(Ouros.attackPar, other.transform.position, other.GetComponent<Transform>().rotation);
            other.enabled = false;
            Destroy(other.gameObject);
            knockBack(1.0f);
        }
        else
        {

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            ran = Random.Range(2, 5);
        }
        else
        {

        }
    }
}
