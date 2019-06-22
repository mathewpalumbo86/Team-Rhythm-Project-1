using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    // Stores the mesh being generated
    public Mesh mesh;

    // Arrays that store the mesh values
    Vector3[] vertices;
    int[] triangles;

    // mMsh grid size
    public int xSize = 20;
    public int zSize = 20;

    // Delay between creating new tiles (for testing)
    // public float pauseBetweenTiles;

    
    // Start is called before the first frame update
    void Start()
    {
        // Creates the mesh and stores it
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

    // Creates the mesh data (vertices and triangles arrays)
    void CreateShape()
    {
        // Fills the array with vertices (+1's because always 1 more vertice than square on an axis)
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
        // Gives all the vertices a position (x,y,z)
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
    
    // Updates mesh using the vertices and triangles arrays data
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
