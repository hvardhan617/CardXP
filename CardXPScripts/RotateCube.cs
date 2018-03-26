using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour
{

    public float speed = 100.0f;
    void Start()
    {

    }
    void Update()
    {
        transform.Rotate(Vector3.up,speed * Time.deltaTime);
    }

    

}
 

