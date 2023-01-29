using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _menu;

    private bool _isShow = false;

    private void Start()
    {
        _menu.gameObject.SetActive(false);
        PlayerInputHandler.InputActions.Actions.OpenMenu.performed += ctx => Show();
    }

    public void Show()
    {
        _isShow = !_isShow; 
        _menu.gameObject.SetActive(_isShow);
    }
}
