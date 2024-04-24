using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveServer : MonoBehaviour
{
    float tickrate = 2f;
    public float offset = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            float random_force = Random.Range(-10f, 10f);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().AddForce(new Vector3(random_force, 0), ForceMode2D.Impulse);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(GetComponent<Rigidbody2D>().velocity.x, 0, 2f * Time.deltaTime), 0);
    }
}
