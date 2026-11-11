using UnityEngine;

public class solidDetector : MonoBehaviour
{
    public MouseController mc;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!mc.gameObject.GetComponent<PolygonCollider2D>().enabled) mc.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }
}
