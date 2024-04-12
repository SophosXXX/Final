using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RayCastPointer : MonoBehaviour
{
    public float maxRayDistance = 10f; // Maximum length of the ray trace line

    private LineRenderer lineRenderer;
    public GameObject Character;
    public GameObject Dart;
    private GameObject newDart;
    private DartScoreSystem dartScoreSystem;
    public HealthManager healthManager; // Reference to the HealthManager script
    public HydrationManager hydrationManager; // Reference to the HealthManager script

    public Vector3 rayPivot = new Vector3(0f, 0f, 0f);

    private CharacterMovement charMovement;
    private GameObject lastHitObject; // Store the last object hit by the ray

    void Start()
    {
        // Ensure we have LineRenderer component attached
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set up initial line renderer properties
        lineRenderer.positionCount = 2; // Two points for a straight line
        lineRenderer.startWidth = 0.2f; // Adjust as necessary
        lineRenderer.endWidth = 0.02f; // Adjust as necessary
        lineRenderer.material.color = Color.white; // Set the color to white

        // Remove the shadow from the line renderer material
        lineRenderer.receiveShadows = false;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        dartScoreSystem = GameObject.Find("DartScoreSystem").GetComponent<DartScoreSystem>();
    }

    void Update()
    {
        // Get the character's position and forward direction
        Vector3 characterPosition = Character.transform.position;
        Vector3 characterForward = Character.transform.forward;

        // Calculate the end position of the ray
        Vector3 rayEnd = characterPosition + characterForward * maxRayDistance;

        lineRenderer.SetPosition(0, characterPosition);
        lineRenderer.SetPosition(1, rayEnd);

        // Perform the raycast from the character's position and forward direction
        RaycastHit hit;
        if (Physics.Raycast(characterPosition, characterForward, out hit, maxRayDistance))
        {
            rayPivot = rayEnd;

            Debug.Log(rayPivot);
            // If the ray hits something, set the end position of the line to the point of intersection
            lineRenderer.SetPosition(0, characterPosition);
            lineRenderer.SetPosition(1, hit.point);

            // Detect which object the end of the line touches
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Hit Object: " + hitObject.name);

            if (hitObject.GetComponent<Graphic>() != null)
            {
                Debug.Log("UI Element Hit: " + hitObject.name);
                // You can perform UI-specific actions here
            }

            // Enable the Outline component if it exists
            Outline outline = hitObject.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
            }

            // Disable Outlines for the last hit object if it's different from the current hit object
            if(hitObject.tag != "Outline Objects")
            {
                DisableAllOutlines();
                
            }

            // Store the current hit object
            lastHitObject = hitObject;

            if (hitObject.name == "Fridge" && Input.GetButtonDown("js2"))
            {
                // Reset the health to 100
                healthManager.ResetHealth();
                DisableAllOutlines();
            }
            if (hitObject.name == "Coffee" && Input.GetButtonDown("js2"))
            {
                // Reset the health to 100
                hydrationManager.ResetHealth();
                DisableAllOutlines();
            }
            if (Input.GetButtonDown("js5"))
            {
                // Check if the hit object has a Button component
                Button button = hitObject.GetComponent<Button>();
                
                // If it does, invoke the onClick event
                if (button != null)
                {
                    button.onClick.Invoke();
                }
                else
                {
                    Debug.LogWarning("The hit object does not have a Button component attached.");
                }
            }

            if(hitObject.tag == "Outline Objects" && Input.GetButtonDown("js2"))
            {
                if(hitObject.GetComponent<PickedUp>() != null && !hitObject.name.Contains("dart"))
                {
                    if(!hitObject.GetComponent<PickedUp>().pickedUp)
                    {
                        hitObject.transform.SetParent(null); // Remove parent
                        hitObject.GetComponent<Rigidbody>().useGravity = true;
                    }

                    hitObject.GetComponent<PickedUp>().pickedUp = false;
                }
            }

            // Dart Spawn in Center of Screen
            if(hitObject.name == "Dart_Table" && Input.GetButtonDown("js2"))
            {
                if(dartScoreSystem.currentDarts > 0)
                {
                    // Spawn dart at the center of the screen
                    Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                    Ray centerRay = Camera.main.ScreenPointToRay(centerOfScreen);
                    if (Physics.Raycast(centerRay, out hit))
                    {
                        newDart = Instantiate(Dart, hit.point, Quaternion.identity);
                    }
                }
            }


            // Reset Dart Game
            if(hitObject.name == "ResetGame" && Input.GetButtonDown("js5"))
            {
                hitObject.GetComponent<Button>().onClick.Invoke();
            }
        }
        else
        {
            // If the ray doesn't hit anything, disable the outline for the last hit object
            DisableAllOutlines();
        }

    }

    // Method to disable outline for the last hit object
    public void DisableAllOutlines()
    {
        // Find all game objects with the tag "OutlineObjects"
        GameObject[] outlineObjects = GameObject.FindGameObjectsWithTag("Outline Objects");

        // Loop through each object found
        foreach (GameObject obj in outlineObjects)
        {
            // Check if the object has an "Outline" component
            Outline outlineComponent = obj.GetComponent<Outline>();
            if (outlineComponent != null)
            {
                // Disable the "Outline" component
                outlineComponent.enabled = false;
            }
        }
    }
}
