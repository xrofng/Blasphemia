using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeZero : MonoBehaviour {
    private Ouros Ouros;
    public Color blackColor;
    public bool fadedIn;
    public bool fadedOut;
    public Image black;
    public Canvas canvas;
    private float alpha;
    public bool isFading;
    public bool fadeFinish;
    // Use this for initialization
    void Start () {
        Ouros = FindObjectOfType<Ouros>();
        isFading = false;
        fadeFinish = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (fadedIn == true)
        {
            alpha -= 0.05F;
            black.color = new Color(0.0F, 0.0F, 0.0F, alpha);
            if (alpha <= 0.0f)
            {
                fadeFinish = true;
            }

        } else if (fadedOut == true)
        {
            alpha += 0.05F;
            black.color = new Color(0.0F, 0.0F, 0.0F, alpha);
            if (alpha >= 1.0f)
            {
                fadeFinish = true;
            }
        }
	}

    public void fadeIn()
    {
        if (isFading == false)
        {
            isFading = true;
            fadedOut = false;
            fadedIn = true;
            black.enabled = true;
            alpha = 1.0F;
            black.color = new Color(0.0F, 0.0F, 0.0F, alpha);
        }
       

    }

    public void fadeOut()
    {
        if (isFading == false)
        {
            isFading = true;
            Debug.Log(black.color);
            fadedIn = false;
            fadedOut = true;
            black.enabled = true;
            alpha = 0.0F;
            black.color = new Color(0.0F, 0.0F, 0.0F, alpha);
        }
        
       
    }
}
