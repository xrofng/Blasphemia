using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{

    private int choose;
    public RectTransform cursor;

    static public bool GameWillLoadSave;

    // Use this for initialization
    void Start()
    {
        choose = 1;

    }



    void cursosur()
    {
        
        if (choose == 1)
        {
            cursor.localPosition = new Vector3(0, 20.4f, 0);
        }
        else if (choose == 2)
        {
            cursor.localPosition = new Vector3(0, -24.2f, 0);
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

            if (choose > 2)
            {
                choose = 1;
            }
            if (choose < 1)
            {
                choose = 2;
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
            GameWillLoadSave = true;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if (choose == 2)
        {
            GameWillLoadSave = false;
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }


    }
    // Update is called once per frame
    void Update()
    {
        toggle();
        cursosur();
    }
}
