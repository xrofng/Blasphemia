using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marlanxAI : Enemy
{
    public string NAME;
    public string type;
    public int ATK;
    public int HP;
    public int DEF;
    public float mySpeed;

    //private Animator anime;

    //private bool inSight = false;
    //private Collider2D copied;
    private float walkDuration = 3.0f;
    public GameObject magic;
    public int direction = 1;
    private float timecount;
    public float delay = 3.0f;
    //ai Yo for create more AI
    public float attackRange; //if out of this range mon will do range attack
    public float faceDelay;
    private float faceCount;


    public Light ligttl;
    public Light ligttr;

    public Transform spawnPoint;
    private int magicDel = 0;
    public float jumpspeed;

    public float sightX;
    public float sightY;
    public bool inSight;

    private Ouros Ouros;
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


    void EnemyAction()
    {
        facePlayer();
        if (NAME == "Marlanx")
        {
            transform.Translate(Vector3.right * direction * mySpeed * Time.deltaTime);
            timecount += Time.deltaTime;
            if (timecount > delay)
            {
                rid2d.AddForce(new Vector2(0, jumpspeed));
                meleeCollider.enabled = true;
                timecount = -1.5f;
            }
            else if (timecount > delay - 0.3f)
            {

                changeState(1);

            }
            else if (timecount >= 0)
            {
                changeState(0);
                meleeCollider.enabled = false;
            }
        }

    }

    void walk()
    {
        if (direction == -1)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
            walkDuration -= Time.deltaTime;

            if (walkDuration <= 0.0f)
            {

                magicDel = -1;

                walkDuration = 3.0f;
                direction = 1;
                GetComponent<SpriteRenderer>().flipX = false;

            }
        }
        else if (direction == 1)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
            walkDuration -= Time.deltaTime;

            if (walkDuration <= 0.0f)
            {
                magicDel = 1;
                walkDuration = 3.0f;
                direction = -1;
                GetComponent<SpriteRenderer>().flipX = true;

            }
        }
    }

    void walkAndShoot()
    {
        if (direction == -1)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
            walkDuration -= Time.deltaTime;

            if (walkDuration <= 0.0f)
            {
                magicDel = -1;
                shootCrap();
                walkDuration = 3.0f;
                direction = 1;
                GetComponent<SpriteRenderer>().flipX = false;

            }
        }
        else if (direction == 1)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
            walkDuration -= Time.deltaTime;

            if (walkDuration <= 0.0f)
            {

                magicDel = 1;
                shootCrap();
                walkDuration = 3.0f;
                direction = -1;
                GetComponent<SpriteRenderer>().flipX = true;
                //gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
    }

    void shootCrap()
    {
        magic.GetComponent<Magic>().setDamage(this.ATK);
        if (magicDel == 1)
        {
            magic.GetComponent<Magic>().setDirection(Vector2.right);
            Instantiate(magic, spawnPoint.transform.position, spawnPoint.transform.rotation);

        }
        if (magicDel == -1)
        {
            magic.GetComponent<Magic>().setDirection(Vector2.left);
            Instantiate(magic, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

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
    // Use this for walkDurationialization
    void Start()
    {

        Ouros = FindObjectOfType<Ouros>();
        rid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        meleeCollider.GetComponent<enemyMelee>().ATK = this.ATK;
        timecount = 0;
    }

    void checkDie()
    {
        if (this.HP <= 0)
        {
            isDead = true;
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
                ligttl.enabled = true;
                ligttr.enabled = false;
            }
            else if (Ouros.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x)
            {
                direction = 1;
                GetComponent<SpriteRenderer>().flipX = false;
                ligttr.enabled = true;
                ligttl.enabled = false;
            }
            faceCount = 0;
        }



    }

    void checkInsight()
    {
        if (Ouros.GetComponent<Transform>().position.y < this.GetComponent<Transform>().position.y + sightY)
        {
            if (Ouros.GetComponent<Transform>().position.y > this.GetComponent<Transform>().position.y - sightY)
            {
                if (Ouros.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x - sightX)
                {
                    if (Ouros.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x + sightX)
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
        else
        {

        }
    }
}
