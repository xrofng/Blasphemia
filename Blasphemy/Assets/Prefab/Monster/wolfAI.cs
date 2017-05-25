using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfAI : MonoBehaviour
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
    public float sightXMII;
    public float sightYM;
    public float sightXR;
    public float sightYR;

    public float howlDuration;
    private bool howling;
    private float howlCound;
    private bool stornSpawned;
    public BoxCollider2D snowStorm;
    public Transform spawnPoint;

    public BoxCollider2D meleeCollider;

    public int currentAction;

    public float diveDuration;
    private bool diving;
    private float diveCound;

    public float runDuration;
    private bool running;
    private float runCound;

    public float strikeDuration;    
    private bool striking;
    private float strikeCound;
   

    public bool inSight;

    public bool range = true;
    public bool melee = true;
    public bool meleeII = true;

    private float timecount = 0.0f;
    public float delayClaw = 3.0f;
    public float delayRange = 4.0f;
    private Vector2 offetX;
    public float faceDelay;
    private float faceCount;
    public int direction = 1;
    public float jumpspeed;
    private Ouros Ouros;
    
    
    
    private Rigidbody2D rid2d;
    private Animator animator;
    

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
    void checkMeleeII()
    {
        if (Ouros.GetComponent<Transform>().position.y < this.GetComponent<Transform>().position.y + sightYM)
        {
            if (Ouros.GetComponent<Transform>().position.y > this.GetComponent<Transform>().position.y - sightYM)
            {
                if (Ouros.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x - sightXMII)
                {
                    if (Ouros.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x + sightXMII)
                    {
                        meleeII = true;
                        //range = false;
                        return;
                    }
                }
            }
        }
        meleeII = false;
    }
    void clawFlip()
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

   

    void EnemyAction()
    {
        clawFlip();
        checkMelee();
        checkMeleeII();
        if (ran >= 50 && ran < 90) // jump2
        {
            if (meleeII == true)
            {
                currentAction = 2;
            }
            else
            {
                currentAction = 0;
            }
            
        }
        if (ran >= 0 && ran < 50) //run 0
        {
            if (melee == true && meleeII == false)
            {
                
                currentAction = 1;     
            }else
            {                
                currentAction = 0;
            }
        }  if (ran >= 90 && ran < 100)// howl 3
        {
            currentAction = 3;         
        }

        if (currentAction == 0)
        {

            if (running == false)
            {
               
                running = true;
                diving = false;
                ranTime = Random.Range(5, 8);
                changeState(0);               
            }
            else
            {
                transform.Translate(Vector3.right * direction * mySpeed * Time.deltaTime);
                runCound += Time.deltaTime;
                if (runCound >= runDuration)
                {
                    runCound = 0;
                    running = false;
                    ran = Random.Range(0, 100);
                    meleeCollider.enabled = false;
                }
            }
        } else if (currentAction == 1)
        {
            rid2d.AddForce(new Vector2(0, jumpspeed));
            if (diving == false)
            {
                ranTime = diveDuration;
                diving = true;
                running = false;                
               
                changeState(2);
               
            }
            else
            {
                timecount += Time.deltaTime;
                if (timecount > delayClaw)
                {
                    timecount = 0;
                    meleeCollider.enabled = true;
                }
                transform.Translate(Vector3.right * direction * mySpeed * Time.deltaTime);
                diveCound += Time.deltaTime;
                if (diveCound >= diveDuration)
                {
                    meleeCollider.enabled = false;
                    diveCound = 0;
                    diving = false;
                
                    ran = Random.Range(0, 100);
                }
            }
        } if (currentAction == 2)
        {
            if (striking == false)
            {
                facePlayer();
                striking = true;
                ranTime = strikeDuration;
              
                changeState(1);
            }
            else
            {
                strikeCound += Time.deltaTime;
                if (strikeCound >= strikeDuration)
                {
                    meleeCollider.enabled = false;
                    strikeCound = 0;
                    striking = false;
                    ran = Random.Range(0, 100);
                }
            }
        } if ( currentAction == 3)
        {
            if (howling == false)
            {
                facePlayer();
                howling = true;
                ranTime = howlDuration;
                changeState(3);
                Instantiate(snowStorm);
            }else
            {
                howlCound += Time.deltaTime;
                if(howlCound >= howlDuration)
                {
                    meleeCollider.enabled = false;
                    howlCound = 0;
                    howling = false;
                    ran = Random.Range(0, 89);
                    facePlayer();
                }
            }
        }


        if (currentAction != 0)
        {
            runCound = 0;
        }
        if (currentAction != 1)
        {
            diveCound = 0;
        }
        if (currentAction != 2)
        {
            strikeCound = 0;
        }
        if (currentAction != 3)
        {
            howlCound = 0;
        }
    }
   



    // Use this for walkDurationialization
    void Start()
    {
        Ouros = FindObjectOfType<Ouros>();
        rid2d = GetComponent<Rigidbody2D>();
          animator = GetComponent<Animator>();
        ran = 0;
        meleeCollider.GetComponent<enemyMelee>().ATK = this.ATK;
        offetX = meleeCollider.offset;
        timecount = 0;
        facePlayer();
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
            
            transform.Translate(Vector3.right * dis);
        }
        else if (GetComponent<SpriteRenderer>().flipX == false)
        {
          
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
        else if (other.gameObject.tag == "Wall")
        {
            facePlayer();
        }
    }
}
