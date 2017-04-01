using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Ouros : MonoBehaviour
{

    //private
    public Slider healthBar;
    public int maxHP;
    public int HP;
    public int ATK;
    public int DEF;

    //pos
    public float xAxis;
    public float yAxis;
    public float zAxis;


    //equipment

    //public Weapon weapon;

    public Item item;
    public Item[] itemList;
    private int itemLable = 1;

    public Magic magic;
    public Magic[] magicList;
    public bool magicActive;
    private float magicTimeCount;
    private int magicLable = 1;
    public int maxMagi=2;
    public Image cd;
    //public Item buffItem;

    private Move moveScript;

    private bool DebugModes;

    void Start()
    {
        healthBar.maxValue = maxHP;
        moveScript = GetComponent<Move>();
        magicLable = 1;
    }
    public void Save()
    {
        Debug.Log(Application.dataPath);
        SaveLoadManager.SavePlayer(this);
    }

    public void Load()
    {
        int[] loadedStats = SaveLoadManager.LoadStats();

        healthBar.value = loadedStats[0];
        maxHP = loadedStats[1];
        HP = loadedStats[2];
        ATK = loadedStats[3];
        DEF = loadedStats[4];

        float[] loadedPos = SaveLoadManager.LoadPosition();
        gameObject.transform.position = new Vector3(loadedPos[0], loadedPos[1], loadedPos[2]);
    }

    void DebugMode()
    {
        if (Input.GetButton("DebugMode"))
        {
            if (DebugModes == true)
            {
                DebugModes = false;
            } else
            {
                DebugModes = true;
            }
        }
        if (DebugModes == true)
        {
            HP = maxHP;
        }
    }

    void Update()
    {
        DebugMode();
        updatePosition();
        cooldown_gauge();
        swapItem();
        use_Item();
        passive_Magic();
        swapMagic();
        use_Magic();
        healthBar.value = HP;
        checkDie();
    }
    void updatePosition()
    {
        xAxis = gameObject.transform.position.x;
        yAxis = gameObject.transform.position.y;
        zAxis = gameObject.transform.position.z;
    }
    void checkDie()
    {
        if (this.HP <= 0)
        {
            SceneManager.LoadScene(0);
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
        
        if (Input.GetButtonDown("MagicChange") && maxMagi!=2)
        {
            
            if (Input.GetAxisRaw("MagicChange") == 1)
            {
                if (magicLable + 1 == maxMagi)
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
                    magicLable = maxMagi - 1;
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

    public void knockBack(float dis)
    {
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.Translate(Vector3.right* dis);
        }
        else if (GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.Translate(Vector3.left * dis);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "EnemyAttack") //when got hit by enemy attack
        {
            HP -= other.gameObject.GetComponent<Magic>().Dmg - this.DEF/ other.gameObject.GetComponent<Magic>().Dmg;
            other.GetComponent<selfDestruct>().destroyNow();
            knockBack(0.2f);
        }
        
        

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")//when got hit by enemy body
        {
            HP -= other.gameObject.GetComponent<EnemyAI>().ATK - this.DEF / other.gameObject.GetComponent<EnemyAI>().ATK;
            knockBack(0.2f);
        }
        else
        {

        }
    }
}

       
   
