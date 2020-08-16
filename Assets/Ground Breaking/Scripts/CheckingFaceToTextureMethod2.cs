using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System.Linq;
using UnityEngine.UI;
using System.IO;

public class CheckingFaceToTextureMethod2 : MonoBehaviour
{
    int meshCount = -1;
    public MeshFilter[] meshFiltersRealtimeUV;
    public MeshFilter[] meshFiltersNoUV;
    List<Vector3[]> vertices;
    List<Vector2[]> uv;


    Mesh[] meshes;
    Transform[] meshTransforms;
    Material meshMaterial;

    List<Vector3[]> storedVerts= new List<Vector3[]>();
    List<Vector2[]> storedUVs = new List<Vector2[]>();
    Camera cam;

    private void Awake()
    {
        meshCount = meshFiltersRealtimeUV.Length;
    }

    private void OnEnable()
    {
        for (int i = 0; i < meshCount; i++)
        {
            storedVerts.Add(meshFiltersRealtimeUV[i].mesh.vertices);
            storedUVs.Add(meshFiltersRealtimeUV[i].mesh.uv);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < meshCount; i++)
        {
            if(meshFiltersRealtimeUV[i]!=null)
            {
                meshFiltersRealtimeUV[i].mesh.vertices = storedVerts[i];
                meshFiltersRealtimeUV[i].mesh.uv = storedUVs[i];
            }
        }
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        meshes = new Mesh[meshCount];
        vertices = new List<Vector3[]>();
        uv = new List<Vector2[]>();
        meshTransforms = new Transform[meshCount];
        for (int i = 0; i < meshCount; i++)
        {
            meshes[i] = meshFiltersRealtimeUV[i].mesh;
            vertices.Add(new Vector3[meshes[i].vertices.Length]);
            uv.Add(new Vector2[vertices[i].Length]);
            meshTransforms[i] = meshFiltersRealtimeUV[i].transform;
        }

        cam = Camera.main;
        meshMaterial = meshFiltersRealtimeUV[0].GetComponent<MeshRenderer>().material;
        yield return null;
        yield return null;
        AssignUV();
        AssignTexture();
    }

  
    public bool isFlipHorizontal;
    public bool isFlipVertical;
    private void AssignUV()
    {
        for (int i = 0; i < meshCount; i++)
        {
            int verticesCount = vertices[i].Length;
            vertices[i] = meshes[i].vertices;
            for (int j = 0; j < verticesCount; j++)
            {
                vertices[i][j] = meshTransforms[i].TransformPoint(vertices[i][j]);
            }

            for (int j = 0; j < verticesCount; j++)
            {
                uv[i][j] = cam.WorldToViewportPoint(vertices[i][j]);
                if (isFlipHorizontal)
                    uv[i][j].x = 1 - uv[i][j].x;
                if (isFlipVertical)
                    uv[i][j].y = 1 - uv[i][j].y;
            }
            meshes[i].uv = uv[i];
        }
    }

    public Vector2 rtResolution=new Vector2(360,640);
    RenderTexture rt;
    void AssignTexture()
    {
        rt = new RenderTexture((int)rtResolution.x, (int)rtResolution.y, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(GameManager.instance.rt, rt);
        meshMaterial.mainTexture = rt;
        for (int i = 1; i < meshCount; i++)
        {
            meshFiltersRealtimeUV[i].GetComponent<MeshRenderer>().sharedMaterial = meshMaterial;
        }
    }

}
