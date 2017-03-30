using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class D : MonoBehaviour {

    static public int sequence;
    public Transform canvas;
    public GameObject[] DialogueList;
    
    private float timeCount = 0f;
    private float delayTime = 0.5f;
    private bool count;
    private bool start;

    public GameObject ObtainBox;
    public Sprite Opic;
    public string Otex;

    // Use this for initialization
    void Start () {
        sequence = 0;
        timeCount = 0f;
        delayTime = 0.5f;
        count = false;
        start = false;
    }
	
	// Update is called once per frame
	void Update () {
		

	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "DialogueEvent")
        {
            count = true;
            if (start == true)
            {
                playDialogue();
                start = false;
                Destroy(other.gameObject);
                timeCount = 0f;
                delayTime = 0.5f;
                count = false;
                start = false;
            } else
            {
                if (count == true)
                {
                    timeCount += Time.deltaTime;
                }
                if (timeCount > delayTime)
                {
                    start = true;
                    count = false;
                }
            }
            
        }
        else if (other.gameObject.tag == "Loot")
        {
            Opic = other.GetComponent<Item>().icon;
            Otex = other.GetComponent<Item>().itemName;
            Obtain();
        }
    }
    public void playDialogue()
    {
        GameObject Dlog = Instantiate(DialogueList[sequence]) as GameObject;
        Dlog.transform.SetParent(canvas.transform, false);
    }
    public void Obtain()
    {
        GameObject Dobt = Instantiate(ObtainBox) as GameObject;
        Dobt.transform.SetParent(canvas.transform, false);
        Dobt.GetComponent<ObtainBox>().setImage(Opic);
        Dobt.GetComponent<ObtainBox>().setName(Otex);

    }

}
