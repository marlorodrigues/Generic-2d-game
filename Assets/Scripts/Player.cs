using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool isJupimg = false;
    public bool doubleJump = false;
    
    private Rigidbody2D rig;
    private Animator anim;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Speed * Time.deltaTime;
        
        if(Input.GetAxis("Horizontal") != 0f){
            anim.SetBool("walk", true);
            
            if(Input.GetAxis("Horizontal") > 0f)
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            else
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else 
            anim.SetBool("walk", false);
    }

    void Jump(){
        if(Input.GetButtonDown("Jump")){
            if(!isJupimg){
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else if(doubleJump){
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = false;
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 8){
            isJupimg = false;
            anim.SetBool("jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.layer == 8){
            isJupimg = true;
            anim.SetBool("jump", true);
        }
    }
        
}
