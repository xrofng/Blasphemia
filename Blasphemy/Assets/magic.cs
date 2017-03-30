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
    public int direction;
    public bool unLimit;
    public int charge;

    public int Dmg;
    
    private Ouros Ouros;
    // Update is called once per frame
    Magic(int d)
    {
        direction = d;
    }
    public void setDirection(int d)
    {
        direction = d;
    }
    public void setDamage(int d)
    {
        Dmg = d;
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
        if (direction == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        transform.Translate(speed * Time.deltaTime * direction, 0, 0);
    }
}
    

