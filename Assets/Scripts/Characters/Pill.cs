using UnityEngine;

public class Pill : MouseController
{
    [SerializeField] private int pillID = 3;
    public bool yaColisiono = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(yaColisiono) return;
        if(collision.gameObject.tag == "pill")
        {
            ContactPoint2D point = collision.GetContact(0);
            Vector2 contacto = point.point;
            Destroy(gameObject);
            Destroy(collision.gameObject);

            if(gameObject.GetEntityId() > collision.gameObject.GetEntityId()){

                GameManager.instance.evolution(pillID,contacto);
            }
            yaColisiono = true;

        }
    }
}
