using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill2_Kame : MonoBehaviour
{

    public GameObject target;
    public float rotationSpeed;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        //angle = Mathf.Atan2(target.transform.position.y, target.transform.position.x);

        //Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        

        Vector3 diff = target.transform.position - this.transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation=Quaternion.Euler(0f,0f,rot_z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z), rotationSpeed * Time.deltaTime);
        
    }

    private void OnDrawGizmos()
    {
        // Draw a line gizmo to visualize the target angle
        //Vector3 diff = target.transform.position - this.transform.position;
        //diff.Normalize();
        //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //Vector3 direction = Quaternion.Euler(0, 0, rot_z) * Vector3.right;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right*10f);
    }
}

