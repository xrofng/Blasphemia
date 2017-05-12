using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDrop : MonoBehaviour {

    public Item[] dropList;
    public int[] amount;
    public int[] percent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int ran = Random.Range(0, 100);
        int oldpercent = 0;
        bool willDrop=false;
        Item drop=null;
        for (int i = 0; i < dropList.Length; i++)
        {
            if (ran >= oldpercent && ran < percent[i] + oldpercent)
            {
                willDrop = true;
                drop = dropList[i];
                drop.amount = amount[i];
            }
            oldpercent = percent[i] + oldpercent;
        }
        if (willDrop == true)
        {
            Instantiate(drop, this.GetComponent<Transform>().position, Quaternion.identity);
        }else
        {

        }
        Destroy(gameObject);

    }
}
