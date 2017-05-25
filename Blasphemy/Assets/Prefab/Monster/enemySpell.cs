using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpell : MonoBehaviour
{
    public string magicName;
    public string type;
    public float speed;
    public float Fspeed;
    public float Lspeed;
    public Vector2 travelDir;
    public int Dmg;
    public GameObject spawner;
    private Ouros Ouros;
    public float timer = 0.0f;
    [SerializeField]
    private float rotSpeed;
    public int direction;
    private bool rottated;
    private float timCout;
    // Use this for initialization
    void Start () {
        Ouros = FindObjectOfType<Ouros>();
        if(magicName=="Poison" || magicName=="Snowball" || magicName == "Flame")
        {
            travelDir = new Vector2(Ouros.transform.position.x - gameObject.transform.position.x, Ouros.transform.position.y - gameObject.transform.position.y);
        }
        if (magicName == "Arrow")
        {
            int start = Random.Range(-90, 90);
            print(start);
            this.GetComponent<Transform>().Rotate(new Vector3(0, 0, start));
        }
    }
	public void setDirection(int d)
    {
        direction = d;
    }
    public void setTravelDir(Vector2 td)
    {
        travelDir = td;
    }
	// Update is called once per frame
	void Update ()
    {
		if(magicName=="Poison")
        {
            transform.Translate(travelDir.x * speed * Time.deltaTime, travelDir.y * speed * Time.deltaTime, 0);
        }
        else if(magicName=="DotPoison")
        {
            timer += Time.deltaTime;
        }
        else if(magicName=="Snowball")
        {
            gameObject.transform.Rotate(0, 0, Time.deltaTime * rotSpeed, Space.Self);
            //transform.Translate(, 0,Space.World);            
            transform.Translate(Vector3.right * direction *speed* Time.deltaTime,Space.World);
        }
        else if (magicName == "Spear")
        {
            transform.Translate(travelDir * speed * Time.deltaTime, Space.World);
            if(transform.position.y < spawner.transform.position.y+0.8f)
            {
                travelDir = new Vector2(0,0);
                GetComponent<Interact>().enabled = true;
                GetComponent<Interact>().feedback.GetComponent<SpriteRenderer>().enabled = true;
                this.gameObject.tag = "Switch";
                this.enabled = false;
                
            }
        }
        else if (magicName == "Scimitar")
        {
            timCout += Time.deltaTime;
            if( timCout > 3)
            {
                gameObject.transform.Rotate(0, 0, -Time.deltaTime * rotSpeed, Space.Self);
                //transform.Translate(, 0,Space.World);            
                transform.Translate(Vector3.right * -direction * speed * Time.deltaTime, Space.World);
            }
            else
            {
                gameObject.transform.Rotate(0, 0, Time.deltaTime * rotSpeed, Space.Self);
                //transform.Translate(, 0,Space.World);            
                transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);
            }
            
        }else if (magicName == "Arrow")
        {
            timCout += Time.deltaTime;
            if (timCout > 3)
            {
                if (rottated == false)
                {
                    Vector3 v1 = this.travelDir;
                    Vector3 v2 = FindObjectOfType<Ouros>().transform.position-this.transform.position;
                    float dotPow2 = ((v1.x * v2.x) + (v1.y * v2.y)) * ((v1.x * v2.x) + (v1.y * v2.y));
                    float v1sizePow2 = (v1.x * v1.x) + (v1.y * v1.y);
                    float v2sizePow2 = (v2.x * v2.x) + (v2.y * v2.y);
                    float crossZ = (v1.x * v2.y) - (v1.y * v2.x);
                    float rotateAngle = Mathf.Acos(Mathf.Sqrt(dotPow2 / (v1sizePow2 * v2sizePow2)));
                    rotateAngle = rotateAngle * (90 / Mathf.PI) + 90;
                    if (crossZ >= 0)
                    {
                        this.GetComponent<Transform>().Rotate(new Vector3(0, 0, rotateAngle));
                    }
                    else
                    {
                        this.GetComponent<Transform>().Rotate(new Vector3(0, 0, -rotateAngle));
                    }
                    rottated = true;
                }


                speed += Time.deltaTime * 60;
                
                if (speed >= Lspeed)
                {
                    speed = Lspeed;
                }
                transform.Translate(travelDir * speed * Time.deltaTime);
            }
            else
            {
                speed = Fspeed;
                transform.Translate(travelDir * speed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //when got hit by enemy attack // des it's magic
        {
            
            if (Ouros.isInvisible == false)
            {
                Ouros.HP -= this.Dmg - Ouros.DEF / this.Dmg;
                
            }
        }
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "Player") //when got hit by enemy attack // des it's magic
        {
            if (magicName == "SnowStorm")
            {
                Ouros.transform.Translate(Vector3.right * direction*speed * 4 * Time.deltaTime);
                if (Time.frameCount% 30 ==0) {
                    Ouros.recieveDamage(this.Dmg);
                }
            }
            if (magicName == "MagicHour")
            {               
                if (Time.frameCount % 20 == 0)
                {
                    Ouros.recieveDamage(this.Dmg);
                    Ouros.knockBack(0.0f);
                }
            }
            if (magicName == "Sweep")
            {
                
                Ouros.recieveDamage(this.Dmg);
                Ouros.knockBack(1.2f);
                this.enabled = false;
                this.GetComponent<CircleCollider2D>().enabled = false;

            }
        }
       
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") //when got hit by enemy attack // des it's magic
        {
            if (Ouros.isInvisible == false)
            {
                
                Ouros.HP -= this.Dmg - Ouros.DEF / this.Dmg;
                if (magicName == "Snowball")
                {
                    this.GetComponent<selfDestruct>().destroyNow();
                    Ouros.knockBack(1.2f);
                }
            }else
            {
                this.GetComponent<selfDestruct>().destroyNow();
            }

        }
        
    }
}
