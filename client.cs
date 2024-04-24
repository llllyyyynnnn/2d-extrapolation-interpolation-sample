using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class client : MonoBehaviour
{
    // todo: points instead of vel

    float last_vel = 0;
    bool updated = false;
    float pingMs = 120;
    private float measure_acceleration(float v1, float v0) 
        => (v1 - v0) / Time.fixedDeltaTime; 
    private float predict_updated_velocity(float current, float elapsed) 
        => current + (measure_acceleration(current, last_vel) * elapsed); 

    private void Update()
    {
        float pingSec = pingMs / 1000;
        GameObject server = FindObjectOfType<moveServer>().gameObject;
        Rigidbody2D rb2d = server.GetComponent<Rigidbody2D>();

        float x = server.transform.position.x;
        float delta = x - transform.position.x;

        float move = delta * Time.deltaTime;

        float predicted_velocity = predict_updated_velocity(rb2d.velocity.x, pingSec);
        float velocity_diff = predicted_velocity / rb2d.velocity.x;
        float set_velocity;

        float extrapolation_max_ping = 100;
        float ping_diff = pingMs / extrapolation_max_ping;

        if (ping_diff > 1)
            set_velocity = 0;
        else
            set_velocity = velocity_diff * rb2d.velocity.x * ping_diff;


        float new_pos = ( (transform.position.x + move) + 
                          (predict_updated_velocity(rb2d.velocity.x, pingSec) * Time.deltaTime));

        transform.position = new Vector3(new_pos, server.transform.position.y, 0);
        if (updated) return;
        StartCoroutine(update_velocity(rb2d.velocity.x, pingSec));
        pingMs = Random.Range(5, 30);
    }

    IEnumerator update_velocity(float val, float elapsed)
    {
        updated = true;
        yield return new WaitForSeconds(elapsed);
        last_vel = val;
        updated = false;

        Debug.Log(elapsed * 1000 + "ms elapsed");
    }
}
