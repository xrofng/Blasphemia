using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckAI : MonoBehaviour
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
    public enum ACTION { FLOAT, OUT, SWOOP, IN, FIRE };
    public ACTION currentAction;
    public float floatDuration;
    private bool floating;
    private float floatCound;

    public float outDuration;
    private bool outing;
    private float outCound;

    public float swoopDuration;
    private bool swooping;
    private float swoopCound;

    public float inDuration;
    private bool ining;
    private float inCound;

    public float fireDuration;
    private bool firing;
    private float fireCound;

    public float swoopSpd;
    public enemySpell sweep;
    public Vector3 outPo;
    public Vector3 inPo;
    public Vector3 sidePo1;
    public Vector3 sidePo2;
    public Vector3 comebackPo;
    public GameObject flame;
    public GameObject drpflame;
    public Transform eyeL;
    public Transform eyeR;
    public Transform mouthL;
    public Transform mouthR;
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



    void EnemyAction()
    {
    
        checkMelee();
        checkMeleeII();
        //if (actioning == false)
        //{
        //    actioning = true;
        //    if (melee == false && meleeII == false)
        //    {
        //        if (ran >= 0 && ran < 20)//walk
        //        {
        //            currentAction = ACTION.RUN;
        //        }
        //        if (ran >= 20 && ran < 30)//idle
        //        {
        //            currentAction = ACTION.IDLE;
        //        }
        //        if (ran >= 30 && ran < 70) // blade
        //        {
        //            currentAction = ACTION.BLADE;
        //        }
        //        else if (ran >= 70 && ran < 100)// arrow
        //        {
        //            currentAction = ACTION.ARROW;
        //        }
        //    }
        //    if (melee == false && meleeII == true)
        //    {
        //        if (ran >= 0 && ran < 20)//walk
        //        {
        //            currentAction = ACTION.RUN;
        //        }
        //        if (ran >= 20 && ran < 30)//idle
        //        {
        //            currentAction = ACTION.IDLE;
        //        }
        //        if (ran >= 30 && ran < 50) // blade
        //        {
        //            currentAction = ACTION.BLADE;
        //        }
        //        if (ran >= 50 && ran < 80) // scimitar
        //        {
        //            currentAction = ACTION.SCIMITAR;
        //        }
        //        else if (ran >= 80 && ran < 100)// arrow
        //        {
        //            currentAction = ACTION.ARROW;
        //        }
        //    }
        //    if (melee == true && meleeII == true)
        //    {
        //        if (ran >= 0 && ran < 10)//walk
        //        {
        //            currentAction = ACTION.RUN;
        //        }
        //        if (ran >= 10 && ran < 40)//spear
        //        {
        //            currentAction = ACTION.SPEAR;
        //        }
        //        if (ran >= 40 && ran < 60) // blade
        //        {
        //            currentAction = ACTION.BLADE;
        //        }
        //        if (ran >= 60 && ran < 80) // scimitar
        //        {
        //            currentAction = ACTION.SCIMITAR;
        //        }
        //        else if (ran >= 80 && ran < 100)// arrow
        //        {
        //            currentAction = ACTION.ARROW;
        //        }
        //    }
        //}


        if (currentAction == ACTION.FLOAT)
        {
            if (floating == false)
            {
                floating = true;
                changeState(0);
            }
            else
            {
                if (this.GetComponent<SpriteRenderer>().isVisible == false)
                {
                    facePlayer();
                }
                floatCound += Time.deltaTime;
                transform.Translate(Vector3.right * direction * mySpeed * Time.deltaTime);
                if (floatCound >= floatDuration)
                {
                    floatCound = 0;
                    floating = false;
                    actioning = false;
                    ran = Random.Range(0, 100);

                }
            }
        }
        else if (currentAction == ACTION.OUT)//walk
        {
            if (outing == false)
            {
                outing = true;
                changeState(1);
            }
            else
            {
                if (this.transform.position.y < outPo.y)
                {
                    transform.Translate(Vector3.up * mySpeed * Time.deltaTime);

                    this.GetComponent<CapsuleCollider2D>().enabled = false;
                }
                else
                {
                    int inpoo = Random.Range(0, 2);
                    this.GetComponent<CapsuleCollider2D>().enabled = false;
                    if (inpoo == 0)
                    {
                        Vector3 tra = sidePo1 - this.GetComponent<Transform>().position;
                        transform.Translate(tra);
                    }
                    if (inpoo == 1)
                    {
                        Vector3 tra = sidePo2 - this.GetComponent<Transform>().position;
                        transform.Translate(tra);
                    }
                    outCound = 0;
                    outing = false;
                    actioning = false;
                    facePlayer();
                    
                    currentAction = ACTION.SWOOP;
                }

                outCound += Time.deltaTime;
                if (outCound >= outDuration)
                {

                }
            }


        }
        else if (currentAction == ACTION.SWOOP)//arrow
        {
            if (swooping == false)
            {
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                swooping = true;
                changeState(2);
                sweep.enabled = true;
                sweep.GetComponent<CircleCollider2D>().enabled = true;
            }
            else
            {
                transform.Translate(Vector3.right * direction * swoopSpd * Time.deltaTime);
                swoopCound += Time.deltaTime;
                if (swoopCound >= swoopDuration)
                {
                    //Vector3 tra = comebackPo - this.GetComponent<Transform>().position;
                    //transform.Translate(tra);
                    swoopCound = 0;
                    swooping = false;
                    sweep.enabled = false;
                    sweep.GetComponent<CircleCollider2D>().enabled = false;
                    actioning = false;
                    currentAction = ACTION.IN;
                    this.GetComponent<CapsuleCollider2D>().enabled = true;
                }
            }
        }
        else if (currentAction == ACTION.IN)//arrow
        {
            if (ining == false)
            {
                ining = true;
                changeState(0);
            }
            else
            {
                
                if (this.transform.position.y < inPo.y)
                {
                    transform.Translate(Vector3.up * mySpeed * Time.deltaTime);

                   
                }
                else
                {
                    
                    inCound = 0;
                    ining = false;
                    actioning = false;
                    facePlayer();
                    this.GetComponent<CapsuleCollider2D>().enabled = true;
                    currentAction = ACTION.FLOAT;
                }

                if (inCound >= inDuration)
                {
                    
                }
            }
        }    
        else if (currentAction == ACTION.FIRE)//spear
        {
            if (firing == false)
            {
                facePlayer();
                firing = true;
                changeState(3);
                if(direction == 1)
                {
                    eyeR.GetComponent<SpriteRenderer>().enabled = true;
                    mouthR.GetComponent<SpriteRenderer>().enabled = true;
                }else
                {
                    eyeL.GetComponent<SpriteRenderer>().enabled = true;
                    mouthL.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                fireCound += Time.deltaTime;
                if (fireCound >= fireDuration)
                {
                    eyeR.GetComponent<SpriteRenderer>().enabled = false;
                    mouthR.GetComponent<SpriteRenderer>().enabled = false;
                    eyeL.GetComponent<SpriteRenderer>().enabled = false;
                    mouthL.GetComponent<SpriteRenderer>().enabled = false;
                    facePlayer();
                    fireCound = 0;
                    firing = false;
                    actioning = false;
                    if(direction == 1)
                    {
                        Instantiate(flame, mouthR.position, mouthR.rotation);
                    }else if (direction == -1)
                    {
                        Instantiate(flame, mouthL.position, mouthL.rotation);
                    }                        
                    ran = Random.Range(0, 100);
                }
            }
        }



        if (currentAction != ACTION.FLOAT)
        {
            floatCound = 0;
            floating = false;
        }
        if (currentAction != ACTION.OUT)
        {
            outCound = 0;
            outing = false;
        }
        if (currentAction != ACTION.SWOOP)
        {
            swoopCound = 0;
            swooping = false;
        }
        if (currentAction != ACTION.FIRE)
        {
            fireCound = 0;
            firing = false;
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
        if (other.gameObject.tag == "Wall")
        {
            facePlayer();
        }
    }
}
