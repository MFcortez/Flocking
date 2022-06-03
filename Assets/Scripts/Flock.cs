using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager manager;
    float speed;

    void Start()
    {
        speed = Random.Range(manager.minSpeed,
            manager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyRules();
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = manager.allFish;

        Vector3 vCenter = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach(GameObject go in gos)
        {
            if(go != gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, transform.position);
                if(nDistance <= manager.neighbourDistance)
                {
                    vCenter += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f)
                    {
                        vAvoid += (transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed += anotherFlock.speed;
                }
            }
        }
        if(groupSize > 0)
        {
            vCenter /= groupSize;
            gSpeed /= groupSize;

            Vector3 direction = (vCenter - vAvoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                    Quaternion.LookRotation(direction),
                    manager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
