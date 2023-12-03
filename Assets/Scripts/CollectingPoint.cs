using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectingPoint : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _partsCollecting = new List<Transform>();
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ingredient"))
        {
            float firstHeight = 0;
            float height = 0;

            other.transform.GetComponent<Item>().IsDragging = false;

            if (_partsCollecting.Count == 0)
            {
                other.transform.position = transform.position;
            }

            if (_partsCollecting.Count > 0)
            {
                firstHeight = _partsCollecting[_partsCollecting.Count - 1].GetComponent<BoxCollider>().size.y * 0.5f;
                height = firstHeight + other.transform.GetComponent<BoxCollider>().size.y * 0.5f;

                other.transform.position = _partsCollecting[_partsCollecting.Count - 1].position + new Vector3(0, height, 0);
            }

            _partsCollecting.Add(other.transform);
            Destroy(other.transform.GetComponent<Rigidbody>());
            other.transform.GetComponent<BoxCollider>().enabled = false;

        }
    }
}
