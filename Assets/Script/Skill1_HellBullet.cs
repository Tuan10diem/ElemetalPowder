using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_HellBullet : MonoBehaviour
{
    public List<GameObject> firePoint;
    public GameObject bulletPrefab;
    public GameObject bossController;
    public float timeBetweenShoots;
    private Subjects _subjects;
    private float timer = 0;
    public Vector3 to = new Vector3(0, 0, 225f); 
    public float rotationSpeed = 45f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenShoots)
        {
            Rotating();
            Shooting();
            timer = 0f;
        }   
    }

    private void Shooting()
    {
        foreach (var i in firePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, i.transform.position, i.transform.rotation);
            bullet.GetComponent<BulletController>().damage = bossController.GetComponent<BossController>().dameSkill[0];
        }
    }

    private void Rotating()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        //transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, -1f);
        //Debug.Log(transform.rotation.eulerAngles.z);
        if (transform.rotation.eulerAngles.z <= 180f || transform.rotation.eulerAngles.z >= 360f)
        {
            //to=new Vector3(0,0,225f+305f-transform.rotation.eulerAngles.z);
            rotationSpeed *= -1;
        }
        
    }

}
