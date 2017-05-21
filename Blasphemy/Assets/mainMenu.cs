using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

    private int choose;
    public RectTransform cursor;
    public Text winful;
    public Text newg;
    public Text con;
    private int currentMode;

    static public bool GameWillLoadSave;

	
	void Start () {
        choose = 1;       
        Cursor.visible = false;
        // ***YoYo the Great - implement in start to check what mode is it and set currentMode 0=full 1=window
    }



    void cursosur()
    {
       
        if (choose == 1)
        {
            cursor.localPosition = new Vector3(0, newg.GetComponent<Transform>().localPosition.y, 0);
        }
        else if (choose == 2)
        {
            cursor.localPosition = new Vector3(0, con.GetComponent<Transform>().localPosition.y, 0);
        } else if (choose == 3)
        {
            cursor.localPosition = new Vector3(0, winful.GetComponent<Transform>().localPosition.y, 0);
        }
    }
    void toggle()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                choose += 1;
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
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
            GameWillLoadSave = false;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if (choose == 2)
        {
            GameWillLoadSave = true;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        } else if (choose == 3)
        {
            // ***YoYo the Great - implement switch in this check currentMode blah blah
        }
        

    }
    void Update()
    {
        toggle();
        cursosur();
    }
}
