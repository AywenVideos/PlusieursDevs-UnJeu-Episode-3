using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light spotlight; //
    [SerializeField] private float distanceMax = 2f;
    [SerializeField] Transform targetObject;

    private void Start()
    {
        if (spotlight == null)
        {
            spotlight = GetComponent<Light>();
        }
    }

    private void Update()
    {
        if (targetObject != null)
        {
            float distance = Vector3.Distance(transform.position, targetObject.position);
            if (distance < distanceMax)
            {
                spotlight.enabled = false;
            }
            else
            {
                spotlight.enabled = true;
            }
        }
    }
}