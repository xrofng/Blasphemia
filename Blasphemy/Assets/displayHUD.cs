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
        if (type == "image")
        {
            gameObject.GetComponent<Image>().sprite = Ouros.item.icon;
        } else if (type == "amount")
        {
            gameObject.GetComponent<Text>().text = Ouros.item.amount.ToString();
        }
    }
}
