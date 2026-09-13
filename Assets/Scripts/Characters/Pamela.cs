using UnityEngine;

public class Pamela : MouseController
{
    [SerializeField] private int pamelaID = 7;
    public bool yaColisiono = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(yaColisiono) return;
        if(collision.gameObject.tag == "pamela")
        {
            ContactPoint2D point = collision.GetContact(0);
            Vector2 contacto = point.point;
            Destroy(gameObject);
            Destroy(collision.gameObject);

            if(gameObject.GetEntityId() > collision.gameObject.GetEntityId()){

                GameManager.instance.evolution(pamelaID,contacto);
            }
            yaColisiono = true;

        }
    }
}
