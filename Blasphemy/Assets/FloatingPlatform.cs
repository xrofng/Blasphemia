using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour {

    
    public float duration;
    private float timecount;
    public float speed;
    public Vector2 direction;
    //trigger
    public bool isTrigger;
    public bool playerOn;

    private Ouros Ouros;
    // Use this for initialization
    void Start () {
		Ouros = FindObjectOfType<Ouros>();
    }
	
	// Update is called once per frame
	void Update () {


            timecount += Time.deltaTime;
            if (timecount > duration)
            {
                direction = new Vector2(direction.x * -1, direction.y * -1);
                timecount = 0;
            }
            transform.Translate(new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime);
            if (playerOn == true)
            {
                Ouros.transform.Translate(new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime);
            }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            playerOn = true;            
        } 
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerOn = false;

        }
    }
}
