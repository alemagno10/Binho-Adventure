using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D body;
    public float speed;

    public bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        speed = 7;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        
        if(Input.GetKey(KeyCode.Space) & isGrounded){
            body.velocity = new Vector2(body.velocity.x, speed);
            isGrounded = false;
        }
    }

    // https://gamedev.stackexchange.com/questions/181291/how-can-i-tell-if-a-gameobject-is-currently-touching-another-gameobject-with-a-s
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }

    }
}
