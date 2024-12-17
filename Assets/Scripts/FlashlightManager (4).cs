using UnityEngine;

public class DualLightController : MonoBehaviour
{
    [SerializeField] private Material lightMaterial;
    [SerializeField] private Transform circleLight;
    [SerializeField] private Transform coneLight;

    [Header("Circle Light Settings")]
    [SerializeField] private float circleRadius = 2f;

    [Header("Cone Light Settings")]
    [SerializeField] private float coneLength = 4f;
    [SerializeField] private float coneStartWidth = 0.5f;
    [SerializeField] private float coneEndWidth = 2f;

    [Header("Common Settings")]
    [SerializeField] private float softness = 0.1f;

    private void Update()
    {
        // Update circle light
        lightMaterial.SetVector("_CirclePos", circleLight.position);
        lightMaterial.SetFloat("_CircleRadius", circleRadius);

        // Update cone light
        lightMaterial.SetVector("_ConePos", coneLight.position);
        lightMaterial.SetFloat("_ConeRotation", coneLight.rotation.eulerAngles.z * Mathf.Deg2Rad);
        lightMaterial.SetFloat("_ConeLength", coneLength);
        lightMaterial.SetFloat("_ConeStartWidth", coneStartWidth);
        lightMaterial.SetFloat("_ConeEndWidth", coneEndWidth);

        // Update common properties
        lightMaterial.SetFloat("_Softness", softness);
    }
}