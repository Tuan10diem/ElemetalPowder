using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSpark : MonoBehaviour
{

    public int damage;
    public float countDown;
    public Collider2D collider2D;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        collider2D.enabled = false;
        yield return new WaitForSeconds(countDown);

        collider2D.enabled = true;
        
        transform.localScale = new Vector3(2f, 4f, 1f);
        this.transform.parent = null;
        yield return new WaitForSeconds(0.5f);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f,1f,1f), 5f * Time.deltaTime);

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision) return;
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatus>().HandleHurt(damage);
        }
    }
}
