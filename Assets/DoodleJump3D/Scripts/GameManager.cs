using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<BaseManager> _managers;

    private void Start()
    {
        foreach (var manager in _managers)
            manager.Initialize();

        var managerPlatform = GetManager<ManagerPlatform>();
        var managerDoodle = GetManager<ManagerDoodle>();
        var managerFramesMap = GetManager<ManagerFramesMap>();

        var inputComponent = managerDoodle.DoodleController.GetDoodleComponent<InputComponent>();
        var jumpingComponent = managerDoodle.DoodleController.GetDoodleComponent<JumpingComponent>();
        inputComponent.JumpCommand.Subscribe(_ =>
        {          
            jumpingComponent.SetTargetPlatform(managerPlatform.PlatformController.CurrentSelectPlatfrom.transform.position);
            managerPlatform.OutlineCurrentPlatform();
            managerPlatform.PlatformController.FormationSelectionAllowedPlatform(); 
        });

        jumpingComponent.JumpingOnForwardCommnad.Subscribe(_ => { managerDoodle.DoodleAnimator.PlayRotation(); });
        managerDoodle.DoodleController.Doodle.OnChangePositionDoodle.Subscribe(positionDoodle => { managerFramesMap.FramesMapController.CheckAndRespawnFramesMap(positionDoodle); });
    }

    private T GetManager<T>() where T : BaseManager
    {
        var managerFinded = _managers.Find(manager => manager is T);
        return (T)managerFinded;
    }
}
