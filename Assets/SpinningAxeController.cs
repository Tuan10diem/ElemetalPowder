using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAxeController : MonoBehaviour
{

    public float rotationSpeed;
    public float axeDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

    }
}
