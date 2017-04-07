using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string NAME;
    public string type;
    public int ATK;
    public int HP;
    public int DEF;
    public float mySpeed;
    
    //private Animator anime;
    [SerializeField]
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


    public Transform spawnPoint;
    private int magicDel = 0;
    public float jumpspeed;
    public float sightX;
    public float sightY;
    public bool inSight;

    private Ouros Ouros;
    private Rigidbody2D rid2d;
    private Animator animator;


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
    // Use this for walkDurationialization
    void Start()
    {
        Ouros = FindObjectOfType<Ouros>();
        rid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timecount = 0;
    }

    void checkDie()
    {
        if (this.HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void EnemyAction()
    {
        //if (type == "walkonly")
        //{
        //    walk();
        //}
        //if (type == "shootCrap")
        //{
        //    shootCrap();
        //}
        //if (type == "walkThenShoot")
        //{
        //    walkAndShoot();
        //}
        //if (type == "Jumper")
        //{

        //}
        //if (type == "Dasher")
        //{

        //}
        //if (type == "")
        //{

        //}/
        facePlayer();
        if (NAME == "Marlanx")
        {
            transform.Translate(Vector3.right * direction *  mySpeed *Time.deltaTime);
            timecount += Time.deltaTime;
            if (timecount > delay)
            {
                rid2d.AddForce(new Vector2(0, jumpspeed));
                changeState(1);
                GetComponentInChildren<BoxCollider2D>().enabled = true;
                timecount = -1.8f;
            } else if (timecount >= 0)
            {
                GetComponentInChildren<BoxCollider2D>().enabled = false;
            }
        }
        if (NAME == "Duiston")
        {
            //Melee-just play animation
            //Range-create Magic class with isTrigger 2dcollider rigidbody2d large snowball, hit do damage and destroy snowball
        }
        if (NAME == "Tekar")
        {
            //Melee-create Magic class with isTrigger 2dcollider lava pool, if player step on it do damage overtime (create prefab of lava pool)
            //Range-Target player Instantiate lava ball that float to player, then create 2 more that go furthur for fix distance
        }
        if (NAME == "Scynla")
        {
            //Target player Instantiate poison obj that float to player 3 time with delay time to time
            //Find player position, instantiate poison rain above player, slowly move down to floor do aoe damage.
            //create poison mist that has long length as fighting area. do damage over time.
        }
        if (NAME == "Warrir")
        {
            //create Magic class with isTrigger 2dcollider if IceShard in front of it
            //
        }
        if(NAME == "Finik")
        {

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

    void walk()
    {
        if (direction==-1)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
            walkDuration -= Time.deltaTime;

            if(walkDuration<=0.0f)
            {
                
                magicDel = -1;
                
                walkDuration = 3.0f;
                direction = 1;
                GetComponent<SpriteRenderer>().flipX = false;
                //gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
        else if(direction==1)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
            walkDuration -= Time.deltaTime;

            if (walkDuration <= 0.0f)
            {
                
                magicDel = 1;
                
                walkDuration = 3.0f;
                direction = -1;
                GetComponent<SpriteRenderer>().flipX = true;
                //gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
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
                //gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
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
        if (magicDel==1)
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

   
    void Update()
    {
        checkInsight();
        checkDie();
        if (inSight == true)
        {
            EnemyAction();
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
}
