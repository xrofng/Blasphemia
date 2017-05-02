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

    void facePlayer()
    {
        faceCount += Time.deltaTime;
        if (faceCount > faceDelay)
        {

            if (Ouros.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x)
            {
                direction = 1;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Ouros.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x)
            {
                direction = -1;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            faceCount = 0;
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
                        range = false;
                        return;
                    }
                }
            }
        }
        melee = false;
    }

    void EnemyAction()
    {
        facePlayer();

        if (range == false && melee == true)
        {
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
        else if(range == true && melee == false) 
        {
            timecount += Time.deltaTime;
            if (timecount > delayRange)
            {
                if(summoned==false)
                {
                    //changeState(2);
                    if (direction == -1)
                    {
                        Instantiate(snowball, spawnPointR.transform.position, spawnPointR.transform.rotation);
                    }
                    if (direction == 1)
                    {
                        Instantiate(snowball, spawnPointL.transform.position, spawnPointL.transform.rotation);
                    }
                    snowball.setDirection(-direction);
                    timecount = 0.0f;
                }
            }
        }
    }

    void checkDie()
    {
        if (this.HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            HP -= Ouros.ATK - this.DEF / Ouros.ATK;


        }
        else
        {

        }
    }
    // Use this for initialization
    void Start ()
    {
        Ouros = FindObjectOfType<Ouros>();
        rid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        meleeCollider.GetComponent<enemyMelee>().ATK = this.ATK;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        checkDie();
        checkMelee();
        checkRange();
        EnemyAction();
        if(summoned==true)
        {
            summoned = false;
        }
    }
}
