using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("jasper"))
        {
            Invoke("FallDown", 0.2f);
            // Calling FallDown() after 0.2s
        }
    }

    private void FallDown()
    {
        GetComponentInParent<Rigidbody>().useGravity = true;
        GetComponentInParent<Rigidbody>().isKinematic = false;
        Destroy(transform.parent.gameObject, 2f);
    }
}