using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraTransformsData", menuName = "Scriptable Objects/CameraTransformsData")]
public class CameraTransformsData : ScriptableObject
{
    public List<Transform> cameraTransforms;
}
