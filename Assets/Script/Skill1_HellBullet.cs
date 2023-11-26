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
    private float timer = 0;
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
            bullet.GetComponent<BulletController>().damage = bossController.GetComponent<BossController>().listSkill[0].damage;
        }
    }

    private void Rotating()
    {
        for (int i = 0; i < firePoint.Count; i++)
        {
            firePoint[i].transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            if (firePoint[i].transform.rotation.eulerAngles.z >= 340f)
            {
                if(rotationSpeed > 0) rotationSpeed = -rotationSpeed;
            }
            if (firePoint[i].transform.rotation.eulerAngles.z <= 200f)
            {
                if (rotationSpeed < 0) rotationSpeed = -rotationSpeed;
            }
        }
        
        
        
    }

}
