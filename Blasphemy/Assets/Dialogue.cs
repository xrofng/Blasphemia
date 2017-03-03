using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    private Text text;
    public float chrono;
    public string[] dialogue;
    private bool enterPressed;
    public float charRate;
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        text.text = "";
        enterPressed = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Return") && enterPressed == false)
        {
            text.text = "";
            enterPressed = true;
            StartCoroutine(DisplayDialogue(dialogue[0]));
        }
	}

    private IEnumerator DisplayDialogue(string d)
    {
        text.text = "";
        for (int i = 0; i < d.Length; i++) {
            text.text += d[i];
            
            if (i < d.Length)
            {
                if (i < d.Length)
                {
                    if (Input.GetButton("Return"))
                    {
                        yield return new WaitForSeconds(chrono*charRate);
                    } else
                    {
                        yield return new WaitForSeconds(chrono);
                    }
                }

               
            } else
            {
                break;
            }
        }

        while (true)
        {
            if (Input.GetButtonDown("Return"))
            {
                break;
            }
            yield return 0;
        }
        enterPressed = false;
        text.text = "";
    }
}
