using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public Light directionalLight;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        directionalLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        directionalLight.transform.position = cameraTransform.position;
        directionalLight.transform.rotation = cameraTransform.rotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Light position: " + directionalLight.transform.position);
            Debug.Log("Light point to: " + directionalLight.transform.forward);
        }
    }
}
