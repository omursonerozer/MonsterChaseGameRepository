using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 20f;
    private float movementX;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        myBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    void LastUpdate()
    {
        if(myBody.transform.position.x > 56f || myBody.transform.position.x < -57f){
            Destroy(gameObject);
        }
    }

    void PlayerMovementKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {   
        if(movementX > 0){ // walking to the right
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if(movementX < 0){ // walking to the left
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else{
            anim.SetBool(WALK_ANIMATION, false);
        }
        
    }

    void PlayerJump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded){
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(GROUND_TAG)){
            isGrounded = true;
        }

        if(collision.gameObject.CompareTag(ENEMY_TAG)){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(ENEMY_TAG)){
            Destroy(gameObject);
        }
    }
}
