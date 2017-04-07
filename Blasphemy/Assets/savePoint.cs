using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savePoint : MonoBehaviour {

    private bool save;
    private Ouros Ouros;
    public Canvas canvas;
    public GameObject saveText;

    // Use this for initialization
    void Start () {
        Ouros = FindObjectOfType<Ouros>();
    }
	
	// Update is called once per frame
	void Update () {
        if (save == true)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Ouros.Save();
            }
           
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            save = true;
            //GameObject Dlog = Instantiate(saveText) as GameObject;
            //Dlog.transform.SetParent(canvas.transform, false);
        }
        else
        {

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy(saveText.gameObject);
            save = false;
        }
        else
        {

        }
    }


}
