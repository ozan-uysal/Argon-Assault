using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float controlSpeed = 5f;


    void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");
        Debug.Log(xThrow);

        float xOffset = xThrow*Time.deltaTime*controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYPos=transform.localPosition.y + yOffset;

        transform.localPosition = new Vector3
            (newXPos,
             newYPos,
            transform.localPosition.z);
    }
}
