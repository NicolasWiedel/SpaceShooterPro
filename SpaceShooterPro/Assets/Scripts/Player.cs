using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    // data type (it, float, bool, string)
    // every variale has an name
    // optional value assiged
    [SerializeField]
   private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        // take current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    { 
       float horizontalInput = Input.GetAxis("Horizontal");

    // Vector3.right = new Vector3(1, 0, 0)
    transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
    }
}
