using UnityEngine;

public class BluePlatformView : PlatformView
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
            SetActive(false);

        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            OnCollisionTexture.Execute(this);
    }
}
