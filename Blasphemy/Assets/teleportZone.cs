using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportZone : MonoBehaviour {

    public Transform tp;
    public string toWhatZone;
    private Ouros Ouros;
    public AudioSource BGM;
    public AudioClip[] bgmDream;


	// Use this for initialization
	void Start () {
        Ouros = FindObjectOfType<Ouros>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void changeSong()
    {
        if (toWhatZone == "Forest")
        {
            BGM.clip = bgmDream[0];
        } else if (toWhatZone == "Snow")
        {
            BGM.clip = bgmDream[1];
        }
        else if (toWhatZone == "Lava")
        {
            BGM.clip = bgmDream[2];
        }
        BGM.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            float x = tp.GetComponent<Transform>().position.x;
            float y = tp.GetComponent<Transform>().position.y;
            float z = Ouros.GetComponent<Transform>().position.z;
            Ouros.gameObject.GetComponent<Transform>().position = new Vector3(x, y, z);
            changeSong();

        }
    }
}
