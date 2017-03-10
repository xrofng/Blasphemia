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
    public Item[] itemList;
    private int itemLable = 1;

    public Magic magic;
    public Magic[] magicList;
    private bool magicActive;
    private float magicTimeCount;
    private int magicLable = 1;
    public Image cd;
    //public Item buffItem;

    private Move moveScript;

    void Start()
    {
        healthBar.maxValue = maxHP;
        moveScript = GetComponent<Move>();
        magicLable = 1;
    }

    void Update()
    {
        cooldown_gauge();
        swapItem();
        use_Item();
        passive_Magic();
        swapMagic();
        use_Magic();
        healthBar.value = HP;
        checkDie();
    }

    void checkDie()
    {
        if (this.HP <= 0)
        {

        }
    }

    void swapItem()
    {
        item = itemList[itemLable];
        if (Input.GetButtonDown("ItemChange"))
        {
            int maxitem = itemList.Length;
            if (itemLable + 1 == maxitem)
            {
                itemLable = 1;
            }
            else
            {
                itemLable += 1;
            }

        }
    }

    void use_Item()
    {
        if (Input.GetButtonDown("Item") && moveScript.isOnGround == true)
        {
            if (item.amount > 0)
            {
                item.useItem();
                item.amount -= 1;
            }
            else
            {

            }

        }

    }

    void passive_Magic()
    {
        if (magic.type == "deathclaw")
        {
            GetComponent<grapplingHook>().enabled = true;
        } else
        {
            GetComponent<grapplingHook>().enabled = false;
        }
    }

    void cooldown_gauge()
    {
        if (magicActive == false)
        {
            cd.color = new Color(0.2F, 0.2F, 0.2F, 1.0F);

        } else if (magicActive == true)
        {
            cd.color = new Color(1.0F, 1.0F, 1.0F, 1.0F);
        } else
        {

        }
    }

    void swapMagic()
    {
        magic = magicList[magicLable];
        if (Input.GetButtonDown("MagicChange"))
        {
            int maxMagic = magicList.Length;
            if (Input.GetAxisRaw("MagicChange") == 1)
            {
                if (magicLable + 1 == maxMagic)
                {
                    magicLable = 1;
                } else
                {
                    magicLable += 1;
                }

            }
            else if (Input.GetAxisRaw("MagicChange") == -1)
            {
                if (magicLable - 1 == 0)
                {
                    magicLable = maxMagic - 1;
                } else
                {
                    magicLable -= 1;
                }

            }
        }

    }

    void use_Magic()
    {
        if (Input.GetButtonDown("Special") && magicActive == true)
        {
            if (magic.charge > 0)
            {
                if (magic.type == "projectile")
                {
                    if (GetComponent<SpriteRenderer>().flipX == true)
                    {
                        magic.setDirection(-1);
                    }
                    else if (GetComponent<SpriteRenderer>().flipX == false)
                    {
                        magic.setDirection(1);
                    }
                    Instantiate(magic, gameObject.transform.position, gameObject.transform.rotation);
                }
                magic.charge -= 1;
            }
            else
            {

            }
            magicActive = false;
        }
        magicTimeCount -= Time.deltaTime;
        if (magicTimeCount < 0)
        {
            magicActive = true;
            magicTimeCount = 1;
        }




    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "EnemyAttack") //when got hit by enemy attack
        {
            this.HP -= 30;
            other.GetComponent<selfDestruct>().destroyNow();
        }
        
        

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")//when got hit by enemy body
        {
            HP -= 30;
        }
        else
        {

        }
    }
}

       
   
