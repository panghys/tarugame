using UnityEngine;

public class Square : MouseController
{
    [SerializeField] private int squareID = 2;
    public bool yaColisiono = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(yaColisiono) return;
        if(collision.gameObject.tag == "square")
        {
            ContactPoint2D point = collision.GetContact(0);
            Vector2 contacto = point.point;
            Destroy(gameObject);
            Destroy(collision.gameObject);

            if(gameObject.GetEntityId() > collision.gameObject.GetEntityId()){

                GameManager.instance.evolution(squareID,contacto);
            }
            yaColisiono = true;

        }
    }
}
