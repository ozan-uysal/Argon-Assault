using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float controlSpeed = 10f;
    public float xRange = 10f;
    public float yRange = 7f;
    public GameObject[] lasers;

    float xThrow;
    float yThrow;

    public float positionPitchFactor = -2f;
    public float controlPitchFactor = 10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    private void Start()
    {
      
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

     void ProcessTranslation()
    {
         xThrow = Input.GetAxis("Horizontal");
         yThrow = Input.GetAxis("Vertical");
        //Debug.Log(yThrow);
        //Debug.Log(xThrow);

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3
            (clampedXPos,
             clampedYPos,
            transform.localPosition.z);
    }
    void ProcessRotation()
    {
        //float pitch = transform.localPosition.y*positionPitchFactor*controlPitchFactor*yThrow;
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }
    void ProcessFiring()
    {
        if (Input.GetButton("Fire1")) 
        {
            ActivateLeftLaser(true);
        } 
        
       else if(Input.GetButton("Fire2"))
        {
            ActivateRightLaser(true);
        }
        else
        {
            DeactiveLasers(false);
        }
    }
    void ActivateLeftLaser(bool isActive)
    {
        var emissionModule= lasers[0].GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
    }
    void ActivateRightLaser(bool isActive)
    {
        var emissionModule = lasers[1].GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
    }
    void DeactiveLasers(bool isActive)
    {
        foreach (var laser in lasers)
        {
          var emissionModule = laser.GetComponent<ParticleSystem>().emission;
          emissionModule.enabled = isActive;
        }
    }
}
