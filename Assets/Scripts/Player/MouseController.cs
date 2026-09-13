using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public Vector3 mousePos;
    private float maxXizq = -9.70f;
    private float maxXder = -4.3f;
    [SerializeField] private float collisionCD = 0.2f;
    public Rigidbody2D rb;
    public PolygonCollider2D pc;
    public bool isBeingHeld = true;
    public bool isOnCollisionCooldown = true;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PolygonCollider2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        pc.enabled = false;
    }
    void Start()
    {
        if(isBeingHeld) UpdateMousePosition();

    }
    private void Update()
    {
        if (isOnCollisionCooldown)
        {
            collisionCD -= Time.deltaTime;
            if(collisionCD <= 0)
            {
                rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                isOnCollisionCooldown = false;
            }
        }
        if (isBeingHeld)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.x = Mathf.Clamp(mousePos.x, maxXizq, maxXder);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,5f); 
            transform.position = new Vector2(mousePos.x,transform.position.y);
        if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.instance.canBeClicked)
        {
            pc.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f); 
            GameManager.instance.dropObject(this);
        }
            
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f); 
        }

    }

    private void UpdateMousePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = spawnPosition(mousePos);
    }

    public Vector2 spawnPosition(Vector3 mousePosition)
    {
        return new Vector2(mousePosition.x,transform.position.y);
    }
}
