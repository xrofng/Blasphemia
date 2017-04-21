using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perspectiveBG : MonoBehaviour {

    private Ouros Ouros;
    public GameObject background;
    public float maxMoveX;
    public float maxMoveY;
    public Transform centerObj;
    public Vector3 realPixellength;
    private Vector3 center;
    private Vector3 ourospos;
    private Vector3 move;
    private bool scroll;

    // Use this for initialization
    void Start () {
        Ouros = FindObjectOfType<Ouros>();
        center = centerObj.position;
    }

	// Update is called once per frame
	void Update () {
        ourospos = Ouros.GetComponent<Transform>().position;
        ourospos.z = 0;
        Vector3 ad = center - ourospos;
        move.x = ad.x * (maxMoveX / realPixellength.x);
        move.y = ad.y * (maxMoveY / realPixellength.y);
        Vector3 bg = background.GetComponent<Transform>().position;
        bg.z = 0;
        if (bg != bg-move)
        {
            Debug.Log("BG " + bg);
            Debug.Log("move " + move);
            Debug.Log("maxBGmove " + (bg - move));
            Debug.Log("ouros " + ourospos);
           
            float dt = Time.deltaTime;
            if (ad.x > 0)
            {

            } else if (ad.x < 0)
            {
                 
            } else if (ad.x == 0)
            {

            }

            if (scroll == true)
            {
                background.GetComponent<Transform>().Translate(-move.x * dt, -move.y * dt, 0.0f);
            }            
            Debug.Log("after " + background.GetComponent<Transform>().position);

        }

    }
}
