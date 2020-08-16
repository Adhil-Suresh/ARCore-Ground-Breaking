using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneInRange : MonoBehaviour
{
    [HideInInspector]
    public bool InRange = false;

    public int index=-1;

    private void Start()
    {
        index = transform.GetSiblingIndex();
    }

}
