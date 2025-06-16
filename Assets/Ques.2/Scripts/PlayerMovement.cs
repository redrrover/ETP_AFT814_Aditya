using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputAction action;
    private InputAction IA_Movement;

    [Header("Movement Related")]

    private float inputValue;
    private Rigidbody2D rb;
    public float moveforce = 3f;

    [Header ("Jump Related")]

    public Vector2 Boxsize = new Vector2(0,0);

    public float castDistance;
    public float jumpForce;
    private Detection detection;
    

    private void IA_Jump(InputAction.CallbackContext context)
    {
        Jump();
    }
    private void IA_Interact(InputAction.CallbackContext context)
    {
        detection.Interaction();
    }

    private void Awake()
    {
        action = new PlayerInputAction();
        rb = GetComponent<Rigidbody2D>();
        detection = GetComponent<Detection>();

        IA_Movement = action.PlayerAction.Movement;
        IA_Movement.Enable();

        action.PlayerAction.Jump.started += IA_Jump;
        action.PlayerAction.Jump.Enable();

        action.PlayerAction.Interact.started += IA_Interact;
        action.PlayerAction.Interact.Enable();
    }
    private void Update()
    {
        inputValue = action.PlayerAction.Movement.ReadValue<float>();
    }
    private void FixedUpdate()
    {
        
        
        //Debug.Log("value" + inputValue);
        
        rb.AddForce(new Vector2(inputValue * moveforce, 0));
        
    }

    private void Jump()
    {
        int mask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Boxsize, 0,  - transform.up, castDistance, mask);
        if(hit.collider != null)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("can't Jump! Mate.");
        }
    }

    public bool IsgoingLeft()
    {
        if (inputValue < 0)
        {
            return true;

        }
        else
        {
            return false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube( transform.position - transform.up * castDistance, Boxsize);
    }
}
