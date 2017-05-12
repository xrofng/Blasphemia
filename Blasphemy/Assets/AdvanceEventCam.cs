using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceEventCam : MonoBehaviour
{
    //  public float speed;
    // public List<EventStuff> crapScene;
    private D d;
    private EventStuff currentEvent;
    public int choose;
    private int sequence;
    public Camera main;
    private bool play;
    private Vector3 origin;
    private Move movescript;
    private float timeCount;
    private float stayTime;
    // Use this for initialization
    void Start ()
    {
        movescript = FindObjectOfType<Move>();
        d = FindObjectOfType<D>();
        this.GetComponent<Camera>().enabled = false;
        sequence = 0;
        play = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (play == true)
        {
            movescript.setIngamePut(false);
            //currentEvent = crapScene[choose];
            currentEvent = d.CameraList[choose];
              
            Vector3 directionNew = new Vector3(currentEvent.goodshit[sequence].position.x - this.transform.position.x, currentEvent.goodshit[sequence].position.y - this.transform.position.y, currentEvent.goodshit[sequence].position.z - this.transform.position.z);
            
            Vector3 eventCam = this.transform.position + GetComponentInParent<Transform>().position;
            Vector3 destination = currentEvent.goodshit[sequence].position + GetComponentInParent<Transform>().position;
          
            if (eventCam != destination)
            {
                transform.Translate(directionNew * currentEvent.speed[sequence] * Time.deltaTime, 0);
                timeCount += Time.deltaTime;
                stayTime = currentEvent.delay[sequence];
                if (timeCount >= stayTime)
                {
                    timeCount = 0;
                    if (sequence + 1 >= currentEvent.goodshit.Count)
                    {
                        play = false;
                        main.enabled = true;
                        this.GetComponent<Camera>().enabled = false;
                        this.GetComponent<Transform>().position = origin;
                        movescript.setIngamePut(true);
                        FadeZero f = FindObjectOfType<FadeZero>();
                        f.startnewFade();
                        f.fadeIn();
                        f.startnewFade();
                    }
                    else
                    {
                        sequence += 1;
                    }
                }
                
            } else
            {
            }
        } else
        {
            origin = this.GetComponent<Transform>().position;
        }
        
        
	}

    public void StartCam(int No)
    {
        choose = No;
        play = true;
        main.enabled = false;
        this.GetComponent<Camera>().enabled = true;
    }

    IEnumerator Stay(float time)
    {
        yield return new WaitForSeconds(time);
    }


}
