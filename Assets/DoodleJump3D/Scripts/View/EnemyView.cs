using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }
}
