using UnityEngine;

[CreateAssetMenu(fileName = "BaseGameData", menuName = "ScriptableObject/BaseGameData")]
public class BaseGameData : ScriptableObject
{
    [SerializeField] private bool _isEducationEnd;
    [SerializeField] private bool _isCatSceneView;

    public virtual int CurrentResult { get; set; }
    public virtual int BestResult { get; set; }
    public virtual bool IsEducationEnd => _isEducationEnd;
    public virtual bool IsCatSceneView => _isCatSceneView;

    public void EndEducation()
        => _isEducationEnd = true;

    public void EndCatSceneView()
        => _isCatSceneView = false;

    public void CatSceneView()
        => _isCatSceneView = true;
}
