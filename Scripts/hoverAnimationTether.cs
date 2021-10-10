using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hoverAnimationTether : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalSpeed;
    public float amplitude; 
    public Vector3 tempPosition;
    // Start is called before the first frame update
    void Start()
    {
        tempPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempPosition.x += horizontalSpeed;
        tempPosition.y = (Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude) + 250;
        transform.position = tempPosition;
    }
}
