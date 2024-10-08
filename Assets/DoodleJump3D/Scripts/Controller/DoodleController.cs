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

    public T GetDoodleComponent<T>() where T : BaseComponent
    {
        var componentFinded = _doodleView.Components.Find(component => component is T);
        return (T)componentFinded;
    }
}
