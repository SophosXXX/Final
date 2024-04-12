using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarLookAt : MonoBehaviour
{
    private Transform mainCameraTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, mainCameraTransform.eulerAngles.y, 0f);
    }
}
