using UnityEngine;
using System.Collections;

public class magic : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.5f;
    public int direction;
    // Update is called once per frame
    magic(int d)
    {
        direction = d;
    }
    public void setDirection(int d)
    {
        direction = d;
    }

    void Update ()
    {
        if (direction == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        transform.Translate(speed * Time.deltaTime * direction, 0, 0);
    }
}
