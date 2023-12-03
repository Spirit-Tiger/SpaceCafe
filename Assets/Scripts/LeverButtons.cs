using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverButtons : MonoBehaviour
{
    public int ButtonId;
    public Animator Animator;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(ButtonId == 1)
            {
                SwitchToBottom();
            }
            else if (ButtonId == 2)
            {
                SwitchToMiddle();
            }
            else if(ButtonId == 3)
            {
                SwitchToTop();
            }
        }
    }
    public void SwitchToTop()
    {
        Animator.SetTrigger("L_3");
        GameManager.Instance.GravityState = 3;
    }
    public void SwitchToMiddle()
    {
        Animator.SetTrigger("L_2");
        GameManager.Instance.GravityState = 2;
    }
    public void SwitchToBottom()
    {
        Animator.SetTrigger("L_1");
        GameManager.Instance.GravityState = 1;
    }
}
