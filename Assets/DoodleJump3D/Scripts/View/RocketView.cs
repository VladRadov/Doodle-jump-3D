using UnityEngine;

public class RocketView : MonoBehaviour
{
    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Rocket"))
            SetActive(false);
    }
}
