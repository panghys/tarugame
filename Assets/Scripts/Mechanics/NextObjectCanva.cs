using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextObjectCanva : MonoBehaviour
{
    private Image image;
    [SerializeField] private List<Sprite> sprites;
    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.sprite = sprites[GameManager.instance.nextOne];
    }


}
