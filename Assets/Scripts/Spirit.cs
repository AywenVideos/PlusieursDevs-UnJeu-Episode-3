using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    private float lifeTime;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = Random.Range(2,7);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0 || Vector3.Distance(transform.position,Camera.main.transform.position)<=3)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        gameObject.transform.GetComponentInChildren<Transform>().LookAt(Camera.main.transform);
    }
}
