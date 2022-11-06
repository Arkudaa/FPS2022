using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageNPC : MonoBehaviour
{
    private int health;
    public GameObject smoke;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    public void gotHit()
    {
        health -= 50;
    }

    private void Destroy()
    {
        GameObject lastSmoke = (GameObject)(Instantiate(smoke, transform.position, Quaternion.identity));
        Destroy(lastSmoke, 3);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Destroy();
    }
}
