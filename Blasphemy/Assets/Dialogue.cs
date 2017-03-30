using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    
    public float chrono;
    public string[] dialogue;
    private bool enterPressed;
    public float charRate;
    private bool started;
    private int num;

    public bool permit;
    public GameObject textBox;
    private Text text;
    // Use this for initialization

    public Dialogue(string descrip)
    {
        text.text = descrip;
    }

    void permissive()
    {
        if(permit == true)
        {
            text.enabled = true;
            textBox.GetComponent<Image>().enabled = true;
        } else
        {
            text.enabled = false;
            textBox.GetComponent<Image>().enabled = false;
        }
    }
    void Start () {
        text = GetComponent<Text>();
        if (permit == true)
        {
            text.text = "";
            enterPressed = false;
            num = 0;
            started = false;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        if (started == false)  {
            StartCoroutine(DisplayDialogue(dialogue[num]));
            started = true;
            num += 1;
        }
        permissive();

    }

    private IEnumerator DisplayDialogue(string d){
        
        text.text = "";
        int charCount = 0;
        while (charCount < d.Length){
            text.text += d[charCount];
            charCount++;
            if (charCount < d.Length) {               
                if (Input.GetButton("Return"))
                {
                    yield return new WaitForSeconds(chrono * charRate);
                }
                else
                {
                    yield return new WaitForSeconds(chrono);
                }
            }            
            else {
                break;
            }
        }     
          
        

        while (true)
        {
            if (Input.GetButtonDown("Return"))
            {
                if (num == dialogue.Length -1)
                {

                    Destroy(textBox);
                }
                break;
            }
            yield return 0;
        }
        started = false;
        enterPressed = false;
        text.text = "";
    }
}
