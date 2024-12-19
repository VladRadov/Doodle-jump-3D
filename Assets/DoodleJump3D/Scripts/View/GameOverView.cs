using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Text _currentResult;
    [SerializeField] private Text _bestResult;
    [SerializeField] private Button _buttonMenu;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _buttonMenu.onClick.AddListener(() => { ManagerScenes.Instance.LoadAsyncFromCoroutine("Game"); });
    }

    private void OnEnable()
    {
        _currentResult.text = GameDataContainer.Instance.GameData.CurrentResult.ToString();
        _bestResult.text = GameDataContainer.Instance.GameData.BestResult.ToString();
    }
}
