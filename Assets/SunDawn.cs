using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SunDawn : MonoBehaviour
{
    public float timeRemaining = 60;
    Vector3 startVector;
    Vector3 endVector;
    Color startColor = new Color(253, 94, 83);
    Color endColor=  new Color(254, 41, 5);
    Color middleColor = new Color(255, 255, 255);
    Light lightObject;
    [SerializeField] Color currentColor;
    bool active;

    private void Start()
    {
        startVector = new Vector3(0, -90, 0);
        endVector = new Vector3(180, -90, 0);
        lightObject = GetComponent<Light>();
        lightObject.intensity = 0.005f;
        active = false;
    }

    public void SetActive() {
        active = true;
    }


    void Update()
    {
        if (active)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            if( timeRemaining >= 30)
            {
                lightObject.color = Color.Lerp(startColor, middleColor, timeRemaining / 60);
                print("Yellow to white");
            }
            else
            {
                lightObject.color = Color.Lerp(middleColor, endColor, timeRemaining / 60);
                print("White to red");
            }
            
           

            currentColor = lightObject.color;
            Vector3 interpolatedPosition = Vector3.Lerp(startVector, endVector, timeRemaining / 60);
            transform.rotation = Quaternion.Euler(interpolatedPosition);
        }
    }
        
        
}