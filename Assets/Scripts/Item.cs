using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _gravitation;
    private float _initGrav;

    public GameObject CurrentDish;
    public int PositionInDish;
    private Camera _mainCamera;
    private Rigidbody rb;

    private Plane _dragPlane;
    private Vector3 _offset;

    public bool IsDragging = false;

    private void Awake()
    {
        _initGrav = _gravitation;
        rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        CurrentDish = GameObject.Find("CurrentDish");
    }


    void Update()
    {
        if (rb != null)
        {
            if (GameManager.Instance.GravityState == 1)
            {
                rb.useGravity = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 10f, transform.position.z), _gravitation * Time.deltaTime);
            }
            else if(GameManager.Instance.GravityState == 2)
            {
                rb.velocity = Vector3.zero;
            }
            else if (GameManager.Instance.GravityState == 3)
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
        GameManager.Instance.DraggingItem = this;
        if (CurrentDish.GetComponent<CurrentDish>().Counter == 0)
        {
            CurrentDish.GetComponent<CurrentDish>().PrevPosition = PositionInDish;
        }

        Debug.Log("Down");
    }

    void OnMouseDrag()
    {
        if (IsDragging)
        {
            Debug.Log("Draging");
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
            if (collision.collider.CompareTag("Ingredient") && transform.CompareTag("Ingredient"))
            {
                Transform newElement = collision.transform;
           
                if (newElement.GetComponent<Item>().PositionInDish < CurrentDish.GetComponent<CurrentDish>().PrevPosition)
                {
                    CurrentDish.GetComponent<CurrentDish>().Correct = false;
                    Debug.Log("Incorrect");
                }
                CurrentDish.GetComponent<CurrentDish>().PrevPosition = newElement.GetComponent<Item>().PositionInDish;
                CurrentDish.GetComponent<CurrentDish>().Counter++;
  
                CurrentDish.GetComponent<CurrentDish>().Check();

                
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
