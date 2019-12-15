using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private float maxspeedx;
    private float yspeed;
    private float speed;
    private Animator anim;
    private Rigidbody2D rb;
    private bool facingright;
    private bool onGround;
    private bool onmvh;
    private bool onmvv;
    private Vector3 mvhv;
    private Vector3 mvvv;
    public GameObject lcontrol;
    void Start()
    {
        maxspeedx = 5;
        yspeed = 0;
        facingright = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        onGround = false;
        lcontrol = GameObject.Find("Level");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(onGround);
        yspeed = rb.velocity.y;
        float h = Input.GetAxis("Horizontal");
        //Debug.Log(rb.velocity.y);
        if (Input.GetAxisRaw("Vertical") > 0 && onGround == true)
        {
            jump();
            anim.SetFloat("speedy", yspeed);
        }
        else
        {
            anim.SetFloat("speedy", rb.velocity.y);
        }
        //Debug.Log(yspeed);
        if(h != 0)
        {
            anim.SetFloat("Speed", Mathf.Abs(maxspeedx));
        }
        else
        {
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
        if(onmvh)
        {
            rb.velocity = new Vector2(h * maxspeedx + mvhv.x, yspeed);
        }
        else if(onmvv)
        {
            rb.velocity = new Vector2(h * maxspeedx, yspeed + mvvv.y);
        }
        else
        {
            rb.velocity = new Vector2(h * maxspeedx, yspeed);
        }
        
        
        if (h > 0 && !facingright)
            reverseImage();
        else if (h < 0 && facingright)
            reverseImage();
    }

    void reverseImage()
    {
        // Switch the value of the Boolean
        facingright = !facingright;

        // Get and store the local scale of the RigidBody2D
        Vector2 theScale = rb.transform.localScale;

        // Flip it around the other way
        theScale.x *= -1;
        rb.transform.localScale = theScale;
    }
    void jump()
    {
        yspeed = 10;
        onGround = false;
        anim.SetBool("jumping", true);
    }
    void OnCollisionEnter2D(Collision2D other )
    {
        //Debug.Log(other.contacts[0].normal);
        if (other.gameObject.CompareTag("Floor"))
        {
            var normal = other.contacts[0].normal;
            if (normal.y > 0)
            { //if the bottom side hit something 
                //Debug.Log("You Hit the floor");
                onGround = true;
                anim.SetBool("jumping", false);

            }
            else
            {
                onGround = false;
            }
        }
        else if(other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            lcontrol.GetComponent<LevelController>().takeCoin();
        }
        else if(other.gameObject.CompareTag("Spike"))
        {
            lcontrol.GetComponent<LevelController>().ResetLevel();
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            lcontrol.GetComponent<LevelController>().ResetLevel();
        }
        else if (other.gameObject.CompareTag("MVH"))
        {
            var normal = other.contacts[0].normal;
            if (normal.y > 0)
            { //if the bottom side hit something 
                //Debug.Log("You Hit the floor");
                onGround = true;
                anim.SetBool("jumping", false);
                //Debug.Log("Move");
                mvhv = other.gameObject.GetComponent<Rigidbody2D>().velocity;
                onmvh = true;
            }
            else
            {
                onGround = false;
            }
        }
        else if (other.gameObject.CompareTag("MVV"))
        {
            var normal = other.contacts[0].normal;
            if (normal.y > 0)
            { //if the bottom side hit something 
                //Debug.Log("You Hit the floor");
                onGround = true;
                anim.SetBool("jumping", false);
                mvvv = other.gameObject.GetComponent<Rigidbody2D>().velocity;
                onmvv = true;

            }
            else
            {
                onGround = false;
            }
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            var normal = other.contacts[0].normal;
            if (normal.y > 0)
            { //if the bottom side hit something 
                //Debug.Log("You Hit the floor");
                onGround = true;
                anim.SetBool("jumping", false);

            }
            if (normal.y < 0)
            { //if the top side hit something
                //Debug.Log("You Hit the roof");
                onGround = false;
            }
        }
        else if (other.gameObject.CompareTag("MVH"))
        {
            //Debug.Log("Move");
            
            var normal = other.contacts[0].normal;
            //Debug.Log(normal.y);
            if (normal.y > 0)
            { //if the bottom side hit something 
                //Debug.Log("You Hit the floor");
                onGround = true;
                anim.SetBool("jumping", false);
                mvhv = other.gameObject.GetComponent<Rigidbody2D>().velocity;
                onmvh = true;
            }
            else
            {
                onGround = false;
            }
        }
        else if (other.gameObject.CompareTag("MVV"))
        {
            var normal = other.contacts[0].normal;
            if (normal.y > 0)
            { //if the bottom side hit something 
                //Debug.Log("You Hit the floor");
                onGround = true;
                anim.SetBool("jumping", false);
                mvvv = other.gameObject.GetComponent<Rigidbody2D>().velocity;
                onmvv = true;

            }
            else
            {
                onGround = false;
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            onGround = false;
        }
        else if(other.gameObject.CompareTag("MVH"))
        {
            onmvh = false;
            onGround = false;
        }
        else
        {
            onmvv = false;
            onGround = false;
        }
    }
}
