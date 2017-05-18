using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveMaze : MonoBehaviour {

    public bool entrance;
    public CaveMaze ent;
    public CaveMaze[] hole;
    public GameObject[] destination;    
    public int desti;
    public int h;
    public int leftorright;
    public int holw1;
    public int holw2;
    public int pseudo;
    // Use this for initialization
    void Start () {

        pseudo = 25;
}
	
	// Update is called once per frame
	void Update () {
		
	}
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (this.entrance == true)
            {                           
                for(int i = 0; i < 10; i++)
                {
                    int ran = Random.Range(0, 100);
                    if (ran >=0 && ran < pseudo)
                    {
                        hole[i].desti = 1;
                    }else
                    {
                        hole[i].desti = 0;
                    }                   
                }
                h = Random.Range(0, 9);
                if(h>=0 && h < 5)
                {
                    leftorright = 0;                    
                    holw2 = h + 1;
                } else
                {
                    leftorright = 1;
                    holw2 = h- 5 + 1;
                }
                hole[h].desti = 2;
            }else
            {
                FadeZero f = FindObjectOfType<FadeZero>();
                f.startnewFade();
                f.fadeOut();
                if(this.desti == 0)
                {
                    ent.pseudo = 100;
                }
                if (this.desti == 1)
                {
                    ent.pseudo = 25;
                }
                float x = destination[desti].GetComponent<Transform>().position.x;
                float y = destination[desti].GetComponent<Transform>().position.y;
                float z = destination[desti].GetComponent<Transform>().position.z;
                other.gameObject.GetComponent<Transform>().position = new Vector3(x, y, z);
                StartCoroutine(delay());
                
                
                
            }
        }
    }

    IEnumerator delay()
    {       
        yield return new WaitForSeconds(1.23f);
        FadeZero f = FindObjectOfType<FadeZero>();
        f.startnewFade();
        f.fadeIn();
    }

}
