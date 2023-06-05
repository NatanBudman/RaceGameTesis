using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class IceWall : MonoBehaviour
{
    public float duration;
    public float raiseSpeed;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float elapsedTime;
    public float destroyDuration;
    private float currentDestroyDuration;
    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * 4; // Puedes ajustar esta dirección según el tamaño del muro
        currentDestroyDuration = 0f;
        StartCoroutine(RiseWall());
    }
    private void Update()
    {
        currentDestroyDuration += Time.deltaTime;
        if(currentDestroyDuration >= destroyDuration)
        {
            Destroy(gameObject);
        }


    }
    private IEnumerator RiseWall()
    {
        elapsedTime = 0f;
        float distanceToRaise = Vector3.Distance(initialPosition, targetPosition);
        float speed = distanceToRaise / duration;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float currentSpeed = Mathf.Lerp(0f, speed * raiseSpeed, t); // Multiplica la velocidad máxima por raiseSpeed
            Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, currentSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
