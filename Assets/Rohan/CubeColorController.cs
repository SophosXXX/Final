using UnityEngine;

public class CubeColorController : MonoBehaviour {
    public LightTemperature lightTemperature; // Reference to the LightTemperature component of the light source
    private MeshRenderer cubeRenderer; // Reference to the MeshRenderer component of the CubeLight object

    private void Start() {
        cubeRenderer = GetComponent<MeshRenderer>(); // Get the MeshRenderer component
    }

    private void Update() {
        if(lightTemperature && cubeRenderer) {
            Color lightColor = LightTemperature.ColorFromTemperature(lightTemperature.temperature);
            cubeRenderer.material.color = lightColor;
            cubeRenderer.material.SetColor("_EmissionColor", lightColor);
        }
    }
}