using UnityEngine;

[ExecuteAlways]
public class ResolutionAdapter : MonoBehaviour
{
    [Header("Aspect Ratio del diseño (Ej: 16 y 9 para 16:9)")]
    public float targetAspectWidth = 16f;
    public float targetAspectHeight = 9f;

    private Camera cam;
    private float initialOrthographicSize;

    void Awake()
    {
        cam = GetComponent<Camera>();
        initialOrthographicSize = cam.orthographicSize;
    }

    void Update()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        if (cam == null) return;

        float targetAspect = targetAspectWidth / targetAspectHeight;
        float currentAspect = (float)Screen.width / Screen.height;
        if (currentAspect < targetAspect)
        {
            cam.orthographicSize = initialOrthographicSize * (targetAspect / currentAspect);
        }
        else
        {
            cam.orthographicSize = initialOrthographicSize;
        }
    }
}