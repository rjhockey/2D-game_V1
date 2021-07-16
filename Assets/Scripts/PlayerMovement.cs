using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()   // runs when script is called
    {
        // grab references for rigidbody & animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    private void Update()    // to access player movement, runs every frame of the game
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // store players horizontal axis value

        // to control how fast player is moving
        // Horizontal records left a key as a -1, right d key as a +1
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // make player flip left/right
        if (horizontalInput > .01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Input.GetKey - boolean variable (true or false)
        // KeyCode is an enumeration that contains all buttons ie. KeyCode.space
        // && grounded allows player to only jump when on the ground
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump ()
    {
        body.velocity = new Vector2(body.velocity.x, speed);  // defines what happens when space key is depressed>player jumps
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
