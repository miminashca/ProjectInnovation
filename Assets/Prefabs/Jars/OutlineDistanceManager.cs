using System.Collections;
using UnityEngine;

[RequireComponent(typeof(OutlineController))]
public class OutlineDistanceManager : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Максимальное расстояние, внутри которого обводка видна (если объект находится ближе — обводка видна, иначе — нет).")]
    public float maxDistance = 3f;

    private OutlineController outlineController;
    // Референсный объект (игрок) с тегом "Thief"
    private Transform thiefTransform;

    void Start()
    {
        outlineController = GetComponent<OutlineController>();
        if (outlineController.outlineObject == null)
        {
            Debug.LogError("OutlineDistanceManager: Дочерний объект обводки не найден!");
        }
        // Запускаем поиск объекта с тегом "Thief"
        StartCoroutine(FindThiefCoroutine());
    }

    IEnumerator FindThiefCoroutine()
    {
        while (thiefTransform == null)
        {
            GameObject foundThief = GameObject.FindWithTag("Thief");
            if (foundThief != null)
            {
                thiefTransform = foundThief.transform;
                Debug.Log("OutlineDistanceManager: Найден объект с тегом 'Thief': " + foundThief.name);
                break;
            }
            yield return null;
        }
    }

    void Update()
    {
        if (outlineController == null || thiefTransform == null)
            return;

        float distance = Vector3.Distance(transform.position, thiefTransform.position);
        Debug.Log("OutlineDistanceManager: Расстояние до Thief = " + distance.ToString("F2"));

        // Если объект находится в пределах maxDistance – включаем дочерний объект с оутлайном, иначе – отключаем
        if (distance <= maxDistance)
        {
            if (!outlineController.outlineObject.activeSelf)
            {
                outlineController.outlineObject.SetActive(true);
                Debug.Log("OutlineDistanceManager: Включена обводка");
            }
        }
        else
        {
            if (outlineController.outlineObject.activeSelf)
            {
                outlineController.outlineObject.SetActive(false);
                Debug.Log("OutlineDistanceManager: Выключена обводка");
            }
        }
    }
}
