using UnityEngine;

public class Triangle : MouseController
{
    [SerializeField] private int triangleID = 1;
    public bool yaColisiono = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(yaColisiono) return;
        if(collision.gameObject.tag == "triangle")
        {
            ContactPoint2D point = collision.GetContact(0);
            Vector2 contacto = point.point;
            Destroy(gameObject);
            Destroy(collision.gameObject);

            if(gameObject.GetEntityId() > collision.gameObject.GetEntityId()){

                GameManager.instance.evolution(triangleID,contacto);
            }
            yaColisiono = true;

        }
    }
}
