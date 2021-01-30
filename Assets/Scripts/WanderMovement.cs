using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderMovement : MonoBehaviour
{
    public float speed;
    public float range;
    public Vector3 origin;
    public float radius;

    bool stopped;
    Vector3 wayPoint;

    private AnimationController anim;
    private new Transform transform;

    private bool IsInsideBoundary(Vector3 point)
    {
        return Vector3.Distance(point, origin) < radius;
    }

    private void SetDestination()
    {
        Vector3 destination = new Vector3();
        int count = 0;
        do
        {
            count++;
            destination.x = transform.position.x + Random.Range(-range, range);
            destination.y = transform.position.y;
            destination.z = transform.position.z + Random.Range(-range, range);
        } while (!IsInsideBoundary(destination) && count < 50);

        wayPoint = destination;
    }

    public void Stop()
    {
        stopped = true;
        anim.Move(0, 0);
    }


    void Start()
    {
        anim = GetComponent<AnimationController>();
        transform = GetComponent<Transform>();
        SetDestination();
    }


    void Update()
    {
        if (!stopped)
        {
            Vector3 dir = (wayPoint - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
            anim?.Move(dir.x, 1);
            if (Vector3.Distance(transform.position, wayPoint) < 0.1f)
            {
                SetDestination();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(origin, radius);
    }
}
