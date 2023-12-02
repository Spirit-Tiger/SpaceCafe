using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingPoint : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _partsCollecting = new List<Transform>();
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ingredient"))
        {
            Debug.Log("Trigger1");
            _partsCollecting.Add(other.transform);
            other.transform.GetComponent<Item>().IsDragging = false;
            float firstHeight = 0;
            if (_partsCollecting[_partsCollecting.Count - 1] != null)
            {
                firstHeight = _partsCollecting[_partsCollecting.Count - 1].GetComponent<BoxCollider>().size.y;
            }

            float height = firstHeight + other.transform.GetComponent<BoxCollider>().size.y;

            if (_partsCollecting.Count == 1)
            {
                height = 0;
                other.transform.position = transform.position;
            }

            if (_partsCollecting.Count > 1)
            {
                other.transform.position = _partsCollecting[_partsCollecting.Count - 2].position + new Vector3(0, height * 0.5f, 0);
            }

            Destroy(other.transform.GetComponent<Rigidbody>());
            other.transform.GetComponent<BoxCollider>().enabled = false;
          
           
        }
    }
}
