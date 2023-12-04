using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordWarning : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisableWarning());
    }

    private IEnumerator DisableWarning()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);    
    }
}
