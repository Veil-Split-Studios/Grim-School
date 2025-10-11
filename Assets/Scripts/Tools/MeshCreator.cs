using UnityEngine;

public class MeshCreator : MonoBehaviour
{

    Mesh myMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myMesh = CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public Mesh CreateMesh()
    {
        GameObject meshObject = new GameObject("MyMeshObject", typeof(MeshFilter), typeof(MeshRenderer));
        myMesh = new Mesh();
        myMesh.name = "MyMesh";
        Vector3[] vertices = new Vector3[9];
        int[] triangles = new int[9];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 1, 0);
        vertices[3] = new Vector3(0, 2, 0);
        vertices[5] = new Vector3(0, 3, 0);
        vertices[6] = new Vector3(0, 3, 0);
        vertices[7] = new Vector3(0, 3, 0);
        vertices[8] = new Vector3(0, 3, 0);

        Vector2[] uv = new Vector2[4];


        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 3;
        triangles[5] = 4;


        myMesh.vertices = vertices;
        myMesh.triangles = triangles;
        myMesh.RecalculateNormals();
        return myMesh;
    }
}
