using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUp : MonoBehaviour
{
    public bool pickedUp = false;
    public Vector3 pivot = new Vector3(0f, 0f, 0f);

    public float force = 1000f;

    public GameObject Ray;
    // public GameObject Hand;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUp)
        {
            //transform.position = Ray.GetComponent<RayCastPointer>().rayPivot;

            //GetComponent<BoxCollider>().enabled = false;

            Freeze();

            if(Input.GetButton("js5"))
            {
                transform.SetParent(null);
                rb.useGravity = true;

                rb.AddForce(Ray.transform.forward * force);
                pickedUp = false;

                // GetComponent<BoxCollider>().enabled = true;
                if (gameObject.name.Contains("dart"))
                {
                    // Unfreeze position along all axes
                    rb.constraints &= ~RigidbodyConstraints.FreezePosition;

                    // Unfreeze the position along the Y axis
                    rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
                    rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                }
                else
                {
                    Unfreeze();
                }
            }
        }

    }

    void Unfreeze()
    {
        // Unfreeze position along all axes
        rb.constraints &= ~RigidbodyConstraints.FreezePosition;

        // Unfreeze rotation along all axes
        rb.constraints &= ~RigidbodyConstraints.FreezeRotation;
    }

    void Freeze()
    {
        // Freeze position along all axes
        rb.constraints = RigidbodyConstraints.FreezePosition;

        // Freeze rotation along all axes
        rb.constraints |= RigidbodyConstraints.FreezeRotation;
    }

}
