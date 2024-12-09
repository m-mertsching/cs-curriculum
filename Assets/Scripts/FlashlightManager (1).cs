
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    private Material flashlightMaterial;
    private Flashlight[] flashlights;
    
    private ComputeBuffer lightBuffer;
    private Flashlight.LightData[] lightData;
    
    private void Start()
    {
        lightData = new Flashlight.LightData[flashlights.Length];
        lightBuffer = new ComputeBuffer(flashlights.Length, 32); // 2 Vector4s = 32 bytes
        
        flashlightMaterial.SetBuffer("_LightBuffer", lightBuffer);
        flashlightMaterial.SetInt("_LightCount", flashlights.Length);
    }
    
    private void Update()
    {
        // Update light data from all flashlights
        for (int i = 0; i < flashlights.Length; i++)
        {
            lightData[i] = flashlights[i].GetLightData();
        }
        
        lightBuffer.SetData(lightData);
    }
    
    private void OnDestroy()
    {
        if (lightBuffer != null)
            lightBuffer.Release();
    }
}