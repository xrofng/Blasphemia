using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public string type;
    public int damage;
    public float delayTime;
    private float timecount;
    public float speed;
    public float destructTime;
    public Transform spawnPoint;
    public int direction;
    private bool doDMG;
    public GameObject obj;

    public bool inSight;
    public float sightX;
    public float sightY;
    private Ouros Ouros;

    // Use this for initialization
    void Start()
    {
        Ouros = FindObjectOfType<Ouros>();
        timecount = 5000f;
    }

    // Update is called once per frame
    void Update()
    {

        checkInsight();
        if (inSight == true)
        {
            timecount += Time.deltaTime;
            if (timecount > delayTime)
            {
                TrapType();
            }
        }

    }

    void TrapType()
    {
        if (type == "shoot")//speed damage direction spawnPoint destructTime
        {
            shootTrap();
        }
        if (type == "floor")
        {
            floor();
        }
        timecount = 0;
    }

    void shootTrap()
    {
        
        obj.GetComponent<Magic>().setType("projectile");
        obj.GetComponent<Magic>().setDamage(this.damage);
        obj.GetComponent<Magic>().setSpeed(this.speed);
        obj.GetComponent<Magic>().setSelfDesTime(this.destructTime);
        if (direction == 0)
        {
            obj.GetComponent<Magic>().setDirection(2);
        }
        if (direction == 1)
        {
            obj.GetComponent<Magic>().setDirection(1);
        }
        if (direction == -1)
        {
            obj.GetComponent<Magic>().setDirection(-1);
        }
        Instantiate(obj, spawnPoint.transform.position, spawnPoint.transform.rotation);
        
    }
    void floor()
    {
        doDMG = true;
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

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            if (doDMG == true)
            {
                Ouros.HP -= damage;
                Ouros.knockBack(1);
                doDMG = false;
            }
            
        }
    }

}