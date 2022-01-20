using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class World
{
    public string worldName;
    public List<Level> levels = new List<Level>();
}
[System.Serializable]
public class Level
{
    public int levelNumber;
    public GameObject[] platforms;
}
public class PlatformManager : MonoBehaviour
{
    [SerializeField] List<World> worlds = new List<World>();
    public Transform spawnPoint;
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        GenerateWorld();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void GenerateWorld()
    {
        {
            foreach (World world in worlds)
            {
                for (int i = 0; i < world.levels.Count; i++)
                    foreach (Level level in world.levels)
                {
                    if (level.levelNumber == i)
                    {
                        Vector3 pos = new Vector3(0f, 40 * i, 0f);
                        spawnPoint.position = pos;
                        Instantiate(level.platforms[Random.Range(0, level.platforms.Length)], spawnPoint.position, Quaternion.identity, GameObject.Find("Platform").transform);
                    }
                }
            }
        }
    }
}
