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
    

    private void Awake()
    {
        action = new PlayerInputAction();
        rb = GetComponent<Rigidbody2D>();

        IA_Movement = action.PlayerAction.Movement;
        IA_Movement.Enable();
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
}
