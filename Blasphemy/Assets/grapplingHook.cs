using UnityEngine;
using System.Collections;

public class grapplingHook : MonoBehaviour
{
    public LineRenderer line;
    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float distance = 10f;
    public LayerMask mask;
    public float step = 0.02f;
    private Ouros Ouros;
    public float grabLength;

    // Use this for initialization

    void Start ()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
        Ouros = FindObjectOfType<Ouros>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (joint.distance > .5f)
            joint.distance -= step;
        else
        {
            line.enabled = false;
            joint.enabled = false;
        }

	    if(Input.GetButtonDown("Special"))
        {           
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                targetPos.y = transform.position.y + grabLength;
            } else
            {
                targetPos.y = 0;
            }
            if (Ouros.GetComponent<SpriteRenderer>().flipX == false)
            {
                targetPos.x = transform.position.x + grabLength;
            }
            else
            {
                targetPos.x = transform.position.x - grabLength;
            }
            targetPos.z = 0;

            hit = Physics2D.Raycast(transform.position, targetPos-transform.position, distance, mask);

            if(hit.collider!=null && hit.collider.gameObject.GetComponent<Rigidbody2D>()!=null)
            {
                joint.enabled = true;
                //joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                //joint.distance = Vector2.Distance(transform.position, hit.point);

                //line.enabled = true;
                //line.SetPosition(0, transform.position);
                //line.SetPosition(1, hit.point);
                Vector2 connectPoint = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                connectPoint.x = connectPoint.x / hit.collider.transform.localScale.x;
                connectPoint.y = connectPoint.y / hit.collider.transform.localScale.y;
                Debug.Log(connectPoint);
                joint.connectedAnchor = connectPoint;

                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                //		joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x,hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(transform.position, hit.point);

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
            }
        }

        line.SetPosition(1, joint.connectedBody.transform.TransformPoint(joint.connectedAnchor));

        if(Input.GetButton("Special"))

        {
            line.SetPosition(0, transform.position);
        }

        if(Input.GetButtonUp("Special"))
        {
            joint.enabled = false;
            line.enabled = false;
            this.enabled = false;
        }
	}
}

