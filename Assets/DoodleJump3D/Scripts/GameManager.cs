using System.Collections.Generic;
using UnityEngine;

using UniRx;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<BaseManager> _managers;
    [SerializeField] private DataSettingsContainer _dataSettingsContainer;
    [SerializeField] private GameDataContainer _gameDataContainer;

    private void Awake()
    {
        _dataSettingsContainer.Initialize();
        _gameDataContainer.Initialize();

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
        var managerAchievements = GetManager<ManagerAchievements>();
        var managerStars = GetManager<ManagerStars>();
        var managerTimeline = GetManager<ManagerTimeline>();
        var managerYandexSDK = GetManager<ManagerYandexSDK>();

        var inputComponent = managerDoodle.DoodleController.GetDoodleComponent<InputComponent>();
        var moveComponent = managerDoodle.DoodleController.GetDoodleComponent<MoveComponent>();
        var effectJumpComponent = managerDoodle.DoodleController.GetDoodleComponent<EffectJumpComponent>();
        var jumpingComponent = managerDoodle.DoodleController.GetDoodleComponent<JumpingComponent>();
        var effectShakeComponent = managerDoodle.DoodleController.GetDoodleComponent<EffectShakeComponent>();
        var shotDoodleComponent = managerDoodle.DoodleController.GetDoodleComponent<ShotDoodleComponent>();
        var rotateComponent = managerDoodle.DoodleController.GetDoodleComponent<RotateComponent>();
        var doodleAnimator = managerDoodle.DoodleController.GetDoodleComponent<DoodleAnimator>();
        var changeSideComponent = managerDoodle.DoodleController.GetDoodleComponent<ChangeSideComponent>();

        managerYandexSDK.InitializeSdkSuccess.Subscribe(_ =>
        {
            managerDoodle.DoodleView.RunCatScene();
        });

        managerEducation.OnEducationEnd.Subscribe(_ =>
        {
            SetBlockCursor(true);

            managerLevel.SetActivePause(false);
            managerAchievements.ShowAchivements();
        });

        inputComponent.InputCommand.Subscribe(value =>
        {
            if (jumpingComponent.IsAllowedToSide && managerLevel.IsPause == false)
                moveComponent.Move(value);
        });

        inputComponent.ShootingCommand.Subscribe(_ =>
        {
            if (jumpingComponent.IsFlying || managerLevel.IsPause)
                return;

            shotDoodleComponent.Shot();
        });

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

        jumpingComponent.DoodleStartFlyingCommand.Subscribe(rocket =>
        {
            managerPlatform.PlatformController.ResetPreviousSelectedPlatfrom();
            managerAchievements.Controller.FlyRocketCommand.Execute();
            doodleAnimator.SetActiveAnimator(false);
            rotateComponent.RotateFlyingToRocket();
            managerAudio.PlaySoundRocket();
            managerPostProcessProfile.StartRocketEffect();
            managerRocket.Controller.SetCurrentRocket(rocket);
            managerRocket.Controller.SetFlagFlying(true);
            managerRocket.Controller.StartSmokeEffect(rocket.transform);
        });

        jumpingComponent.FlyingCommand.Subscribe(positionDoodle =>
        {
            managerFramesMap.FramesMapController.CheckAndRespawnFramesMap(positionDoodle);
        });

        jumpingComponent.DoodleEndFlyingCommand.Subscribe(doodlePosition =>
        {
            rotateComponent.ResetRotate();

            var findedNearPlatformFromDoodle = managerPlatform.PlatformController.FindNearPlatformFromDoodle(doodlePosition);
            managerPlatform.PlatformController.SetCurrentSelectPlatfrom(findedNearPlatformFromDoodle);
            moveComponent.MoveToPosition(findedNearPlatformFromDoodle.transform.position);

            managerPlatform.OutlineNextPlatform();
            managerPlatform.PlatformController.FormationSelectionAllowedPlatform();
            managerPlatform.PlatformController.OutlineSelectionAllowedPlatform();

            managerRocket.Controller.SetFlagFlying(false);
            doodleAnimator.SetActiveAnimator(true);

            managerPostProcessProfile.StopRocketEffect();
            managerRocket.Controller.NoActiveCurrentRocket();
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
            managerAchievements.Controller.RotateDoodleCommand.Execute();
            managerDoodle.DoodleAnimator.PlayRotation();
            managerFramesMap.FramesMapController.CheckAndRespawnFramesMap(positionDoodle);
            managerAudio.PlayRotateDoodle();
        });

        jumpingComponent.JumpingOnForwardCommnad.Subscribe(_ =>
        {
            managerAchievements.Controller.JumpPlatformCommand.Execute();
        });

        managerFramesMap.FramesMapController.OnRespawnFrameMap.Subscribe(frameMap =>
        {
            managerPlatform.PlatformController.NoActiveOldPlatforms(frameMap);
            managerPlatform.PlatformController.RespawnPlatforms(frameMap);
            managerEnemies.EnemyController.NoActiveOldEnemies(frameMap);
            managerEnemies.SpawnEnemy(frameMap.transform);
            managerRocket.Controller.ClearNoActiveRockets();
        });

        managerEnemies.DieEnemyCommand.Subscribe(_ =>
        {
            managerAchievements.Controller.KillEnemyCommand.Execute();
            managerAudio.PlayEnemyDieSound();
        });

        managerDoodle.DoodleView.DoodleDieCommand.Subscribe(async _ =>
        {
            SetBlockCursor(false);

            if (jumpingComponent.IsFlying)
            {
                managerRocket.Controller.NoActiveCurrentRocket();
                managerAudio.StopSoundRocket();
            }

            managerDoodle.DoodleView.ActiveTriggerCollider();
            managerCamera.ResetFollowCamera();
            jumpingComponent.OnDieDoodle();
            managerAudio.PlayFall();
            managerPlatform.PlatformController.ClearSlectePlatforms();
            effectShakeComponent.SetActive(true);
            managerDistance.SaveResult();

            await UniTask.Delay(DataSettingsContainer.Instance.Settings.DelayAfterDieDoodle);

            managerMenu.GameOverView.GameOverCommand.Execute();
            managerYandexSDK.FullscreenAdsShow();
        });

        managerEnemies.CreateEnemyCommand.Subscribe(_ => { managerAudio.PlayEnemySound(); });

        managerMenu.PlayingCommand.Subscribe(_ =>
        {
            if (GameDataContainer.Instance.GameData.IsEducationEnd == false)
                managerEducation.SetActive(true);
            else
            {
                SetBlockCursor(true);

                managerLevel.SetActivePause(false);
                managerAchievements.ShowAchivements();
            }

            managerAudio.PlaySoundStartGame();
        });

        managerDoodle.DoodleView.GetStarCommand.Subscribe(_ =>
        {
            managerAchievements.Controller.TakeTheStarCommand.Execute();
            managerAudio.PlaySoundGetStar();
        });

        managerDoodle.DoodleView.SplineAnimateStartCommand.Subscribe(transformDoodle =>
        {
            managerAudio.PlaySoundChangePanelMenu();
            managerRocket.Controller.SetFlagFlying(true);
            managerRocket.Controller.StartSmokeEffect(transformDoodle);
            managerAudio.PlaySoundRocket();
        });

        managerDoodle.DoodleView.SplineAnimateEndCommand.Subscribe(_ =>
        {
            GameDataContainer.Instance.GameData.EndCatSceneView();
            managerDoodle.DoodleView.OnSplineAnimateEnd();
            managerDoodle.DoodleView.SetPositionAfterCatScene();
            jumpingComponent.enabled = true;
            managerRocket.Controller.SetFlagFlying(false);
            managerTimeline.SetActiveTimelinePlayableDirector(true);
            managerMenu.SetActiveMenuPanel(true);

            managerDoodle.DoodleView.ChangingPosition.Subscribe(zPosition =>
            {
                if (managerDoodle.DoodleView.IsDie == false)
                {
                    managerDistance.IncreasingDistance(zPosition);
                    managerAchievements.Controller.DistanceCompletedCommand.Execute();
                }
            });
        });

        managerMenu.SettingsView.ChangingVolume.Subscribe(volume =>
        {
            managerAudio.ChangeVolume(volume);
        });

        managerPlatform.PlatformController.RespawnPlatformEventHandler.AddListener((positionPlatform, indePlatform) =>
        {
            managerRocket.Controller.SpawnRocket(positionPlatform, indePlatform);
            managerStars.SpawStar(positionPlatform, indePlatform);
        });

        managerPlatform.PlatformController.StartRespawnPlatformCommand.Subscribe(countStartPlatform =>
        {
            managerRocket.Controller.GetRandomIndexPlatformForSpawnRocket(countStartPlatform);
            managerStars.GetRandomIndexPlatformForSpawnRocket(countStartPlatform);
        });

        managerPlatform.PlatformController.ExplodingPlatformCommand.Subscribe(_ =>
        {
            managerAudio.PlayExplodingPlatform();
        });

        managerPlatform.PlatformController.FallPlatformCommand.Subscribe(_ =>
        {
            managerAudio.PlaySoundFallPlatform();
        });

        managerPlatform.PlatformController.HidePlatformCommand.Subscribe(_ =>
        {
            managerAudio.PlaySoundWhitePlatformHide();
        });

        shotDoodleComponent.ShotingCommand.Subscribe(_ =>
        {
            managerAudio.PlayShotDoodle();
        });

        managerMenu.ChangePanelCommand.Subscribe(_ =>
        {
            managerAudio.PlaySoundChangePanelMenu();
        });

        managerEducation.ChangePanelCommand.Subscribe(_ =>
        {
            managerAudio.PlaySoundChangePanelMenu();
        });

        managerAchievements.Controller.ChangePanelCommand.Subscribe(_ =>
        {
            managerAudio.PlaySoundChangePanelMenu();
        });

        managerDistance.ChangedBestDistance.Subscribe(distance =>
        {
            managerYandexSDK.SaveBestScore(distance);
        });

        managerYandexSDK.OpenningFullAdCommand.Subscribe(_ =>
        {
            managerAudio.StopAllPlayers();
        });

        changeSideComponent.ChangeSideCommand.Subscribe(newPosition =>
        {
            managerRocket.Controller.SetPositionCurrentRocket(newPosition);
        });
    }

    private T GetManager<T>() where T : BaseManager
    {
        var managerFinded = _managers.Find(manager => manager is T);
        return (T)managerFinded;
    }

    private void SetBlockCursor(bool isBlock)
    {
        Cursor.visible = !isBlock;
        Cursor.lockState = isBlock ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
