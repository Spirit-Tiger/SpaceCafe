using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator Animator;
    public float yPos;
    public int CurrentPosition = 1;
    private Plane _dragPlane;
    private Vector3 _offset;
    private void Awake()
    {
        //Animator.SetTrigger("L_2");
    }
    private void OnMouseDown()
    {
        yPos = transform.position.y;

        _dragPlane = new Plane(Camera.main.transform.forward, transform.position);
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);
        _offset = transform.position - camRay.GetPoint(planeDist);

        Debug.Log(yPos);
    }

    private void OnMouseDrag()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);
        Vector3 pos;
        pos = camRay.GetPoint(planeDist) + _offset;
        Debug.Log(pos);
        if (pos.y >= 2.25f && CurrentPosition == 1)
        {
            Animator.SetTrigger("L_2");
            CurrentPosition = 2;
        }

        if (pos.y >= 2.9f && CurrentPosition == 2)
        {
            Animator.SetTrigger("L_3");
            CurrentPosition = 3;
        }

        if (pos.y <= 2.35f && CurrentPosition == 3)
        {
            Animator.SetTrigger("L_2");
            CurrentPosition = 2;
        }

        if (pos.y <= 1.85f && CurrentPosition == 2)
        {
            Animator.SetTrigger("L_1");
            CurrentPosition = 1;
        }



    }
}
