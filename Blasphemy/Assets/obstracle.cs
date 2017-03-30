using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstracle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Magic")
        {
         
            if (other.gameObject.GetComponent<Magic>().magicName == "Dark Ignito")
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

        }
        else
        {

        }

    }
}
