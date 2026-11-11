using UnityEngine;

public class Circle : MouseController
{
    [SerializeField] private int circleID = 0;
    public bool yaColisiono = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        MouseController mouse = collision.gameObject.GetComponent<MouseController>();
        if (mouse != null)
        {
            mouse.canTrigger = true;
            this.canTrigger = true;
        }

        if(yaColisiono) return;
        if(collision.gameObject.tag == "circle")
        {
            ContactPoint2D point = collision.GetContact(0);
            Vector2 contacto = point.point;
            Destroy(gameObject);
            Destroy(collision.gameObject);

            if(gameObject.GetEntityId() > collision.gameObject.GetEntityId()){

                GameManager.instance.evolution(circleID,contacto);
            }
            yaColisiono = true;

        }
    }
}
