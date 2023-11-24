using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill2_Kame : MonoBehaviour
{

    public GameObject target;
    public float rotationSpeed;
    public GameObject finalSpark;
    public GameObject bossController;

    private float timer = 0;
    private float timeBetween2Shot = 4f;

    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        timer += Time.deltaTime;
        if(timer >= timeBetween2Shot) 
        {
            GameObject kame = Instantiate(finalSpark, this.transform.position, this.transform.rotation);
            kame.GetComponent<FinalSpark>().damage = bossController.GetComponent<BossController>().dameSkill[1];
            kame.transform.parent = this.transform;
            timer = 0;
        }
    }

    private void FollowPlayer()
    {

        Vector3 diff = target.transform.position - this.transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z), rotationSpeed * Time.deltaTime);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right*10f);
    }
}

