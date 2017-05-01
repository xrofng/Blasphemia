using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpell : MonoBehaviour
{
    public string magicName;
    public string type;
    public float speed;
    public Vector2 travelDir;
    public int Dmg;
    public Transform spawner;
    private Ouros Ouros;
    public float timer = 0.0f;
    [SerializeField]
    private float rotSpeed;
    public int direction;
    // Use this for initialization
    void Start () {
        Ouros = FindObjectOfType<Ouros>();
        if(magicName=="Poison" || magicName=="Snowball")
        {
            travelDir = new Vector2(Ouros.transform.position.x - gameObject.transform.position.x, Ouros.transform.position.y - gameObject.transform.position.y);
        }
    }
	public void setDirection(int d)
    {
        direction = d;
    }
	// Update is called once per frame
	void Update ()
    {
		if(magicName=="Poison")
        {
            transform.Translate(travelDir.x * speed * Time.deltaTime, travelDir.y * speed * Time.deltaTime, 0);
        }
        else if(magicName=="DotPoison")
        {
            timer += Time.deltaTime;
        }
        else if(magicName=="Snowball")
        {
            gameObject.transform.Rotate(0, 0, Time.deltaTime * rotSpeed, Space.Self);
            //transform.Translate(, 0,Space.World);            
            transform.Translate(Vector3.right * direction *speed* Time.deltaTime,Space.World);
        }
	}
}
