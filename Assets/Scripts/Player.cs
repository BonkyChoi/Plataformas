using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementForce;
    [SerializeField] private float jumpForce;
    private float hInput;
    private Animator anim;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        FaceMovement();
        //En funcion de mi input
        anim.SetFloat("xSpeed", Mathf.Abs(hInput));
        //La vertical se actualiza en funcion de mi velocidad en Y
        anim.SetFloat("ySpeed", rb.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FaceMovement()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        if (hInput < 0 && transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            //transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (hInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            //transform.localScale = Vector3.one;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(hInput, 0) * movementForce, ForceMode2D.Force);
    }
}
