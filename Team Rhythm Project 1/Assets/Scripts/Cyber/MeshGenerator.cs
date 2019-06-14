using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{


    // Stores the mesh being generated
    public Mesh mesh;

    // arrays that store the mesh values
    Vector3[] vertices;
    int[] triangles;

    // mesh grid size
    public int xSize = 20;
    public int zSize = 20;

    // Delay between creating new tiles (for testing)
    // public float pauseBetweenTiles;

    
    // Start is called before the first frame update
    void Start()
    {
        // creates the mesh and stores it
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Sets up the mesh
        CreateShape();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CreateShape()
    {
        // Fills the array with vertices (+1's because always 1 more vertice than square on an axis)
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
        // gives all the vertices a position
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                // Applies perlin noise to the vertices heights to give uneven terrain
                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;

                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        // Setup the triangles array (allows for grid size and number of points per quad)
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for(int z = 0; z < zSize; z++)
        {
            // Fills the array with triangles
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

                // yield return new WaitForSeconds(pauseBetweenTiles);

            }
            vert++;
        }
        
        

    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }


    // Used to show position of the vertices
    //private void OnDrawGizmos()
    //{

    //    if (vertices == null)
    //        return;
        
    //        for (int i = 0; i < vertices.Length; i++)
    //        {
    //            Gizmos.DrawSphere(vertices[i], 0.1f);
    //        }
    //}


    
}
