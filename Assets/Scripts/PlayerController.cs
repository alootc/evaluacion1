using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool canDoubleJump;


    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float distance;

    PlayerInput playerInput;
    InputAction moveAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        moveInput = new Vector3(x * speed, rb.velocity.y);

    

        if (Input.GetKeyDown(KeyCode.Space))
        {
                if (canDoubleJump)
                {
                   Jump();
                   canDoubleJump = false;
                }
        }

        MovePlayer();
        Jump();
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput;
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0 ,direction.y) * Time.deltaTime;

    }
   
}
