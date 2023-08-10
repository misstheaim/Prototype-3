using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    private Animator playerAnim;

    public ParticleSystem playerExplosion;
    public float jumpForce = 10;
    public float gravityModifier = 1;
    public bool isOnTheGround = true;

    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnTheGround = false;
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerExplosion.Play();
            gameOver = true;
            Debug.Log("Game over");
        }

    }
}
