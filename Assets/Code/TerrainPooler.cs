// This class is based on Brackeys' object pooling tutorial linked here: https://www.youtube.com/watch?v=tdSmKaJvCoA
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
    public SpriteRenderer backgroundSprite;
    
    public Sprite defaultBackground;
    public Sprite iceBackground;
    public Sprite stoneBackground;
    public Sprite mudBackground;

    public List<Block> terrainBlocks;
    public List<Block> iceTerrainBlocks;
    public List<Block> stoneTerrainBlocks;
    public List<Block> mudTerrainBlocks;

    private string currentBiome = "default";
    public Dictionary<string, List<Block>> biomeDictionary;
    
    private Dictionary<string, Dictionary<string, Queue<GameObject>>> biomePoolDictionary;
    
    private void Awake()
    {
        instance = this;
        biomeDictionary = new Dictionary<string, List<Block>>()
        {
            { "default", terrainBlocks },
            { "ice", iceTerrainBlocks },
            { "stone", stoneTerrainBlocks },
            { "mud", mudTerrainBlocks },
        }; //initialize biome dictionary with correct terrain blocks
        
        biomePoolDictionary = new Dictionary<string, Dictionary<string, Queue<GameObject>>>(); //contains the actual terrain block pools

        foreach (KeyValuePair<string, List<Block>> biomeDictEntry in biomeDictionary)
        {
            string biomeName = biomeDictEntry.Key;
            List<Block> blocks = biomeDictEntry.Value;
            
            Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

            foreach (Block block in blocks)
            {
                Queue<GameObject> queue = new Queue<GameObject>();
                for (int i = 0; i < block.size; i++)
                {
                    GameObject obj = Instantiate(block.prefab);
                    obj.name = block.name; //gets rid of Clone in name
                    SpriteRenderer spr = GetSpriteRenderer(obj);
                    block.width = spr.bounds.size.x; // Set block width based on sprite
                    obj.SetActive(false);
                    queue.Enqueue(obj);
                }
                pool.Add(block.name, queue); //populate all pools with blocks
            }
            biomePoolDictionary.Add(biomeName, pool); //add each biome pool to the dictionary
        }
        
        
    }

    public GameObject SpawnFromPool(string blockName, Vector2 position)
    {
        UpdateBiome();
        
        Dictionary<string, Queue<GameObject>> currentPool = biomePoolDictionary[currentBiome];
        
        if (!currentPool.ContainsKey(blockName) || currentPool[blockName].Count == 0)
        {
            Debug.Log("Block with name: " + blockName + " does not exist.");
            return null;
        }
        GameObject objToSpawn = currentPool[blockName].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;

        currentPool[blockName].Enqueue(objToSpawn);
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
    private void UpdateBiome()
    {
        int score = ScoreAndMoneyManager.instance.score;

        string[] biomes = { "default", "ice", "mud", "stone" };
        
        int index = (score / 1000) % biomes.Length;
        currentBiome = biomes[index];
        UpdateBackground();
    }

    private void UpdateBackground()
    {
        if (currentBiome == "default")
        {
            backgroundSprite.sprite = defaultBackground;
            return;
        }

        if (currentBiome == "ice")
        {
            backgroundSprite.sprite = iceBackground;
            return;
        }

        if (currentBiome == "stone")
        {
            backgroundSprite.sprite = stoneBackground;
            return;
        }

        if (currentBiome == "mud")
        {
            backgroundSprite.sprite = mudBackground;
            return;
        }
    }
    
}
