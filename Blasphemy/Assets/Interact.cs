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
    // Use this for initialization
    void Start () {
		
	}
	bool condition()
    {
        if (require == "Spear" && FindObjectOfType<Variable>().var_spear == 3)
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
                if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
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
                if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
                {
                    trigger.checkType();                                      
                }
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }
}
