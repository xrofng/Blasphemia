using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderZone : MonoBehaviour {
    public Move thePlayer;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<Move>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            thePlayer.isOnLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            thePlayer.isOnLadder = false;
        }
    }
}
