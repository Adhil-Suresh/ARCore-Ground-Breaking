using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlane : MonoBehaviour
{
    public bool IsEditorChecking = false;

    private void Update()
    {
        if(IsEditorChecking)
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
              StartCoroutine(BreakingPlane(hit.point));
            }
        }
    }

    public LayerMask UpdateUVplaneLayerMask;
    public float minRange = 0.5f;
    public float maxRange = 1f;
    public float explosionForce = 100f;
    public float maxUpValue = 10f;
    public string planeTagName = "PlaneUpdateUV";
    public LayerMask defaultLayer;
    public IEnumerator BreakingPlane(Vector3 tmpWorldPos)
    {
        List<int> affectedPlaneIndices = new List<int>();
        yield return null;
        yield return null;
        yield return null;
        float tmpRange = UnityEngine.Random.Range(minRange, maxRange);
        Debug.Log(tmpRange);
        Collider[] tmpColliders = Physics.OverlapSphere(tmpWorldPos, tmpRange, UpdateUVplaneLayerMask);
        foreach (var col in tmpColliders)
        {
            if(col.gameObject.CompareTag(planeTagName))
            {
                col.GetComponent<PlaneInRange>().InRange = true;
                col.GetComponent<Rigidbody>().isKinematic = false;
                col.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, tmpWorldPos, tmpRange, maxUpValue, ForceMode.Impulse);
                affectedPlaneIndices.Add(col.GetComponent<PlaneInRange>().index);
            }
        }

        ////////--Enabling Not Updated UV Plane GameObjects--//////////////
        int tmpAffectedPlaneIndicesCount = affectedPlaneIndices.Count;
        CheckingFaceToTextureMethod2 tmpUpdateUVScript = GetComponent<CheckingFaceToTextureMethod2>();
        for (int i = 0; i < tmpAffectedPlaneIndicesCount; i++)
        {
            tmpUpdateUVScript.meshFiltersNoUV[affectedPlaneIndices[i]].gameObject.SetActive(true);
        }
        ////////--Enabling Not Updated UV  Plane GameObjects--//////////////

        ////////--Destroying GameObjects that are not affected--//////////////
        yield return null;
        int tmpMeshesLength = tmpUpdateUVScript.meshFiltersNoUV.Length;
        for (int i = 0; i < tmpMeshesLength; i++)
        {
            if (!affectedPlaneIndices.Contains(i))
            {
                Destroy(tmpUpdateUVScript.meshFiltersNoUV[i].gameObject);
                Destroy(tmpUpdateUVScript.meshFiltersRealtimeUV[i].gameObject);
            }
        }
        ////////--Destroying GameObjects that are not affected--//////////////

        yield return new WaitForSeconds(5f);
        ////////--Destroying Physics(Rigidbogy)--//////////////
        for (int i = 0; i < tmpAffectedPlaneIndicesCount; i++)
        {
            tmpUpdateUVScript.meshFiltersRealtimeUV[affectedPlaneIndices[i]].gameObject.layer = defaultLayer;
            Destroy(tmpUpdateUVScript.meshFiltersRealtimeUV[affectedPlaneIndices[i]].GetComponent<Rigidbody>());
        }
        ////////--Destroying Physics(Rigidbogy)--//////////////
    }

}
