using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Trigger : MonoBehaviour {

    public enum triggering
    {
        Text,
        Sound,
        Video,
        Picture
    }
    public triggering triggerType;
    public int No;
    private D d;
    void Start()
    {
        d = FindObjectOfType<D>();
    }
    void checkType()
    {
        switch (triggerType)
        {
            case triggering.Text:
                d.playDialogue(No);
                break;
            case triggering.Sound:
                d.playAudio(No);
                break;
            case triggering.Video:
               
                break;
            case triggering.Picture:
                
                break;
        }
    }

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            checkType();
            Destroy(gameObject);
        }
    }
}
