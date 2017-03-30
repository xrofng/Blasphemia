using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public Door door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
            {
                door.open();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }
}
