using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class OutlineController : MonoBehaviour
{
    [Tooltip("Материал с шейдером, который смещает вершины по нормали (OutlineExtrude)")]
    public Material outlineMaterial;

    [Tooltip("Начальное значение толщины обводки. Это значение будет управляться извне.")]
    public float outlineWidth = 0.03f;

    [HideInInspector]
    public GameObject outlineObject; // Дочерний объект для обводки

    void Start()
    {
        if (outlineMaterial != null)
        {
            // Создаем уникальную копию материала для этого объекта
            outlineMaterial = Instantiate(outlineMaterial);
        }
        else
        {
            Debug.LogError("OutlineController: outlineMaterial не задан!");
        }
        
        CreateOutlineObject();
    }

    void CreateOutlineObject()
    {
        // Создаем новый дочерний объект для outline
        outlineObject = new GameObject(gameObject.name + "_Outline");
        outlineObject.transform.SetParent(transform, false);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.localRotation = Quaternion.identity;
        outlineObject.transform.localScale = Vector3.one;

        // Устанавливаем слой дочернего объекта равным слою родительского
        SetLayerRecursively(outlineObject, gameObject.layer);

        // Копируем MeshFilter: берем тот же меш, что и у оригинала
        MeshFilter originalMF = GetComponent<MeshFilter>();
        MeshFilter outlineMF = outlineObject.AddComponent<MeshFilter>();
        outlineMF.sharedMesh = originalMF.sharedMesh;

        // Добавляем MeshRenderer и назначаем ему созданную копию материала
        MeshRenderer outlineMR = outlineObject.AddComponent<MeshRenderer>();
        outlineMR.material = outlineMaterial;

        Debug.Log("OutlineController: Создан outline объект с слоем " + LayerMask.LayerToName(gameObject.layer));
    }

    // Рекурсивно устанавливаем слой для объекта и всех его дочерних объектов
    public static void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            if (child != null)
                SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    void Update()
    {
        // Обновляем параметр _OutlineWidth каждый кадр,
        // чтобы изменения, внесенные в инспекторе, применялись в режиме Play
        if (outlineMaterial != null)
        {
            outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);
        }
    }
}
