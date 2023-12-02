using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float _gravitation;

    private Camera _mainCamera;
    private Rigidbody rb;

    private Plane _dragPlane;
    private Vector3 _offset;

    public bool IsDragging = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }


    void Update()
    {
        if (rb != null)
        {
            if (GameManager.Instance.SwithGravitator)
            {
                rb.useGravity = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 10f, transform.position.z), _gravitation * Time.deltaTime);
            }
            else
            {
                rb.useGravity = true;
            }

        }
    }
    void OnMouseDown()
    {
        _dragPlane = new Plane(_mainCamera.transform.forward, transform.position);
        Ray camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);
        _offset = transform.position - camRay.GetPoint(planeDist);
        IsDragging = true;
    }

    void OnMouseDrag()
    {
        if (IsDragging)
        {
            Ray camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDist;
            _dragPlane.Raycast(camRay, out planeDist);
            transform.position = camRay.GetPoint(planeDist) + _offset;
        }
    }
}
