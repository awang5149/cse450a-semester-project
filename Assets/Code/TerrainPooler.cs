using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TerrainPooler : MonoBehaviour 
// This class is based on Brackeys' object pooling tutorial linked here: https://www.youtube.com/watch?v=tdSmKaJvCoA
{
    [System.Serializable]
    public class Block
    {
        public string name;
        public GameObject prefab;
        public float width;
        public float height;
        public float size;
    }

    public static TerrainPooler instance;
    
    public List<Block> terrainBlocks; //
    public Dictionary<string, Queue<GameObject>> blockDictionary; //dictionary to hold block names to their pools
    
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        blockDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Block block in terrainBlocks) 
        { //for each block in the list, initialize their pool/queue and add to dict
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < block.size; i++)
            {
                GameObject obj = Instantiate(block.prefab);
                SpriteRenderer spr = GetSpriteRenderer(block.prefab); //to differentiate between prefabs that have/dont have parent containers
                block.width = spr.bounds.size.x;
                block.height = spr.bounds.size.y; // initialize block widths and height upon instantiation
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            blockDictionary.Add(block.name, queue);
        }
    }

    public GameObject SpawnFromPool(string blockName, Vector2 position)
    { // takes obj with blockName from pool to be placed at position
        if (!blockDictionary.ContainsKey(blockName))
        {
            Debug.Log("Block with name: " + blockName + " does not exist.");
            return null;
        }
        if (blockDictionary[blockName].Count == 0)
        {
            Debug.Log("Pool for block " + blockName + " is empty!");
            return null;
        }
        GameObject objToSpawn = blockDictionary[blockName].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position; 
        
        blockDictionary[blockName].Enqueue(objToSpawn); // repopulate pool
        return objToSpawn;
    }
    
    private SpriteRenderer GetSpriteRenderer(GameObject obj)
    {
        if (obj.transform.childCount > 0) {
            return obj.GetComponentInChildren<SpriteRenderer>();
        }
        return obj.GetComponent<SpriteRenderer>();
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
}
