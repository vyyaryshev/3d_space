using UnityEngine;

public class CanvasAimController : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}