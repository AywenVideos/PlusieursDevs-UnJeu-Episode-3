using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public float speed;
    private float Move;
    public float Jump;
    private bool isJumping;

    private Rigidbody2D Rb;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");
        Rb.velocity = new Vector2(Move * speed, Rb.velocity.y);

        if (Input.GetButtonDown("Jump") && !isJumping){
            Rb.AddForce(new Vector2(Rb.velocity.x, Jump));
        }
    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor")){
            isJumping = true;
        }
    }
}
