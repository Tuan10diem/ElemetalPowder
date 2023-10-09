using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Exploding : MonoBehaviour
{

    public List<ItemPickup> _itemPickups;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1f);       
    }

    private void OnDestroy()
    {
        int randomItem = Random.Range(0, _itemPickups.Count);
        Instantiate(_itemPickups[randomItem], this.transform.position, Quaternion.identity);
    }
}
