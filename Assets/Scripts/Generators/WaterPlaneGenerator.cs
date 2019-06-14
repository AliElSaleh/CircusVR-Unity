using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WaterPlaneGenerator : MonoBehaviour
{
    public float Size = 1.0f;

    public int GridSize = 16;

    private MeshFilter Filter;

    [UsedImplicitly]
	void Start()
    {
        Filter = GetComponent<MeshFilter>();
        Filter.mesh = GenerateMesh();
    }
	
    [UsedImplicitly]
    void Update()
    {
		
	}

    private Mesh GenerateMesh()
    {
        Mesh Mesh = new Mesh();

        var Vertices = new List<Vector3>();
        var Normals = new List<Vector3>();
        var UVs = new List<Vector2>();

        for (int x = 0; x < GridSize+1; x++)
        {
            for (int y = 0; y < GridSize+1; y++)
            {
                Vertices.Add(new Vector3(-Size * 0.5f + Size * (x/(float)GridSize), 0, -Size * 0.5f + Size * (y / (float)GridSize)));
                Normals.Add(Vector3.up);
                UVs.Add(new Vector2(x / (float)GridSize, y / (float)GridSize));
            }
        }

        var Triangles = new List<int>();
        var VerticesCount = GridSize + 1;

        for (int i = 0; i < VerticesCount * VerticesCount - VerticesCount; i++)
        {
            if ((i + 1) % VerticesCount == 0)
            {
                continue;
            }

            Triangles.AddRange(new List<int>()
            {
                i + 1 + VerticesCount,
                i + VerticesCount,
                i,
                i,
                i + 1,
                i + VerticesCount + 1
            });
        }

        Mesh.SetVertices(Vertices);
        Mesh.SetNormals(Normals);
        Mesh.SetUVs(0, UVs);
        Mesh.SetTriangles(Triangles, 0);

        return Mesh;
    }
}
