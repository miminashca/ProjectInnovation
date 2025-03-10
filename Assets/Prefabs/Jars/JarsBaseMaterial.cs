using UnityEngine;


public class JarsBaseMaterial : MonoBehaviour
{
    public Texture newTexture; // Укажи текстуру в инспекторе
    private Renderer rend;
    private MaterialPropertyBlock mpb;

    void Start()
    {
        rend = GetComponent<Renderer>(); // Получаем рендерер объекта
        mpb = new MaterialPropertyBlock(); 

        // Получаем текущие свойства материала
        rend.GetPropertyBlock(mpb);

        // Устанавливаем новую текстуру (Base Map в Shader Graph / стандартном шейдере URP)
        mpb.SetTexture("_BaseMap", newTexture);  

        // Применяем изменения
        rend.SetPropertyBlock(mpb);
    }
}

