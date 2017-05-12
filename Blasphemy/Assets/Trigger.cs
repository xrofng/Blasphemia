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
        AnimateScene,
        
    }
    public triggering triggerType;
    public int No;
    public Trigger[] duplicate;
    private D d;
    private bool start;
    public float delay;
    private float timmcount;
	public bool yetLaew;
    public bool monster;
    public string monName;
    private bool stopga;
   

    void Start()
    {
        setStop();
		if (yetLaew == true){
			Destroy(gameObject);
		}
        start = false;
        timmcount = 0.0f;
        d = FindObjectOfType<D>();
        
       
    }
    public void setStop()
    {
        switch (triggerType)
        {
            case triggering.Text:
               
                break;
            case triggering.Sound:
               
                break;
            case triggering.CameraEvent:
                stopga = true;
               
                break;
            case triggering.Video:

                break;
            case triggering.Picture:

                break;
            case triggering.CameraEventText:
                stopga = true;
              
                break;
            case triggering.SoundCameraEvent:
                stopga = true;
               
                break;
            case triggering.SoundAndSub:
                
                break;

        }
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
                stopga = true;
                d.playCameraEvent(No);
                break;
            case triggering.Video:
               
                break;
            case triggering.Picture:
                
                break;
            case triggering.CameraEventText:
                stopga = true;
                d.playCameraEvent(No);
                d.playDialogue(No);
                break;
            case triggering.SoundCameraEvent:
                stopga = true;
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
	public void setYetlaew(bool y)
	{
		yetLaew = y;
	}
	int getNo()
    {
        return No;
    }
   
    
	// Update is called once per frame
	void Update () {
        if (monster == true)
        {
            if (monName == "Marlanx" && this.GetComponent<marlanxAI>().isDead == true)
            {
                d.playDialogue(No);
            }
        }
        
        if (start == true)
        {
            if (stopga == true)
            {
                FindObjectOfType<Move>().setIngamePut(false);
            }
            timmcount += Time.deltaTime;
            if (timmcount > delay)
            {
                checkType();                
                yetLaew = true;
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (monster == false)
        {
            if (other.gameObject.tag == "Player")
            {
                
                start = true;
                
                for (int i = 0; i < duplicate.Length; i++)
                {
                    Destroy(duplicate[i].gameObject);
                }
            }
        }
       
    }

}
