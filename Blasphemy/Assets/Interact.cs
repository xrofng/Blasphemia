using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
    public string require;
    public bool isQuest;
    public GameObject success;
    public GameObject fail;
    public Door door;
    public int type; // door0 trigger1
    public Trigger trigger;
    private bool pass;
    public Canvas canvas;
    public SpriteRenderer feedback;
    // Use this for initialization
    void Start () {
		
	}
	bool condition()
    {
        if (require == "Spear" && FindObjectOfType<Variable>().var_spear >= 3)
        {
            return true;
        }
        return false;
    }
	// Update is called once per frame
	void Update () {
        if (isQuest == true)
        {
            pass = condition();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            if (type ==0)
            {
                if (Input.GetButton("Interact"))
                {
                    
                    if (pass == true) {
                        GameObject Dlog = Instantiate(success) as GameObject;
                        Dlog.transform.SetParent(canvas.transform, false);
                        door.open();
                        
                    } else
                    {
                        GameObject Dlog = Instantiate(fail) as GameObject;
                        Dlog.transform.SetParent(canvas.transform, false);
                    }
                }
            }
            if (type == 1)
            {
                if (Input.GetButton("Interact"))
                {
                    trigger.checkType();                                      
                }
            }
            if (type == 2)
            {
                if (Input.GetButton("Interact"))
                {
                    door.open();
                }
            }

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            feedback.enabled = true;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            feedback.enabled = false;
        }
       
    }
}
