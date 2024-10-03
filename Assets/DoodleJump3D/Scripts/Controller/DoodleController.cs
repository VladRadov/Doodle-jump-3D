using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DoodleController
{
    private DoodleView _doodleView;
    private Doodle _doodle;

    public DoodleView Doodle => _doodleView;

    public DoodleController(DoodleView doodleView, Doodle doodle)
    {
        _doodleView = doodleView;
        _doodle = doodle;
    }

    public void Initialize()
    {
        InputComponent inputComponent = GetDoodleComponent<InputComponent>();
        MoveComponent moveComponent = GetDoodleComponent<MoveComponent>();
        JumpingComponent jumpingComponent = GetDoodleComponent<JumpingComponent>();
        EffectJumpComponent effectJumpComponent = GetDoodleComponent<EffectJumpComponent>();

        inputComponent.InputCommand.Subscribe(value => { moveComponent.Move(value); });
        jumpingComponent.JumpingOnPlaceCommnad.Subscribe(positionDoodle => { effectJumpComponent.Create(positionDoodle); });
    }

    public T GetDoodleComponent<T>() where T : BaseComponent
    {
        var componentFinded = _doodleView.Components.Find(component => component is T);
        return (T)componentFinded;
    }
}
