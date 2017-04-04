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
    
    public bool unLimit;
    public int charge;

    public int Dmg;
    
    private Ouros Ouros;
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
        Ouros = FindObjectOfType<Ouros>();
    }

    void Update()
    {
        if (type == "projectile")
        {
            projectile();
        }

    }

    void projectile()
    {
        
        
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
}
    

