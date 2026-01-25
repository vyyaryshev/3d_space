using UnityEngine;

public class DynamicAimController : MonoBehaviour
{
    public Vector3 aimPosition;
    Vector2 screenCenter;
    private void Start()
    {
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }
    private void Update()
    {
        Ray screenRay = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit)) transform.position = hit.point;
        else transform.position = screenRay.GetPoint(400f);
        aimPosition = transform.position;
    }
}