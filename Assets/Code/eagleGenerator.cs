using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagleGenerator : MonoBehaviour
{
    public GameObject eagle;

    public float minSpeed;
    public float maxSpeed;
    public float currentSpeed;
    public float speedConstant;
    
    // Start is called before the first frame update
    void Awake()
    {
        currentSpeed = minSpeed;
        generateEagle();
    }

    public void generateEagle()
    {
        GameObject newEagle = Instantiate(eagle, transform.position, transform.rotation);
        
        newEagle.GetComponent<eagleScript>().eagleGenerator = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedConstant;
        }
    }
}
