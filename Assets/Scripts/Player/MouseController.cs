using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public Vector3 mousePos;
    private float maxXizq = -9.70f;
    private float maxXder = -5.3f;
    [SerializeField] private float collisionCD = 0.2f;
    [SerializeField] private float triggerCD = 10f;
    public Rigidbody2D rb;
    public PolygonCollider2D pc;
    public bool isBeingHeld = true;
    public bool isOnCollisionCooldown = true;
    public bool isOnTriggerCooldown = false;
    public bool canTrigger = true;
    public bool hasBeenPlayed = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PolygonCollider2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        pc.enabled = false;
        if(this.gameObject.tag == "circle" || this.gameObject.tag == "triangle") canTrigger = false;
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
                if(this.gameObject.tag != "circle") rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                isOnCollisionCooldown = false;
            }
        }
        /*if (isOnTriggerCooldown)
        {
            triggerCD -= Time.deltaTime;
            if(triggerCD <= 0)
            {
                pc.enabled = true;
                isOnTriggerCooldown = false;
            }
        }*/
        if (isBeingHeld && GameManager.instance.isRunning)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.x = Mathf.Clamp(mousePos.x, maxXizq, maxXder);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,5f); 
            transform.position = new Vector2(mousePos.x,transform.position.y);
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame && GameManager.instance.canBeClicked)
        {
            //isOnTriggerCooldown = true;
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
