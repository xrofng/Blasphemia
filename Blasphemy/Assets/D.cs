using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class D : MonoBehaviour {
    // D stand from Display
    //dialogue
    static public int sequence;
    public Transform canvas;
    public GameObject[] DialogueList;
    private float timeCount = 0f;
    private float delayTime = 0.5f;
    private bool count;
    private bool start;

    //sound
    public AudioClip[] SoundList;
    public AudioSource cometPlayer;
    //camera
    public EventStuff[] CameraList;
    private AdvanceEventCam aec;
    //video
    public VideoClip[] VideoList;
    public VideoStream vs;
    //obtainBox
    public GameObject ObtainBox;
    public Sprite Opic;
    public string Otex;
    //spawnDestroy
    public GameObject[] spaNdes;
    // Use this for initialization
    void Start () {
        aec = FindObjectOfType<AdvanceEventCam>();
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
      
      
    }
    public void spawnSomething(int no)
    {
        Instantiate(spaNdes[no]);
    }
    public void destroySomething(int no)
    {        
        foreach (Transform child in spaNdes[no].transform)
        {
            child.GetComponentInChildren<selfDestruct>().enabled = true;
        }
        spaNdes[no].GetComponent<selfDestruct>().enabled = true;
    }
    public void playDialogue(int no)
    {
        GameObject Dlog = Instantiate(DialogueList[no]) as GameObject;
        Dlog.transform.SetParent(canvas.transform, false);
    }
    public void playAudio(int no)
    {
        cometPlayer.clip = SoundList[no];
        cometPlayer.Play();
    }
    public void playCameraEvent(int no)
    {
        aec.StartCam(no);
    }
    public void playVideo(int no)
    {
        vs.startVideo(VideoList[no]);
    }
    public void Obtain()
    {
        GameObject Dobt = Instantiate(ObtainBox) as GameObject;
        Dobt.transform.SetParent(canvas.transform, false);
        Dobt.GetComponent<ObtainBox>().setImage(Opic);
        Dobt.GetComponent<ObtainBox>().setName(Otex);
    }

}
