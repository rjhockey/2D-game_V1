using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpPower;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    // private bool grounded; removed
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;

    // runs when script is called
    private void Awake()   
    {
        // grab references for rigidbody & animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // to access player movement, runs every frame of the game
    private void Update()    
    {
        // store players horizontal axis value
        horizontalInput = Input.GetAxis("Horizontal"); 



        // make player flip left/right
        if (horizontalInput > .01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Input.GetKey - boolean variable (true or false)
        // KeyCode is an enumeration that contains all buttons ie. KeyCode.space
        // && grounded allows player to only jump when on the ground


        // set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // wall jump logic
        if (wallJumpCoolDown > 0.2f)
        {
 

            // to control how fast player is moving
            // Horizontal records left a key as a -1, right d key as a +1
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }

            else
                body.gravityScale = 3;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }

        else
            wallJumpCoolDown += Time.deltaTime;
    }

    private void Jump ()
    {
        if (isGrounded())
        {
            // defines what happens when space key is depressed>player jumps
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCoolDown = 0;
            
        }

        
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
