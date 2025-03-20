using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleGenerator : MonoBehaviour
{
    public GameObject eagle;

    public float minSpeed;
    public float maxSpeed;
    public float currentSpeed;
    public float spawnRate = 4.5f;
    public float speedConstant;
    
    // Start is called before the first frame update
    void Awake()
    {
        currentSpeed = minSpeed;
        InvokeRepeating(nameof(generateEagle), 1f, spawnRate);
    }

    public void generateEagle() 
    {
        GameObject newEagle = Instantiate(eagle, transform.position, transform.rotation);
        newEagle.GetComponent<EagleScript>().EagleGenerator = this;
		
    } // instantiate new eagle and attach EagleScript

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            // currentSpeed += speedConstant;
        } // constantly accelerate eagles
    }
}
