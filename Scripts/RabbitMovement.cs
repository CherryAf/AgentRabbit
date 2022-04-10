using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector]
    public bool isHidden;
    private State state;
    SpriteRenderer spriteRenderer;
    private float speed = 175.0f;
    public GameObject ceiling;
    private float oob;
    
    private enum State
    {
        Normal,
        Hiding,
    }
    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        oob = ceiling.transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                break;
            case State.Hiding:
                break;

        }
        //move left
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !isHidden)
            spriteRenderer.flipX = true;
            this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 3f, 0, 0);

        //move right
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !isHidden)
            spriteRenderer.flipX = false;
            this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 3f, 0, 0);

        //jump
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !isHidden)
            //rb.AddForce(new Vector2(0, 300f));
            if (gameObject.transform.position.y <= oob)
            {
                rb.AddForce(Vector2.up * speed);
            }
        //hide
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(state == State.Hiding)
            {
                HideRabbit();
            }
        }


    }
    void HideRabbit() {  
        if (!isHidden)
        {
            GetComponent<Renderer>().enabled = false;
            isHidden = true;
            AkSoundEngine.PostEvent("HideLP", null);
        }
        else {
            GetComponent<Renderer>().enabled = true;
            isHidden = false;
            AkSoundEngine.PostEvent("ResetLP", null);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            state = State.Hiding;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            state = State.Normal;
        }
        if (other.gameObject.layer == 10)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
    private void OnCollisionEnter2D(Collision2D other) { 
  
        if(other.gameObject.layer == 7)
        {
            Debug.Log("obstacle collided");
            
        }
    }
}
