using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{
    
    public Camera mainCamera;
    public GameObject groundPlane;
    private const float prefabRotation = 180f;

   
    public void Awake()
    {
        Application.targetFrameRate = 60;
    }

  
    public void Update()
    {
        _UpdateApplicationLifecycle();

        if (IsPointerOverUIObject())
            return;
        if (Input.touchCount == 2)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;
            Vector3 tmpTouchedMousePos = Input.mousePosition;
            if (Frame.Raycast(tmpTouchedMousePos.x, tmpTouchedMousePos.y, raycastFilter, out hit))
            {
                // Instantiate prefab at the hit pose.
                var tmpGameObject = Instantiate(groundPlane, hit.Pose.position, hit.Pose.rotation);

                // Compensate for the hitPose rotation facing away from the raycast (i.e.
                // camera).
                tmpGameObject.transform.Rotate(0, prefabRotation, 0, Space.Self);

                // Create an anchor to allow ARCore to track the hitpoint as understanding of
                // the physical world evolves.
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                Debug.Log(anchor);
                // Make game object a child of the anchor.
                tmpGameObject.transform.parent = anchor.transform;
                tmpGameObject.SetActive(true);
                StartCoroutine(tmpGameObject.GetComponentInChildren<BreakPlane>().BreakingPlane(hit.Pose.position));
            }
        }
      
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    private void _UpdateApplicationLifecycle()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }

}

