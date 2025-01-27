using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuoyantScript : MonoBehaviour
{
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float BouyancyForce = 10;
    private Rigidbody thisRigidbody;
    private bool hasTouchedWater;
    
    void Awake() {
        thisRigidbody = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void FixedUpdate() {
        // Check if underwater
        float diffy = transform.position.y;
        bool isUnderwater = diffy < 0;
        if(isUnderwater) {
            hasTouchedWater = true;
        }

        // Ignore if never touched water
        if(hasTouchedWater) {
            return;
        }

        // bouyance logic

        if(isUnderwater) {
          Vector3 vector = Vector3.up * BouyancyForce * -diffy;
          thisRigidbody.AddForce(vector, ForceMode.Acceleration);

        }
        thisRigidbody.drag = isUnderwater ? underwaterDrag : airDrag;
        thisRigidbody.angularDrag = isUnderwater ? underwaterAngularDrag : airAngularDrag;

    }
}
