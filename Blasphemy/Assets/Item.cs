using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string itemName;
    public int price;
    public string description;
    public Sprite icon;
    public bool isConsumable;
    public bool isUsable;
    public int recoveryPoint;
    public int amount;
    public bool key;

    public Magic magicGet;

    private Ouros Ouros;
    private D d;

    Item(string n, int p, string d, Sprite i, bool c, bool u,int a)
    {
        itemName = n;
        price = p;
        description =d;
        icon = i;
        isConsumable = c;
        isUsable = u;
        amount = a;
    }

    // Usbool isUsable;e this for initialization
    void Start () {
        Ouros = FindObjectOfType<Ouros>();
        d = FindObjectOfType<D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void skillGet()
    {

        for (int i=1;i< Ouros.magicList.Length; i++)
        {
            if (Ouros.magicList[i] == null)
            {
                Ouros.magicList[i] = magicGet;
                Ouros.maxMagi += 1;
                return;
            }                
        }            
        
    }

    void itemGet()
    {
        d.Opic = this.icon;
        if (this.isConsumable == true)
        {
            d.Otex = this.itemName + " x " + this.amount;
        }
        else
        {
            d.Otex = this.itemName;
        }

        d.Obtain();
        skillGet();
        Item hand = Ouros.item;
        if (this.key == false)
        {
            for (int i = 1; i < Ouros.itemList.Length; i++)
            {

                if (Ouros.itemList[i].itemName == this.itemName)
                {
                    Ouros.itemList[i].amount += this.amount;
                }
                Destroy(gameObject);
                if (Ouros.GetComponent<Move>().isOnGround == true)
                {
                    Ouros.GetComponent<Move>().isOnGround = true;
                }
            }
        }else
        {
            keyItem();
        }
       
    }

    void keyItem()
    {
        if (this.itemName == "Spear")
        {
            Variable v = FindObjectOfType<Variable>();
            v.var_spear += 1;
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            itemGet();
        }

    }

    public void useItem()
    {           
        if (this.itemName == "Potion")
        {
            Ouros = FindObjectOfType<Ouros>();
            if (Ouros.HP != Ouros.maxHP)
            {
                if  (Ouros.HP + 100 > Ouros.maxHP)
                {
                    Ouros.HP  = Ouros.maxHP;
                }
                else
                {
                    Ouros.HP += 100;
                }
              
                Ouros.item.amount -= 1;
            }
        }
        if  (this.itemName == "Ether")
        {
            Ouros = FindObjectOfType<Ouros>();
            if (Ouros.magic.unLimit == false)
            {
                Ouros.GetComponent<Ouros>().magic.charge += 5;
                Ouros.item.amount -= 1;
            }
        }
        
    }

}
