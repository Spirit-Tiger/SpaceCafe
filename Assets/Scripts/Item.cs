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
    public bool IsComponent = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }


    void Update()
    {
        if (rb != null)
        {
            if (GameManager.Instance.SwithGravitator && !IsComponent)
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

    void OnMouseUp()
    {
        IsDragging = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsDragging && !IsComponent)
        {
            if (collision.collider.CompareTag("Ingredient"))
            {
                Transform newElement = collision.transform;
                float aditionalHeight = newElement.GetComponent<BoxCollider>().size.y;
                newElement.GetComponent<Rigidbody>().isKinematic = true;
                newElement.GetComponent<BoxCollider>().enabled = false;
                newElement.SetParent(transform);
                newElement.GetComponent<Item>().IsComponent = true;
                newElement.localPosition = Vector3.zero + new Vector3(0, transform.GetComponent<BoxCollider>().size.y * 0.5f + transform.GetComponent<BoxCollider>().size.y * 0.5f, 0);
                transform.GetComponent<BoxCollider>().size = new Vector3(transform.GetComponent<BoxCollider>().size.x,transform.GetComponent<BoxCollider>().size.y + aditionalHeight, transform.GetComponent<BoxCollider>().size.z);
                newElement.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
}
