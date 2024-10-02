using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleComponent : BaseComponent
{
    [SerializeField] private float _duration;

    private void OnCollisionEnter(Collision collision)
    {
        var doodle = collision.gameObject.GetComponent<DoodleView>();

        if (doodle != null)
            gameObject.SetActive(false);
    }
}
