using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public List<MeshRenderer> meshRenderers;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Material mat = meshRenderers[0].material;
            for (int i = 0; i < meshRenderers.Count; i++)
            {
                meshRenderers[i].sharedMaterial = mat;
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(meshRenderers[0].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < meshRenderers.Count; i++)
            {
              Mesh mesh =  meshRenderers[i].GetComponent<MeshFilter>().mesh;
            }
        }

    }
}
