using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public Canvas pauScreen;
    //public GUISkin showGUI;
    private bool paused;
    private Move moveScript;
    private Ouros Ouros;

    private int choose;
    public RectTransform swo;
    //resume 1
    //save 2
    //exit 3
    // Use this for initialization
    void Start ()
    {
        moveScript = FindObjectOfType<Move>();
        Ouros = FindObjectOfType<Ouros>();
        paused = false;	
	}

    public void pausefnc()
    {
        if (Input.GetButtonDown("Esc"))
        {
            choose = 1;
            
            if (Time.timeScale == 1)
            {
                pauScreen.GetComponent<Canvas>().enabled = true;
                moveScript.setIngamePut(false);
                paused = true;
                Time.timeScale = 0;
            }
            else
            {
                pauScreen.GetComponent<Canvas>().enabled = false;
                moveScript.setIngamePut(true);
                paused = false;
                Time.timeScale = 1;
            }
        }
    }
    void sword()
    {
        if (choose == 1)
        {
            swo.localPosition = new Vector3(130, 150, 0);
        }
        else if (choose == 2)
        {
            swo.localPosition = new Vector3(100, 70, 0);
        }
        else if (choose == 3)
        {
            swo.localPosition = new Vector3(205, -30, 0);
        }
    }
    void toggle()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                choose += 1;
            } else if (Input.GetAxisRaw("Vertical") > 0)
            {
                choose -= 1;
            }

            if (choose > 3)
            {
                choose = 1;
            }
            if (choose < 1)
            {
                choose = 3;
            }
        }

        if (Input.GetButtonDown("Interact"))
        {
            enter();
        }
    }
    void enter()
    {
        if (choose == 1)
        {
            pauScreen.GetComponent<Canvas>().enabled = false;
            moveScript.setIngamePut(true);
            Time.timeScale = 1;
        } else if (choose == 2)
        {
            Ouros.Load();
        }
        else if (choose == 3)
        {
            
        }
        
    }
    // Update is called once per frame
    void Update ()
    {
        pausefnc();
        if (paused == true)
        {
            toggle();
            sword();
        }
        

    }
}
