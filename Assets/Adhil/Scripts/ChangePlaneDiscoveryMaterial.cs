using System.Collections;
using System.Collections.Generic;
using GoogleARCore.Examples.Common;
using UnityEngine;

public class ChangePlaneDiscoveryMaterial : MonoBehaviour
{
    public GameObject planeGenerator;
    public Material transparentPlaneMaterial;
    public Material opaquePlaneMaterial;
    bool isTouched = false;
    PlaneVisibilityMode planeVisibilityMode =PlaneVisibilityMode.Opaque;

    private void Start()
    {
        opaquePlaneMaterial.SetColor("_GridColor", Color.white);
        opaquePlaneMaterial.SetFloat("_UvRotation", Random.Range(0.0f, 360.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTouched)
        {
            if(Input.touchCount == 2||Input.GetKeyDown(KeyCode.K))
            {
                isTouched = true;
                if (planeVisibilityMode == PlaneVisibilityMode.Opaque)
                {
                    for (int i = 0; i < planeGenerator.transform.childCount; i++)
                    {
                        planeGenerator.transform.GetChild(i).GetComponent<Renderer>().material = transparentPlaneMaterial;
                    }
                    planeVisibilityMode = PlaneVisibilityMode.Transparent;
                    planeGenerator.GetComponent<DetectedPlaneGenerator>().DetectedPlanePrefab.GetComponent<Renderer>().material = transparentPlaneMaterial;
                    Debug.Log("Transparent Mode");
                }
                else
                {
                    for (int i = 0; i < planeGenerator.transform.childCount; i++)
                    {
                        planeGenerator.transform.GetChild(i).GetComponent<Renderer>().material = opaquePlaneMaterial;
                    }
                    planeVisibilityMode = PlaneVisibilityMode.Opaque;
                    planeGenerator.GetComponent<DetectedPlaneGenerator>().DetectedPlanePrefab.GetComponent<Renderer>().material = opaquePlaneMaterial;
                    Debug.Log("Opaque Mode");
                }
                StartCoroutine(ReEnableTouch());
            }
        }
    }

    public void ChangeMaterial()
    {
        if (planeVisibilityMode == PlaneVisibilityMode.Opaque)
        {
            for (int i = 0; i < planeGenerator.transform.childCount; i++)
            {
                planeGenerator.transform.GetChild(i).GetComponent<Renderer>().material = transparentPlaneMaterial;
            }
            planeVisibilityMode = PlaneVisibilityMode.Transparent;
            planeGenerator.GetComponent<DetectedPlaneGenerator>().DetectedPlanePrefab.GetComponent<Renderer>().material = transparentPlaneMaterial;
            Debug.Log("Transparent Mode");
        }
        else
        {
            for (int i = 0; i < planeGenerator.transform.childCount; i++)
            {
                planeGenerator.transform.GetChild(i).GetComponent<Renderer>().material = opaquePlaneMaterial;
            }
            planeVisibilityMode = PlaneVisibilityMode.Opaque;
            planeGenerator.GetComponent<DetectedPlaneGenerator>().DetectedPlanePrefab.GetComponent<Renderer>().material = opaquePlaneMaterial;
            Debug.Log("Opaque Mode");
        }
    }

    IEnumerator ReEnableTouch()
    {
        yield return new WaitForSeconds(1f);
        isTouched = false;
    }

    enum PlaneVisibilityMode
    {
        Opaque,
        Transparent
    }
}
