using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savePoint : MonoBehaviour {

    private bool save;
    private Ouros Ouros;
    public Canvas canvas;
    public GameObject saveText;
    public GameObject standPar;
    public GameObject pressPar;


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
                GameObject pressParc = Instantiate(pressPar) as GameObject;
                pressParc.transform.SetParent(Ouros.transform, false);
                GameObject Dlog = Instantiate(saveText) as GameObject;
                Dlog.transform.SetParent(canvas.transform, false);
                Ouros.Save();
            }
           
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            save = true;
            //GameObject standParc = Instantiate(standPar) as GameObject;
            //standParc.transform.SetParent(this.transform, false);
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
            //standPar.gameObject.GetComponent<ParticleSystem>().Stop();
            save = false;
        }
        else
        {

        }
    }


}
