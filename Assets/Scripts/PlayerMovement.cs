using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;

    private void Awake()   // runs when script is called
    {
        body = GetComponent<Rigidbody2D>();     // getComponet accesses ridgidbody2d      
    }

    
    private void Update()    // to access player movement, runs every frame of the game
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // store players horizontal axis vaule

        // to control how fast player is moving
        // Horizontal records left a key as a -1, right d key as a +1
        body.velocity = new Vector2(horizontalInput, body.velocity.y);

        // make player flip left/right
        if (horizontalInput > .01f)   
            transform.localScale = Vector3.one;
        else if (horizontalInput < -.01f)
            transform.localScale = new Vector3(-1,1,1);

        // Input.GetKey - boolean variable (true or false)
        // KeyCode is an enumeration that contains all buttons ie. KeyCode.space
        if (Input.GetKey(KeyCode.Space))
            body.velocity = new Vector2(body.velocity.x, speed);  // defines what happens when space key is depressed>player jumps
        }
}
