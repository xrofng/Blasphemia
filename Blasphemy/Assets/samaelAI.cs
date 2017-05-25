using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class samaelAI : MonoBehaviour
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



    private bool actioning;
    public enum ACTION { IDLE, RUN, ARROW, SPEAR, SCIMITAR, BLADE };
    public ACTION currentAction;
    public float idleDuration;
    private bool idlinging;
    private float idleCound;

    public float runDuration;
    private bool running;
    private float runCound;

    public float arrowDuration;
    private bool arrowing;
    private float arrowCound;
    
    public float spearDuration;
    private bool spearing;
    private float spearCound;

    public float scimDuration;
    private bool sciming;
    private float scimCound;

    public float bladeDuration;
    private bool blading;
    private float bladeCound;
    public GameObject arrow;
    public GameObject spear;
    public GameObject spearI;
    public GameObject spearII;
    public GameObject scimitar;
    public GameObject blade;
    public int arrow_ammount;
    public int maxArrow;
    public float spearAbove;
    public bool inSight;
    public GameObject floorlevel;

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
 


    void EnemyAction()
    {
        
        checkMelee();
        checkMeleeII();
        if (actioning == false)
        {
            actioning = true;
            if (melee == false && meleeII == false)
            {
                if (ran >= 0 && ran < 20)//walk
                {
                    currentAction = ACTION.RUN;
                }
                if (ran >= 20 && ran < 30)//idle
                {
                    currentAction = ACTION.IDLE;
                }
                if (ran >= 30 && ran < 70) // blade
                {
                    currentAction = ACTION.BLADE;
                }
                else if (ran >= 70 && ran < 100)// arrow
                {
                    currentAction = ACTION.ARROW;
                }
            }
            if (melee == false && meleeII == true)
            {
                if (ran >= 0 && ran < 20)//walk
                {
                    currentAction = ACTION.RUN;
                }
                if (ran >= 20 && ran < 30)//idle
                {
                    currentAction = ACTION.IDLE;
                }
                if (ran >= 30 && ran < 50) // blade
                {
                    currentAction = ACTION.BLADE;
                }
                if (ran >= 50 && ran < 80) // scimitar
                {
                    currentAction = ACTION.SCIMITAR;
                }
                else if (ran >= 80 && ran < 100)// arrow
                {
                    currentAction = ACTION.ARROW;
                }
            }
            if (melee == true && meleeII == true)
            {
                if (ran >= 0 && ran < 10)//walk
                {
                    currentAction = ACTION.RUN;
                }
                if (ran >= 10 && ran < 40)//spear
                {
                    currentAction = ACTION.SPEAR;
                }
                if (ran >= 40 && ran < 60) // blade
                {
                    currentAction = ACTION.BLADE;
                }
                if (ran >= 60 && ran < 80) // scimitar
                {
                    currentAction = ACTION.SCIMITAR;
                }
                else if (ran >= 80 && ran < 100)// arrow
                {
                    currentAction = ACTION.ARROW;
                }
            }
        }
        

        if (currentAction == ACTION.IDLE)
        {
            if (idlinging == false)
            {
                idlinging = true;      
                changeState(0);
            }
            else
            {
                idleCound += Time.deltaTime;
                if (idleCound >= idleDuration)
                {
                    idleCound = 0;
                    idlinging = false;
                    actioning = false;
                    ran = Random.Range(0, 100);
                    
                }
            }
        }else if (currentAction == ACTION.RUN)//walk
        {
            if (running == false)
            {
                running = true;
                changeState(1);
            }
            else
            {
                transform.Translate(Vector3.right * direction * mySpeed * Time.deltaTime);
                runCound += Time.deltaTime;
                if (runCound >= runDuration)
                {
                    runCound = 0;
                    running = false;
                    actioning = false;
                    ran = Random.Range(0, 100);
                }
            }
           
        }
        else if (currentAction == ACTION.ARROW)//arrow
        {
            if (arrowing == false)
            {
                arrowing = true;
                changeState(2);
            }
            else
            {
                if (arrow_ammount < maxArrow)
                {
                    Instantiate(arrow, this.transform.position, arrow.transform.rotation);
                    arrow_ammount += 1;
                }                  
                arrowCound += Time.deltaTime;
                if (arrowCound >= arrowDuration)
                {
                    arrowCound = 0;
                    arrow_ammount = 0;
                    arrowing = false;
                    actioning = false;
                    ran = Random.Range(0, 100);
                }
            }

        }
        else if (currentAction == ACTION.SPEAR)//spear
        {
            if (spearing == false)
            {
                facePlayer();
                ranTime = spearDuration;
                spearing = true;                
                changeState(3);
                
                if (direction == 1)
                {
                    spear = spearI;
                   
                }
                else
                {
                    spear = spearII;
                    
                }
                Instantiate(spear, this.transform.position + new Vector3(0, spearAbove, 0), spear.transform.rotation);
            }
            else
            {
                
                    spearCound += Time.deltaTime;
                if (spearCound >= spearDuration)
                {
                    spearCound = 0;
                    spearing = false;
                    actioning = false;
                    ran = Random.Range(0, 100);
                }
            }
        }
        else if (currentAction == ACTION.SCIMITAR)
        {
            if (sciming == false)
            {
                facePlayer();
                sciming = true;
                changeState(4);
                if(direction == 1)
                {
                    scimitar.GetComponent<enemySpell>().direction = 1;
                    scimitar.GetComponent<enemySpell>().direction = -1000;
                    scimitar.GetComponent<SpriteRenderer>().flipX = false;
                } else
                {
                    scimitar.GetComponent<enemySpell>().direction = -1;
                    scimitar.GetComponent<enemySpell>().direction = 1000;
                    scimitar.GetComponent<SpriteRenderer>().flipX = true;
                }
                Instantiate(scimitar, this.transform.position, this.transform.rotation);
            }
            else
            {
                scimCound += Time.deltaTime;
                if (scimCound >= scimDuration)
                {                    
                    scimCound = 0;
                    sciming = false;
                    ran = Random.Range(0, 100);
                    actioning = false;
                }
            }
        }
        else if (currentAction == ACTION.BLADE)
        {
            if (blading == false)
            {
                facePlayer();              
                blading = true;
                changeState(5);
                Instantiate(blade, Ouros.transform.position, Ouros.transform.rotation);
            }
            else
            {
                bladeCound += Time.deltaTime;
                if (bladeCound >= bladeDuration)
                {
                    //meleeCollider.enabled = false;
                    bladeCound = 0;
                    blading = false;
                    ran = Random.Range(0, 100);
                    actioning = false;
                }
            }
        }
        

        if (currentAction != ACTION.IDLE)
        {
            idleCound = 0;
            idlinging = false;
        }
        if (currentAction != ACTION.RUN)
        {
            runCound = 0;
            running = false;
        }
        if (currentAction != ACTION.ARROW)
        {
            arrowCound = 0;
            arrowing = false;
        }
        if (currentAction != ACTION.SPEAR)
        {
            spearCound = 0;
            spearing = false;
        }
        if (currentAction != ACTION.BLADE)
        {
            bladeCound = 0;
            blading = false;
        }
        if (currentAction != ACTION.SCIMITAR)
        {
            scimCound = 0;
            sciming = false;
        }
    }




    // Use this for walkDurationialization
    void Start()
    {
        Ouros = FindObjectOfType<Ouros>();
        rid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ran = 0;
        timecount = 0;
        facePlayer();
        spearI.GetComponent<enemySpell>().spawner = floorlevel;
        spearII.GetComponent<enemySpell>().spawner = floorlevel;
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
        }else
        {

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            ran = Random.Range(2, 5);
        }
        if (other.gameObject.tag == "Wall")
        {
            facePlayer();
        }
    }
}
