using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float mySpeed = 2.55f;
    //private Animator anime;
    [SerializeField]
    //private bool inSight = false;
    private Collider2D copied;
    private float init = 3.0f;
    public GameObject magic;
    public int direction = 0;
    private float delay = 3.0f;
    public Transform spawnPoint;
    private int magicDel = 0;
    // Use this for initialization
    void walk()
    {
        if (direction==0)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
            init -= Time.deltaTime;

            if(init<=0.0f)
            {
                shootCrap();
                magicDel = 1;
                init = 3.0f;
                direction = 1;
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
        else if(direction==1)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
            init -= Time.deltaTime;

            if (init <= 0.0f)
            {
                shootCrap();
                magicDel = 1;
                init = 3.0f;
                direction = 0;
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
    }

    void shootCrap()
    {
        if (magicDel==0)
        {
            Instantiate(magic, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        if (magicDel == 1)
        {
            Instantiate(magic, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Player")) // "EnemyUnit"
    //    {
    //        inSight = true;
    //        if(copied == null)
    //        {
    //            copied = other;
    //        }
    //    }
    //}
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    inSight = false;
    //    copied = null;
    //}

    // Update is called once per frame
    void Update()
    {
        //if (inSight == false)
        //{
        //    walk();
        //}
        //else if (inSight != false)
        //{

        //}
        walk();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            Destroy(gameObject);
        }
        else
        {

        }

    }
}
