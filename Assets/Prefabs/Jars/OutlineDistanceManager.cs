using System.Collections;
using UnityEngine;

[RequireComponent(typeof(OutlineController))]
public class OutlineDistanceManager : MonoBehaviour
{
    public float maxDistance = 3f;

    private OutlineController outlineController;
    private Transform thiefTransform;

    void Start()
    {
        outlineController = GetComponent<OutlineController>();
        if (outlineController.outlineObject == null)
        {
        }
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

        if (distance <= maxDistance)
        {
            if (!outlineController.outlineObject.activeSelf)
            {
                outlineController.outlineObject.SetActive(true);
            }
        }
        else
        {
            if (outlineController.outlineObject.activeSelf)
            {
                outlineController.outlineObject.SetActive(false);
            }
        }
    }
}
