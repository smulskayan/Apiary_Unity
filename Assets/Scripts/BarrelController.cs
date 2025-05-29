using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * move * moveSpeed * Time.deltaTime);
        float clampedX = Mathf.Clamp(transform.localPosition.x, -4f, 4f);
        transform.localPosition = new Vector3(clampedX, transform.localPosition.y, transform.localPosition.z);
    }
}