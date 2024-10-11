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
        var managerEducation = GetManager<ManagerEducation>();
        var managerLevel = GetManager<ManagerLevel>();

        var inputComponent = managerDoodle.DoodleController.GetDoodleComponent<InputComponent>();
        var moveComponent = managerDoodle.DoodleController.GetDoodleComponent<MoveComponent>();
        var effectJumpComponent = managerDoodle.DoodleController.GetDoodleComponent<EffectJumpComponent>();
        var jumpingComponent = managerDoodle.DoodleController.GetDoodleComponent<JumpingComponent>();

        managerEducation.OnEducationEnd.Subscribe(_ => { managerLevel.SetActivePause(false); });

        inputComponent.InputCommand.Subscribe(value =>
        {
            if(managerLevel.IsPause == false)
                moveComponent.Move(value);
        });

        inputComponent.JumpCommand.Subscribe(_ =>
        {
            if (managerLevel.IsPause == false)
            {
                jumpingComponent.SetTargetPlatform(managerPlatform.PlatformController.CurrentSelectPlatfrom.transform.position);
                managerPlatform.OutlineCurrentPlatform();
                managerPlatform.PlatformController.FormationSelectionAllowedPlatform();
            }
        });

        jumpingComponent.JumpingOnPlaceCommnad.Subscribe(positionDoodle => { effectJumpComponent.Create(positionDoodle); });

        jumpingComponent.JumpingOnForwardCommnad.Subscribe(positionDoodle =>
        {
            managerDoodle.DoodleAnimator.PlayRotation();
            managerFramesMap.FramesMapController.CheckAndRespawnFramesMap(positionDoodle);
        });

        managerFramesMap.FramesMapController.OnRespawnFrameMap.Subscribe(frameMap =>
        {
            managerPlatform.PlatformController.NoActiveOldPlatforms(frameMap);
            managerPlatform.PlatformController.RespawnPlatforms(frameMap);
        });
    }

    public void OnEducationEnd()
    {
        
    }

    private T GetManager<T>() where T : BaseManager
    {
        var managerFinded = _managers.Find(manager => manager is T);
        return (T)managerFinded;
    }
}
