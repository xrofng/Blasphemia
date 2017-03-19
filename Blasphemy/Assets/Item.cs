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
    private Ouros Ouros;

    public Magic magicGet;
    public GameObject popup;
    public string pickUpMessage;

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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void skillGet()
    {
        if (this.itemName == "DarkFiraga")
        {
            for (int i=1;i< Ouros.magicList.Length; i++)
            {
                if (Ouros.magicList[i] == null)
                {
                    Ouros.magicList[i] = magicGet;
                   
                    return;
                }                
            }            
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            skillGet();
            Item hand = Ouros.item;
            Debug.Log(hand.amount);
            Debug.Log("get item");
            for (int i=1; i< Ouros.itemList.Length; i++)
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
                Debug.Log(hand.amount);
            }
           
        }

    }

    public void useItem()
    {           
        Debug.Log("Item used");
        Debug.Log(this.name);
        if (this.itemName == "Potion")
        {
            Ouros = FindObjectOfType<Ouros>();
            Ouros.GetComponent<Ouros>().HP = Ouros.GetComponent<Ouros>().HP + 100;
        }
        if  (this.itemName == "Ether")
        {
            Ouros = FindObjectOfType<Ouros>();
            Ouros.GetComponent<Ouros>().magic.charge += 5;
        }
    }
}
