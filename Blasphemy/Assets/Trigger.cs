using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Trigger : MonoBehaviour {

    public enum triggering
    {
        Text,
        Sound,
        CameraEvent,
        Video,
        Picture,
        CameraEventText,
        SoundCameraEvent,
        SoundAndSub,
        AnimateScene
    }
    public triggering triggerType;
    public int No;
    public Trigger[] duplicate;
    private D d;
    private bool start;
    public float delay;
    private float timmcount;

    void Start()
    {
        start = false;
        timmcount = 0.0f;
        d = FindObjectOfType<D>();
    }
   public  void checkType()
    {
        switch (triggerType)
        {
            case triggering.Text:
                d.playDialogue(No);
                break;
            case triggering.Sound:
                d.playAudio(No);
                break;
            case triggering.CameraEvent:
                d.playCameraEvent(No);
                break;
            case triggering.Video:
               
                break;
            case triggering.Picture:
                
                break;
            case triggering.CameraEventText:
                d.playCameraEvent(No);
                d.playDialogue(No);
                break;
            case triggering.SoundCameraEvent:
                d.playAudio(No);
                d.playCameraEvent(No);                
                break;
            case triggering.SoundAndSub:
                d.playAudio(No);
                d.playDialogue(No);
                break;
        }
    }

	// Use this for initialization
	
	int getNo()
    {
        return No;
    }
	// Update is called once per frame
	void Update () {
		if (start == true)
        {
            timmcount += Time.deltaTime;
            if (timmcount > delay)
            {
                checkType();
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.tag == "Player")
        {
            start = true;
            for (int i =0; i< duplicate.Length; i++)
            {
                Destroy(duplicate[i].gameObject);
            }           
        }
    }
}
