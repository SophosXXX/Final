using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    void Update()
    {
        // Check if the 'Z' key is pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Get all buttons in the scene
            Button[] buttons = FindObjectsOfType<Button>();

            // Iterate through each button
            foreach (Button button in buttons)
            {
                // Check if the pointer is over the button (i.e., highlighted)
                if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == button.gameObject)
                {
                    // Invoke the onClick function of the highlighted button
                    button.onClick.Invoke();
                }
            }
        }
    }
}
