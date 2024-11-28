using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = target.position;
    }
}
