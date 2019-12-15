using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVHScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool left;
    public float speed;
    private Vector3 originalpos;
    public Vector3 destination;
    private Rigidbody2D rb;
    private int a;

    void Start()
    {
        originalpos = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(left)
        {
            if(transform.position.x <= destination.x)
            {
                a = 1;
            }
            else if(transform.position.x >= originalpos.x)
            {
                a = -1;
            }

        }
        else
        {
            if (transform.position.x >= destination.x)
            {
                a = -1;
            }
            else if (transform.position.x <= originalpos.x)
            {
                a = 1;
            }
        }
        rb.velocity = new Vector2(a * speed, rb.velocity.y);
    }
}
