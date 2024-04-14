using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity {

    public Rigidbody2D body;
    public float speed;
    public float jumpForce;
    public Vector2 jump;
    public bool isGrounded = true;

    void Start(){
        speed = 7;
        jumpForce = 7;
        jump = new Vector2(0.0f, 2.0f);
    }

    void Update(){
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) && isGrounded){
            body.AddForce(jump * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

     // Detectar colisão com o chão
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;  // Marca como no chão
        }
    }

    // Detectar saída de colisão com o chão
    void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = false;  // Marca como não no chão
        }
    }

    public override void TakeDamage(int damage){
        base.TakeDamage(damage);
    }

    public override void handleDeath(){
        gameObject.SetActive(false);
    }

}
