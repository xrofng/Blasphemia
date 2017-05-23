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

    //trigger
    public int[] triggerstate;
    public GameObject[] goArray;
    public string nameToArray = "TriggerPoint";
    private int count = 0;
    //equipment

    //public Weapon weapon;

    public Item item;
    public Item[] itemList;
    private int itemLable = 1;
    public GameObject attackPar;
    public Magic magic;
    public Magic[] magicList;
    public bool magicActive;
    private float magicTimeCount;
    private int magicLable = 1;
    public int maxMagi=2;
    public Image cd;
    public Image cdcircle;
    public Sprite active;
    public Sprite inactive;
    public GameObject PotionParticle;

    public bool red=false;
    public bool isInvisible;
    private float invicount;
    public float invitime;
    public Color inviColor;
    public Color normalColor;
    public Color damagedColor;



    //public Item buffItem;

    private Move moveScript;

    private bool DebugModes;
    //camera
    public Transform cameraHolder;
    public float CameraSpeed;
    public float deCameraSpeed;
    public CameraMovementType cameraMovementType;
    bool moveCamera;
    public enum CameraMovementType
    {
        Lerp,
        MoveTowards,
        AccelDecel,
        Acceleration
    }


    void Start()
    {
        deCameraSpeed = CameraSpeed;
        isInvisible = false;
        healthBar.maxValue = maxHP;
        moveScript = GetComponent<Move>();
        magicLable = 1;
        if (mainMenu.GameWillLoadSave == true || gameOver.GameWillLoadSave == true)
        {
            Load();
        }
        cameraHolder = Camera.main.transform.parent.transform;
    }
    void Update()
    {
        playerCamera();
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
        blinking();
    }
    public void Save()
    {
        List<GameObject> goList = new List<GameObject>();
        string objName = "TriggerPoint";
        foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (go.tag == objName)
                goList.Add(go);
        }
        for (int i = 0; i < goList.Count; i++)
        {
            goList[i].GetComponent<Trigger>().saveTrigger();
        }

        Debug.Log(Application.dataPath);
        SaveLoadManager.SavePlayer(this);
    }

    public void Load()
    {
        List<GameObject> goList = new List<GameObject>();
        string objName = "TriggerPoint";
        foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (go.tag == objName)
                goList.Add(go);
        }
        for (int i = 0; i < goList.Count; i++)
        {
            goList[i].GetComponent<Trigger>().loadTrigger();
        }


        int[] loadedStats = SaveLoadManager.LoadStats();
        healthBar.value = loadedStats[0];
        maxHP = loadedStats[1];
        HP = loadedStats[2];
        ATK = loadedStats[3];
        DEF = loadedStats[4];
        float[] loadedPos = SaveLoadManager.LoadPosition();
        gameObject.transform.position = new Vector3(loadedPos[0], loadedPos[1], loadedPos[2]);
    }

    void playerCamera()
    {
        
        float disFromCamera = Vector3.Distance(cameraHolder.position, transform.position);
        bool isJumping = GetComponent<Move>().jumping;
        if (disFromCamera > 2 && isJumping == false)
        {
            moveCamera = true;
        }
        else
        {
            if (disFromCamera < 0.1f)
            {
                moveCamera = false;
            }
        }

        if (moveCamera)
        {
            CameraMovement();
        }
    }
    void CameraMovement()
    {
        Vector3 camAbocve = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
        switch (cameraMovementType)
        {
            
            case CameraMovementType.Lerp:
                cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position, transform.position, Time.deltaTime * CameraSpeed);
                break;
            case CameraMovementType.MoveTowards:
                cameraHolder.transform.position = Vector3.MoveTowards(cameraHolder.transform.position, transform.position, Time.deltaTime * CameraSpeed);
                break;
            case CameraMovementType.AccelDecel:
                cameraHolder.transform.position = Interpolation.AccelDecelInterpolation(cameraHolder.position, camAbocve, Time.deltaTime * CameraSpeed);
                break;
            case CameraMovementType.Acceleration:
                cameraHolder.transform.position = Interpolation.AccelerationInterpolation(cameraHolder.position, camAbocve, Time.deltaTime * CameraSpeed, 1);
                break;
        }

    }
    void DebugMode()
    {
        if (Input.GetButton("DebugMode"))
        {
            FindObjectOfType<Variable>().var_spear += 1;
            if (DebugModes == true)
            {
                moveScript.doubleJumpAbilities = false;
                DebugModes = false;
            } else
            {
                moveScript.doubleJumpAbilities = true;
                DebugModes = true;
            }
        }
        if (DebugModes == true)
        {
            HP = maxHP;
        }
    }

 
    void updatePosition()
    {
        xAxis = gameObject.transform.position.x;
        yAxis = gameObject.transform.position.y;
        zAxis = gameObject.transform.position.z;
    }
    public void recieveDamage(int dmg)
    {
        this.HP -= dmg - this.DEF / dmg;
    }
    void checkDie()
    {
        
        if (this.HP <= 0)
        {

            FadeZero fz = FindObjectOfType<FadeZero>();

            fz.fadeOut();
            if (fz.fadeFinish == true)
            {
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
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
                item.useItem();
                GameObject PotionParc = Instantiate(PotionParticle) as GameObject;
                PotionParc.transform.SetParent(moveScript.transform, false);
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
            cdcircle.sprite = inactive;

        } else if (magicActive == true)
        {
            cd.color = new Color(1.0F, 1.0F, 1.0F, 1.0F);
            cdcircle.sprite = active;
        } else
        {

        }
    }

    void swapMagic()
    {    
        
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
            magic = magicList[magicLable];
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
                        magic.setDirection(Vector2.left);
                    }
                    else if (GetComponent<SpriteRenderer>().flipX == false)
                    {
                        magic.setDirection(Vector2.right);
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

    public void blinking()
    {
        if (isInvisible == true)
        {
           
            if (invicount > invitime-0.50 && red== false)
            {
                if (GetComponent<SpriteRenderer>().material.color == damagedColor)
                {
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", normalColor);
                    red = true;
                }
            }
            invicount += Time.deltaTime;
            if (red == true)
            {
                if (invicount > invitime)
                {
                    invicount = 0;
                    red = false;
                    isInvisible = false;
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", normalColor);
                    return;
                }

                if (GetComponent<SpriteRenderer>().material.color == inviColor)
                {
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", normalColor);
                }
                else if (GetComponent<SpriteRenderer>().material.color == normalColor)
                {
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", inviColor);
                }
            }
            
            
        }           
    }

    public void knockBack(float dis)
    {
        GetComponent<SpriteRenderer>().material.SetColor("_Color", damagedColor);
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            //this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * dis);
            transform.Translate(Vector3.right* dis);
        }
        else if (GetComponent<SpriteRenderer>().flipX == false)
        {
           // this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.left * dis);
            transform.Translate(Vector3.left * dis);
        }        
        isInvisible = true;
    }

    

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "EnemyAttack") //when got hit by enemy attack // des it's magic
        {
            if (isInvisible == false)
            {
               // HP -= other.gameObject.GetComponent<Magic>().Dmg - this.DEF / other.gameObject.GetComponent<Magic>().Dmg;
                other.GetComponent<selfDestruct>().destroyNow();
                knockBack(0.2f);
            }
           
        }
       else if (other.gameObject.tag == "EnemyMeleeAttack") //when got hit by enemy melee attack // not dest
        {
            
        } else
        {

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyAttack") //when got hit by enemy attack // des it's magic
        {
            if (isInvisible == false)
            {
               //HP -= other.gameObject.GetComponent<Magic>().Dmg - this.DEF / other.gameObject.GetComponent<Magic>().Dmg;
                
            }

        }
    }
}