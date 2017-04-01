using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string type;
    public int ATK;
    public int HP;
    public int DEF;
    public float mySpeed = 2.55f;
    //private Animator anime;
    [SerializeField]
    //private bool inSight = false;
    //private Collider2D copied;
    private float walkDuration = 3.0f;
    public GameObject magic;
    public int direction = 1;
    private float delay = 3.0f;
    public Transform spawnPoint;
    private int magicDel = 0;

    public float sight;
    public float sightY;
    public bool inSight;

    private Ouros Ouros;
    // Use this for walkDurationialization
    void Start()
    {
        Ouros = FindObjectOfType<Ouros>();
        
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
        if (type == "walkonly")
        {
            walk();
        }
        if (type == "shootCrap")
        {
            shootCrap();
        }
        if (type == "walkThenShoot")
        {
            walkAndShoot();
        }
        if (type == "Jumper")
        {

        }
        if (type == "Dasher")
        {

        }
        if (type == "")
        {

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
            magic.GetComponent<Magic>().setDirection(1);
            Instantiate(magic, spawnPoint.transform.position, spawnPoint.transform.rotation);
            
        }
        if (magicDel == -1)
        {
            magic.GetComponent<Magic>().setDirection(-1);
            Instantiate(magic, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    void checkInsight()
    {
        if (Ouros.GetComponent<Transform>().position.y < this.GetComponent<Transform>().position.y + sightY)
        {
            if (Ouros.GetComponent<Transform>().position.y > this.GetComponent<Transform>().position.y - sightY)
            {
                if (Ouros.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x - sight)
                {
                    if (Ouros.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x + sight)
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
            
            Debug.Log(this.HP);
        }
        else
        {

        }

    }
}
