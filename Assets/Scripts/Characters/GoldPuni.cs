using UnityEngine;

public class GoldPuni : MouseController
{
    [SerializeField] private int gpuniID = 9;
    public bool yaColisiono = false;
    /* que se quede hasta aca
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(yaColisiono) return;
        if(collision.gameObject.tag == "gpuni")
        {
            ContactPoint2D point = collision.GetContact(0);
            Vector2 contacto = point.point;
            Destroy(gameObject);
            Destroy(collision.gameObject);

            if(gameObject.GetEntityId() > collision.gameObject.GetEntityId()){

                GameManager.instance.evolution(gpuniID,contacto);
            }
            yaColisiono = true;

        }
    }
    */
}
