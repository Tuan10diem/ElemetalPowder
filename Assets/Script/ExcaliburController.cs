using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ExcaliburController : MonoBehaviour
{

    public BombController bombController;
    public GameObject arcOfEnergy;
    public KeyCode inputKey = KeyCode.Space;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    private void Awake()
    {
        inputKey = bombController.inputKey;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput1D();
        if(UnityEngine.Input.GetKeyDown(inputKey))
        {
            ExcaliburAttack();
        }
    }

    void ExcaliburAttack()
    {
        GameObject arc = Instantiate(arcOfEnergy, this.transform.position, this.transform.rotation);
    }

    void GetInput1D()
    {
        if (UnityEngine.Input.GetKey(inputUp))
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (UnityEngine.Input.GetKey(inputDown))
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        else if (UnityEngine.Input.GetKey(inputLeft))
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (UnityEngine.Input.GetKey(inputRight))
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
