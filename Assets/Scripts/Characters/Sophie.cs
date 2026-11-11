using UnityEngine;

public class Sophie : MouseController
{
    [SerializeField] private int sophieID = 8;
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
        if(collision.gameObject.tag == "sophie")
        {
            ContactPoint2D point = collision.GetContact(0);
            Vector2 contacto = point.point;
            Destroy(gameObject);
            Destroy(collision.gameObject);

            if(gameObject.GetEntityId() > collision.gameObject.GetEntityId()){

                GameManager.instance.evolution(sophieID,contacto);
            }
            yaColisiono = true;

        }
    }
}
