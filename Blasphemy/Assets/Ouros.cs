using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ouros : MonoBehaviour
{

    //private
    public Slider healthBar;
    public int maxHP;
    public int HP;
    public int ATK;
    public int DEF;

    //equipment

    //public Weapon weapon;
    public Item item;
    //public Item buffItem;

    private Move moveScript;

    void Start()
    {
        healthBar.maxValue = maxHP;
        moveScript = GetComponent<Move>();
    }

    void Update()
    {
        use_Item();
        healthBar.value = HP;
    }

    void use_Item()
    {
        if (Input.GetButtonDown("Item") && moveScript.isOnGround == true)
        {
            if (item.amount > 0)
            {
                Debug.Log("before use");
                print(item.amount);
                item.useItem();
                item.amount -= 1;

            }
            else
            {
                Debug.Log("can't use");
            }

        }

    }

}
