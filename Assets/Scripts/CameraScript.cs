using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerTransform;
    public float offset = 0f;
    public bool followVertically;

    private Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (followVertically)
        {
            t.position = new Vector3(playerTransform.position.x - offset, playerTransform.position.y, t.position.z);
        }
        else
        {
            t.position = new Vector3(playerTransform.position.x - offset, t.position.y, t.position.z);
        }
    }
}
