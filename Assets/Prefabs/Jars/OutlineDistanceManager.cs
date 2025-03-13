using System.Collections;
using UnityEngine;

[RequireComponent(typeof(OutlineController))]
public class OutlineDistanceManager : MonoBehaviour
{
    [Header("���������")]
    [Tooltip("������������ ����������, ������ �������� ������� ����� (���� ������ ��������� ����� � ������� �����, ����� � ���).")]
    public float maxDistance = 3f;

    private OutlineController outlineController;
    // ����������� ������ (�����) � ����� "Thief"
    private Transform thiefTransform;

    void Start()
    {
        outlineController = GetComponent<OutlineController>();
        if (outlineController.outlineObject == null)
        {
            Debug.LogError("OutlineDistanceManager: �������� ������ ������� �� ������!");
        }
        // ��������� ����� ������� � ����� "Thief"
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
                Debug.Log("OutlineDistanceManager: ������ ������ � ����� 'Thief': " + foundThief.name);
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
        //Debug.Log("OutlineDistanceManager: ���������� �� Thief = " + distance.ToString("F2"));

        // ���� ������ ��������� � �������� maxDistance � �������� �������� ������ � ���������, ����� � ���������
        if (distance <= maxDistance)
        {
            if (!outlineController.outlineObject.activeSelf)
            {
                outlineController.outlineObject.SetActive(true);
                Debug.Log("OutlineDistanceManager: �������� �������");
            }
        }
        else
        {
            if (outlineController.outlineObject.activeSelf)
            {
                outlineController.outlineObject.SetActive(false);
                Debug.Log("OutlineDistanceManager: ��������� �������");
            }
        }
    }
}
