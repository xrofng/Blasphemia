using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour
{
    private Ouros Ouros;
    private float translation_x;
    [SerializeField]
    private float speed = 5.0f;
    public int limit;

    void Start()
    {
        Ouros = FindObjectOfType<Ouros>();
    }

    void Update()
    {
        
        if (Input.GetButton("Camera"))
        {
            translation_x = Input.GetAxis("Camera");
            translation_x = Time.deltaTime * translation_x * speed;


            if (this.transform.position.x > Ouros.transform.position.x + limit || this.transform.position.x < Ouros.transform.position.x - limit)
            {

            }
            else {
                if (Ouros.GetComponent<SpriteRenderer>().flipX == true)
                {
                    transform.Translate(new Vector2(-translation_x, 0));
                }
                else if (Ouros.GetComponent<SpriteRenderer>().flipX == false)
                {
                    transform.Translate(new Vector2(translation_x, 0));
                }

            }
        }
        else
        {
            Vector3 playerpos = Ouros.transform.position;
            playerpos.z = transform.position.z;
            transform.position = playerpos;
        }
    }
}
