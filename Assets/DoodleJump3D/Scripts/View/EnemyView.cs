using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public void SetLocalPosition(Vector3 position)
        => transform.localPosition = position;

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
