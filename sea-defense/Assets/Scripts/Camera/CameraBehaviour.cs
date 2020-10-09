using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 0;
    public int xRes;
    public int yRes;
    private float xM;
    private float yM;

    public int Bound = 20;
    // Start is called before the first frame update
    void Start()
    {
        xRes = Screen.width;
        yRes = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if(Input.GetKey(KeyCode.W) ||  (Input.mousePosition.y > yRes - Bound && !EventSystem.current.IsPointerOverGameObject()))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A) || (Input.mousePosition.x < 0 + Bound && !EventSystem.current.IsPointerOverGameObject()))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S) || (Input.mousePosition.y < 0 + Bound && !EventSystem.current.IsPointerOverGameObject()))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D) || (Input.mousePosition.x > xRes - Bound && !EventSystem.current.IsPointerOverGameObject()))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x,0,xM), Mathf.Clamp(transform.position.y, yM, 0), -5);
    }
    
    public void cameraLimits(Vector3 max)
    {
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(1,0));
        xM = max.x - point.x;
        yM = max.y - point.y;
    }
}
