using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayHUD : MonoBehaviour {

    public string type;
    public Ouros Ouros;
    // Use this for initialization
    void Start () {
        Ouros = FindObjectOfType<Ouros>();
    }
	
	// Update is called once per frame
	void Update () {
        if (type == "itemImage")
        {
            gameObject.GetComponent<Image>().sprite = Ouros.item.icon;
        } else if (type == "itemAmount")
        {
            gameObject.GetComponent<Text>().text = "x "+ Ouros.item.amount.ToString();
        }
        else
        if (type == "magicImage")
        {
            gameObject.GetComponent<Image>().sprite = Ouros.magic.icon;
        }
        else if (type == "magicCharge")
        {
            if (Ouros.magic.unLimit == true)
            {
                gameObject.GetComponent<Text>().fontSize = 27;
                gameObject.GetComponent<Text>().text = "∞";
            } else
            {
                gameObject.GetComponent<Text>().fontSize = 16;
                gameObject.GetComponent<Text>().text = "x " + Ouros.magic.charge.ToString();
            }
            
        }
    }
}
