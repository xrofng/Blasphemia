using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMelee : MonoBehaviour {

    public int ATK;
    private Ouros Ouros;

    // Use this for initialization
    void Start () {
        //ATK = this.transform.parent.gameObject.GetComponent<EnemyAI>().ATK;
        Ouros = FindObjectOfType<Ouros>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //when got hit by enemy attack // des it's magic
        {
            if(Ouros.isInvisible == false)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                Ouros.HP -= ATK - Ouros.DEF / ATK;
                this.GetComponent<BoxCollider2D>().enabled = false;
                Ouros.knockBack(1f);
            }
            
        }




    }
}
