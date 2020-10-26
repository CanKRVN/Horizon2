using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turner : MonoBehaviour
{
    public float turnSpeed;
    private void Update()
    {
        transform.Rotate(new Vector3(0, turnSpeed * -transform.position.x, 0) * Time.deltaTime);

    }
}
