using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Movement
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Flip
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Jump
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // Animations
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        if (SoundManager.instance != null && jumpSound != null)
            SoundManager.instance.PlaySound(jumpSound);

        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        grounded = false;
        anim.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && grounded; 
    }
}