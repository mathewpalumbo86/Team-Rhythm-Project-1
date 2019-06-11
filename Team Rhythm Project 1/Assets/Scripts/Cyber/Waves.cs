
using UnityEngine;

public class Waves : MonoBehaviour
{
    public int depth = 20; // hight on Y axis 
    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;



     void Start()
     {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);// use to scroll the terrain to create the waves 
     }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();// getting the terrain feature from the terrain 

        terrain.terrainData = CreateWaves(terrain.terrainData); // chaning the terrain data 
    }


    TerrainData CreateWaves(TerrainData terrainData)
    {

        //terrainData.heightmapResolution = width + 1;//without wont make correct 

        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, CreateHeights()); // creating the waves and the diffrent heights 
        return terrainData;// import 

    }

    float [,] CreateHeights()
    {
        float[,] heights = new float[width, height]; // grid of floats that creates the points of waves 
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {

                heights[x, y] = CalculateHeight(x, y); // noise value 
            }
        }
        return heights;// import 

    }

    
    float CalculateHeight(int x, int y) // making the noise for the creations of the wave generation 
    {
       float xCord = (float)x / width * scale + offsetX;// these are noise map cords 
       float yCord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCord, yCord);// import 

    }



}
