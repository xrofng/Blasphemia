using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public string magicName;
    public string description;
    public Sprite icon;
    public string type;
    public float speed;
    public Vector2 direction;
    public bool stand;
    public bool unLimit;
    public int charge;

    public int Dmg;
   

    // Update is called once per frame
    Magic(Vector2 d)
    {
        direction = d;
    }
    public void setDirection(Vector2 d)
    {
        direction = d;
    }
    public void setDamage(int d)
    {
        Dmg = d;
    }
    public void setSpeed(float s)
    {
        speed = s;
    }
    public void setSelfDesTime(float sdt)
    {
        GetComponent<selfDestruct>().time = sdt;
    }
    public void setType(string t)
    {
        type = t;
    }

    void Start()
    {

    }

 
    void Update()
    {
        if (type == "projectile")
        {
            projectile();
        } else if (type == "physic")
        {
            physic();
        }
        else if (type == "surf")
        {
            surf();
        }

    }
    void physic()
    {
        if (direction.x == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction.x == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
        
    }
    void projectile()
    {
        if (direction.y == 1)
        {
            GetComponent<SpriteRenderer>().flipY = false;
        }
        else if (direction.y == -1)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
        if (direction.x == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction.x == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        transform.Translate(direction.x* speed*Time.deltaTime, direction.y * speed * Time.deltaTime, 0);       

    }
    void surf()
    {
       
        Ouros Ouros = FindObjectOfType<Ouros>();
        Move movescr = FindObjectOfType<Move>();
        float ox = Ouros.GetComponent<Transform>().position.x;
        float tx = this.GetComponent<Transform>().position.x;
       
        if (stand == false)
        {
            if (ox > tx - 0.1f && ox < tx + 0.1f)
            {
                GetComponent<SpriteRenderer>().flipX = Ouros.GetComponent<SpriteRenderer>().flipX;
            }
            else if (ox < tx)
            {
                this.direction = new Vector2(-1, 0);
                GetComponent<SpriteRenderer>().flipX = true;
                transform.Translate(Vector3.right * speed * direction.x * Time.deltaTime);

            }
            else if (ox > tx)
            {
                this.direction = new Vector2(1, 0);
                GetComponent<SpriteRenderer>().flipX = false;
                transform.Translate(Vector3.right * speed * direction.x * Time.deltaTime);

            }
        }
        

        if (stand == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, movescr.jumpSpeed*70/100));
            }
             
            transform.Translate(Vector3.right * speed * direction.x * Time.deltaTime);
            Ouros.GetComponent<Transform>().Translate(Vector3.right * speed * direction.x * Time.deltaTime);
            if (Ouros.GetComponent<SpriteRenderer>().flipX ==true)
            {
                this.direction = new Vector2(-1, 0);
                GetComponent<SpriteRenderer>().flipX = true;             

            }
            else if (Ouros.GetComponent<SpriteRenderer>().flipX == false)
            {
                this.direction = new Vector2(1, 0);
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        


    }
    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<selfDestruct>().destroyNow();
            FindObjectOfType<Ouros>().recieveDamage(Dmg);
            
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {       
        if (other.gameObject.tag == "Player" && this.type == "surf")
        {
            stand = true;
            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                this.direction = new Vector2(-1, 0);
                
            } else
            {
                this.direction = new Vector2(1, 0);
            }
            Move movescr = FindObjectOfType<Move>();
            movescr.adInput = false;

        }
    }
    void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player" && this.type == "surf")
        {
            stand = false;
            this.direction = new Vector2(0, 0);
            Move movescr = FindObjectOfType<Move>();
            movescr.adInput = true;
        }
    }

    void OnBecameInvisible()
    {
        if (this.type == "surf")
        {
            stand = false;
            this.direction = new Vector2(0, 0);
            Move movescr = FindObjectOfType<Move>();
            movescr.adInput = true;
        }
    }

}
    

