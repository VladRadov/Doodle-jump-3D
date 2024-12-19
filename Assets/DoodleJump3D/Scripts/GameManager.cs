using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

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
        var managerEnemies = GetManager<ManagerEnemies>();
        var managerDistance = GetManager<ManagerDistance>();
        var managerCamera = GetManager<ManagerCamera>();
        var managerAudio = GetManager<ManagerAudio>();
        var managerMenu = GetManager<ManagerMenu>();
        var managerRocket = GetManager<ManagerRocket>();

        var inputComponent = managerDoodle.DoodleController.GetDoodleComponent<InputComponent>();
        var moveComponent = managerDoodle.DoodleController.GetDoodleComponent<MoveComponent>();
        var effectJumpComponent = managerDoodle.DoodleController.GetDoodleComponent<EffectJumpComponent>();
        var jumpingComponent = managerDoodle.DoodleController.GetDoodleComponent<JumpingComponent>();
        var effectShakeComponent = managerDoodle.DoodleController.GetDoodleComponent<EffectShakeComponent>();
        var shotDoodleComponent = managerDoodle.DoodleController.GetDoodleComponent<ShotDoodleComponent>();

        managerEducation.OnEducationEnd.Subscribe(_ => { managerLevel.SetActivePause(false); });

        inputComponent.InputCommand.Subscribe(value =>
        {
            if(managerLevel.IsPause == false)
                moveComponent.Move(value);
        });

        inputComponent.ShootingCommand.Subscribe(_ => { shotDoodleComponent.Shot(); });

        inputComponent.JumpCommand.Subscribe(_ =>
        {
            if (managerLevel.IsPause == false)
            {
                if (managerPlatform.PlatformController.PreviousSelectedPlatfrom == null || managerPlatform.PlatformController.PreviousSelectedPlatfrom.IsDoodleOnPlatform)
                {
                    managerPlatform.PlatformController.ShiftRankPlatforams();
                    jumpingComponent.SetTargetPlatform(managerPlatform.PlatformController.NextSelectPlatfrom.transform.position);
                    managerPlatform.OutlineNextPlatform();
                    managerPlatform.PlatformController.FormationSelectionAllowedPlatform();
                    managerPlatform.PlatformController.OutlineSelectionAllowedPlatform();
                }
            }
        });

        jumpingComponent.DoodleEndFlyingCommand.Subscribe(doodlePosition =>
        {
            var findedNearPlatformFromDoodle = managerPlatform.PlatformController.FindNearPlatformFromDoodle(doodlePosition);
            managerPlatform.PlatformController.SetCurrentSelectPlatfrom(findedNearPlatformFromDoodle);
            moveComponent.MoveToPosition(findedNearPlatformFromDoodle.transform.position);
            managerPlatform.OutlineNextPlatform();
            managerPlatform.PlatformController.FormationSelectionAllowedPlatform();
            managerPlatform.PlatformController.OutlineSelectionAllowedPlatform();
        });

        jumpingComponent.JumpingOnPlaceCommnad.Subscribe(positionDoodle =>
        {
            effectJumpComponent.Create(positionDoodle);
            managerCamera.ShakeCamera.Shake();
            managerAudio.PlayJump();
        });

        jumpingComponent.JumpingOnForwardWithRotationCommnad.Subscribe(positionDoodle =>
        {
            managerDoodle.DoodleAnimator.PlayRotation();
            managerFramesMap.FramesMapController.CheckAndRespawnFramesMap(positionDoodle);
        });

        jumpingComponent.FlyingCommand.Subscribe(positionDoodle => { managerFramesMap.FramesMapController.CheckAndRespawnFramesMap(positionDoodle); });

        jumpingComponent.DoodleStartFlyingCommand.Subscribe(_ => { managerAudio.PlayRocket(); });

        managerFramesMap.FramesMapController.OnRespawnFrameMap.Subscribe(frameMap =>
        {
            managerPlatform.PlatformController.NoActiveOldPlatforms(frameMap);
            managerPlatform.PlatformController.RespawnPlatforms(frameMap);
            managerEnemies.EnemyController.NoActiveOldEnemies(frameMap);
            managerEnemies.SpawnEnemy(frameMap.transform);
            managerRocket.SpawnRocket(frameMap.transform);
        });

        managerDoodle.DoodleView.ChangingPosition.Subscribe(zPosition => { managerDistance.IncreasingDistance(zPosition); });

        managerDoodle.DoodleView.DoodleDieCommand.Subscribe(async _ =>
        {
            managerAudio.PlayFall();
            effectShakeComponent.SetActive(true);
            await UniTask.Run(async () => { await UniTask.Delay(DataSettingsContainer.Instance.Settings.DelayAfterDieDoodle); });
            managerMenu.GameOverView.SetActive(true);
        });

        managerEnemies.CreateEnemyCommand.Subscribe(_ => { managerAudio.PlayEnemySound(); });

        managerMenu.PlayingCommand.Subscribe(_ => { managerEducation.SetActive(true); });

        managerMenu.SettingsView.ChangingVolume.Subscribe(volume => { managerAudio.ChangeVolume(volume); });
    }

    private T GetManager<T>() where T : BaseManager
    {
        var managerFinded = _managers.Find(manager => manager is T);
        return (T)managerFinded;
    }
}
