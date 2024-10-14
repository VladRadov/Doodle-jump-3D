using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleComponent : BaseComponent
{
    [SerializeField] private float _duration;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Doodle"))
            gameObject.SetActive(false);
    }
}
