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
        var managerPostProcessProfile = GetManager<ManagerPostProcessProfile>();

        var inputComponent = managerDoodle.DoodleController.GetDoodleComponent<InputComponent>();
        var moveComponent = managerDoodle.DoodleController.GetDoodleComponent<MoveComponent>();
        var effectJumpComponent = managerDoodle.DoodleController.GetDoodleComponent<EffectJumpComponent>();
        var jumpingComponent = managerDoodle.DoodleController.GetDoodleComponent<JumpingComponent>();
        var effectShakeComponent = managerDoodle.DoodleController.GetDoodleComponent<EffectShakeComponent>();
        var shotDoodleComponent = managerDoodle.DoodleController.GetDoodleComponent<ShotDoodleComponent>();

        managerEducation.OnEducationEnd.Subscribe(_ => { managerLevel.SetActivePause(false); });

        inputComponent.InputCommand.Subscribe(value =>
        {
            if(jumpingComponent.IsAllowedToSide && managerLevel.IsPause == false)
                moveComponent.Move(value);
        });

        inputComponent.ShootingCommand.Subscribe(_ => { shotDoodleComponent.Shot(); });

        inputComponent.JumpCommand.Subscribe(_ =>
        {
            if (jumpingComponent.IsFlying || managerLevel.IsPause)
                return;

            if (managerPlatform.PlatformController.PreviousSelectedPlatfrom == null || managerPlatform.PlatformController.PreviousSelectedPlatfrom.IsDoodleOnPlatform)
            {
                managerPlatform.PlatformController.ShiftRankPlatforams();
                jumpingComponent.SetTargetPlatform(managerPlatform.PlatformController.NextSelectPlatfrom.transform.position);

                managerPlatform.OutlineNextPlatform();
                managerPlatform.PlatformController.FormationSelectionAllowedPlatform();
                managerPlatform.PlatformController.OutlineSelectionAllowedPlatform();
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

            managerRocket.SetFlagFlying(false);
        });

        jumpingComponent.JumpingOnPlaceCommnad.Subscribe(positionDoodle =>
        {
            moveComponent.EndMoveToPosition();
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

        jumpingComponent.DoodleStartFlyingCommand.Subscribe(doodleTransform =>
        {
            managerAudio.PlayRocket();
            managerPostProcessProfile.StartRocketEffect();
            managerRocket.SetFlagFlying(true);
            managerRocket.StartSmokeEffect(doodleTransform);
        });

        jumpingComponent.DoodleEndFlyingCommand.Subscribe(_ =>
        {
            managerPostProcessProfile.StopRocketEffect();
        });

        managerFramesMap.FramesMapController.OnRespawnFrameMap.Subscribe(frameMap =>
        {
            managerPlatform.PlatformController.NoActiveOldPlatforms(frameMap);
            managerPlatform.PlatformController.RespawnPlatforms(frameMap);
            managerEnemies.EnemyController.NoActiveOldEnemies(frameMap);
            managerEnemies.SpawnEnemy(frameMap.transform);
        });

        managerDoodle.DoodleView.ChangingPosition.Subscribe(zPosition =>
        {
            if (managerDoodle.DoodleView.IsDie == false)
                managerDistance.IncreasingDistance(zPosition);
        });

        managerDoodle.DoodleView.DoodleDieCommand.Subscribe(async _ =>
        {
            managerCamera.ResetFollowCamera();
            jumpingComponent.OnDieDoodle();
            managerAudio.PlayFall();
            managerPlatform.PlatformController.ClearSlectePlatforms();
            effectShakeComponent.SetActive(true);
            managerDistance.SaveResult();
            await UniTask.Run(async () => { await UniTask.Delay(DataSettingsContainer.Instance.Settings.DelayAfterDieDoodle); });
            managerMenu.GameOverView.GameOverCommand.Execute();
        });

        managerEnemies.CreateEnemyCommand.Subscribe(_ => { managerAudio.PlayEnemySound(); });

        managerMenu.PlayingCommand.Subscribe(_ => { managerEducation.SetActive(true); });

        managerMenu.SettingsView.ChangingVolume.Subscribe(volume => { managerAudio.ChangeVolume(volume); });

        managerPlatform.PlatformController.RespawnPlatformCommand.Subscribe(lastPositionPlatform => { managerRocket.SpawnRocket(lastPositionPlatform); });
    }

    private T GetManager<T>() where T : BaseManager
    {
        var managerFinded = _managers.Find(manager => manager is T);
        return (T)managerFinded;
    }
}
