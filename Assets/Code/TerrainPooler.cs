// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class TerrainPooler : MonoBehaviour 
// // This class is based on Brackeys' object pooling tutorial linked here: https://www.youtube.com/watch?v=tdSmKaJvCoA
// {
//     [System.Serializable]
//     public class Block
//     {
//         public string name;
//         public GameObject prefab;
//         public float width;
//         public float size;
//     }
//
//     public static TerrainPooler instance;
//     
//     public List<Block> terrainBlocks; //
//     public Dictionary<string, Queue<GameObject>> blockDictionary; //dictionary to hold block names to their pools
//     
//     private void Awake()
//     {
//         instance = this;
//     }
//     
//     // Start is called before the first frame update
//     void Start()
//     {
//         blockDictionary = new Dictionary<string, Queue<GameObject>>();
//
//         foreach (Block block in terrainBlocks) 
//         { //for each block in the list, initialize their pool/queue and add to dict
//             Queue<GameObject> queue = new Queue<GameObject>();
//             for (int i = 0; i < block.size; i++)
//             {
//                 GameObject obj = Instantiate(block.prefab);
//                 SpriteRenderer spr = GetSpriteRenderer(block.prefab); //to differentiate between prefabs that have/dont have parent containers
//                 block.width = spr.bounds.size.x; // initialize block widths upon instantiation
//                 obj.SetActive(false);
//                 queue.Enqueue(obj);
//             }
//             blockDictionary.Add(block.name, queue);
//         }
//     }
//
//     public GameObject SpawnFromPool(string blockName, Vector2 position)
//     { // takes obj with blockName from pool to be placed at position
//         if (!blockDictionary.ContainsKey(blockName))
//         {
//             //Debug.Log("Block with name: " + blockName + " does not exist.");
//             return null;
//         }
//         if (blockDictionary[blockName].Count == 0)
//         {
//             //Debug.Log("Pool for block " + blockName + " is empty!");
//             return null;
//         }
//         GameObject objToSpawn = blockDictionary[blockName].Dequeue();
//         objToSpawn.SetActive(true);
//         objToSpawn.transform.position = position; 
//         
//         blockDictionary[blockName].Enqueue(objToSpawn); // repopulate pool
//         return objToSpawn;
//     }
//     
//     private SpriteRenderer GetSpriteRenderer(GameObject obj)
//     {
//         return obj.transform.childCount > 0 ? obj.transform.GetComponentInChildren<SpriteRenderer>() : obj.GetComponent<SpriteRenderer>();
//     }
//     
//     public float GetBlockWidth(string blockName)
//     {
//         foreach (Block block in terrainBlocks)
//         {
//             if (blockName == block.name)
//                 return block.width;
//         }
//         return 1.6f;
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPooler : MonoBehaviour
{
    [System.Serializable]
    public class Block
    {
        public string name;
        public GameObject prefab;
        public float width;
        public float size;
        public string biome; // New field to categorize blocks by biome
    }

    public static TerrainPooler instance;

    public List<Block> terrainBlocks;
    private Dictionary<string, Queue<GameObject>> blockDictionary;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        blockDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Block block in terrainBlocks)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < block.size; i++)
            {
                GameObject obj = Instantiate(block.prefab);
                SpriteRenderer spr = GetSpriteRenderer(block.prefab);
                block.width = spr.bounds.size.x;
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            blockDictionary.Add(block.name, queue);
        }
    }

    public GameObject SpawnFromPool(string blockName, Vector2 position)
    {
        if (!blockDictionary.ContainsKey(blockName) || blockDictionary[blockName].Count == 0)
        {
            Debug.Log("Block with name: " + blockName + " does not exist or is empty.");
            return null;
        }
        GameObject objToSpawn = blockDictionary[blockName].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;

        blockDictionary[blockName].Enqueue(objToSpawn);
        return objToSpawn;
    }

    private SpriteRenderer GetSpriteRenderer(GameObject obj)
    {
        return obj.transform.childCount > 0 ? obj.transform.GetComponentInChildren<SpriteRenderer>() : obj.GetComponent<SpriteRenderer>();
    }

    public float GetBlockWidth(string blockName)
    {
        foreach (Block block in terrainBlocks)
        {
            if (blockName == block.name)
                return block.width;
        }
        return 1.6f;
    }

    public string[] GetBiomeBlocks(string biome)
    {
        List<string> biomeBlocks = new List<string>();
        foreach (Block block in terrainBlocks)
        {
            if (block.biome == biome)
            {
                biomeBlocks.Add(block.name);
            }
        }
        return biomeBlocks.ToArray();
    }
}
