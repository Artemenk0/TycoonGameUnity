using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public float moveSpeed = 10f; 
    public float edgeThreshold = 50f; 
    public Vector2 minBounds = new Vector2(-50, -50); 
    public Vector2 maxBounds = new Vector2(50, 50); 

    private void Start()
    {
        transform.rotation = Quaternion.Euler(55f, -180f, 0f);
        transform.position = new Vector3(-100f, 30f, 20f);
    }

    private void Update()
    {
        Vector3 direction = Vector3.zero;
        Vector3 position = transform.position;

        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x <= edgeThreshold) direction.x = 1; 
        if (mousePos.x >= Screen.width - edgeThreshold) direction.x = -1; 
        if (mousePos.y <= edgeThreshold) direction.z = 1; 
        if (mousePos.y >= Screen.height - edgeThreshold) direction.z = -1; 

        position += direction * moveSpeed * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        position.z = Mathf.Clamp(position.z, minBounds.y, maxBounds.y);

        transform.position = position;
    }
}


