using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject groundPrefab;

    public int groundCount;
    private float groundWidth;

    private Queue<GameObject> groundPool = new Queue<GameObject>();
    private GameObject lastGround; // pointer to access last added ground item
    private Vector2 startPosition;
    private GameObject hamster;
    private float playerPos;
    
    // Start is called before the first frame update
    void Start()
    {
        groundWidth = groundPrefab.GetComponent<Renderer>().bounds.size.x;
        startPosition = transform.position;
        hamster = GameObject.FindWithTag("Player");
        float offsetX = startPosition.x;
        playerPos = hamster.transform.position.x;
        
        for (int i = groundCount - 1; i >= 0; i--)
        {
            // calculate position of the ground piece, instantiate, and add to queue
            float calculatedPositionX = -i * groundWidth + 12f;

            Vector2 calculatedPosition = new Vector2(calculatedPositionX, startPosition.y);
            
            GameObject ground = Instantiate(groundPrefab, calculatedPosition, Quaternion.identity);
            groundPool.Enqueue(ground);
            lastGround = ground;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject firstGround = groundPool.Peek();
        float firstGroundPos = firstGround.transform.position.x;
        
         if (firstGroundPos < playerPos - 5.5f) 
         { // if first ground is past player, move ground to end of queue
             groundPool.Dequeue();
             float lastGroundPos = lastGround.transform.position.x;
             firstGround.transform.position = new Vector2(lastGroundPos + groundWidth, startPosition.y);
             groundPool.Enqueue(firstGround);
             lastGround = firstGround;
         }
    }
}