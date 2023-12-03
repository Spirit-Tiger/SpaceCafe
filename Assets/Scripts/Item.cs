using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _gravitation;

    public GameObject CurrentDish;
    public int PositionInDish;
    private Camera _mainCamera;
    private Rigidbody rb;

    private Plane _dragPlane;
    private Vector3 _offset;

    public bool IsDragging = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        CurrentDish = GameObject.Find("CurrentDish");
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

        if (CurrentDish.GetComponent<CurrentDish>().PrevPosition == 0)
        {
            CurrentDish.GetComponent<CurrentDish>().PrevPosition = PositionInDish;
        }
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
        if (IsDragging)
        {
            if (collision.collider.CompareTag("Ingredient"))
            {
           
                if(CurrentDish.GetComponent<CurrentDish>().PrevPosition < PositionInDish)
                {
                    CurrentDish.GetComponent<CurrentDish>().Correct = false;
                }
                CurrentDish.GetComponent<CurrentDish>().Counter++;
                CurrentDish.GetComponent<CurrentDish>().Check();

                Transform newElement = collision.transform;
                float aditionalHeight = newElement.GetComponent<BoxCollider>().size.y;
                newElement.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(newElement.GetComponent<Rigidbody>());
               newElement.GetComponent<BoxCollider>().enabled = false;

                newElement.SetParent(transform);
                newElement.localPosition = Vector3.zero + new Vector3(0, transform.GetComponent<BoxCollider>().size.y * 0.5f + transform.GetComponent<BoxCollider>().size.y * 0.5f, 0);

                transform.GetComponent<BoxCollider>().size = new Vector3(transform.GetComponent<BoxCollider>().size.x,transform.GetComponent<BoxCollider>().size.y + aditionalHeight, transform.GetComponent<BoxCollider>().size.z);
                newElement.localRotation = Quaternion.Euler(0f, 0f, 0f);
               
            }
        }
    }
}
