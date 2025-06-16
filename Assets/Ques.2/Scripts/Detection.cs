using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Detection : MonoBehaviour
{
    [Header("interaction related")]
    public float castDistance;
    [SerializeField] private Transform RayCastObject;
    private Vector2 rayDirection = Vector2.right;
    private Vector2 defaultlocation;
    private PlayerMovement movement;
    private Rigidbody2D rb;

    public float backForce;

    private void Awake()
    {
        defaultlocation = RayCastObject.localPosition;
        movement = GetComponent<PlayerMovement>();

    }
    void Update()
    {
        if (movement.IsgoingLeft() == true)
        {
            rayDirection = Vector2.left;
            RayCastObject.localPosition = new Vector2(-defaultlocation.x, defaultlocation.y);
        }
        else
        {
            rayDirection = Vector2.right;
            RayCastObject.localPosition = defaultlocation;
        }
    }
    public void Interaction()
    {
        int mask2 = LayerMask.GetMask("object");
        RaycastHit2D hit2 = Physics2D.Raycast(RayCastObject.position, rayDirection, castDistance, mask2);

        if (hit2.collider != null)
        {
            if (rayDirection.x > 0)
            {
                rb.AddForce(new Vector2(-backForce, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(backForce, 0), ForceMode2D.Impulse);
            }
                
        }
        else
        {
            Debug.Log("no interaction");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 start = RayCastObject.position;
        Vector2 end = start + (rayDirection * castDistance);
        Gizmos.DrawLine(start, end);
    }
}
