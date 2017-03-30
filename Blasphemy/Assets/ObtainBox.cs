using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtainBox : MonoBehaviour {

    public Image lootImage;
    public Text lootName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setImage(Sprite s)
    {
        lootImage.sprite = s;
    }
    public void setName(string s)
    {
        lootName.text = s;
    }
}
