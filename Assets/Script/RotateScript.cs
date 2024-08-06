using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{

    public float degressPerSecond = 90f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float stepY = degressPerSecond * Time.deltaTime;
        transform.Rotate(0, stepY, 0);
    }
}
