using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float speed = 0.4f;
    public float dirX;
    public SpriteRenderer spr;
    private int jumps;
    private static int limitedJumps = 2;
    
    private Rigidbody2D rigibody;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody2D>();
        jumps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)){            
            if(jumps < limitedJumps){
                animator.SetBool("isJump", true);
                rigibody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                jumps++;
            }
        }
            
        dirX = Input.GetAxis("Horizontal");        
        
        transform.Translate(Vector3.right * dirX * speed);
        if(dirX>0)
        {
            spr.flipX = false;
        }
        if(dirX<0)
        {
            spr.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Flat"){
            animator.SetBool("isJump", false);
            jumps = 0;
        }

        if(collision.gameObject.tag == "Obstacles"){        
            jumps = 0;
        }
        
    }
}
