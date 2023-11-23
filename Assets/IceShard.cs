using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : BulletController
{
    private void Awake()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;

    }

    private void Start()
    {
        StartCoroutine(enableCollider());
    }

    private IEnumerator enableCollider()
    {
        yield return new WaitForSeconds(3f);
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

}
