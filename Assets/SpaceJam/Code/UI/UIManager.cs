using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image reloadCircle;

    private void Start()
    {
        if (reloadCircle == null) return;
        reloadCircle.gameObject.SetActive(false);
        reloadCircle.fillAmount = 0;
    }


    public void ShowReloadIndicator(bool show)
    {
        if (reloadCircle != null) reloadCircle.gameObject.SetActive(show);
    }

    public void UpdateReloadIndicator(float progress)
    {
        if (reloadCircle != null) reloadCircle.fillAmount = progress;
    }
}
