using UnityEngine;
using UnityEngine.UI;

public class BonusRewardManager : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] Text progressCount;
    [SerializeField] Button openBtn;

    private void OnEnable()
    {
        UpdateBarProgressUI();
    }

    internal void UpdateBarProgressUI()
    {
        progressBar.fillAmount = PlayerPrefs.GetFloat("bar progress", 0f);
        progressCount.text = (progressBar.fillAmount*10).ToString();
        if(progressBar.fillAmount >= 1f)
        {
            openBtn.interactable = true;
        }
    }

    public void OpenBonusReward()
    {
        openBtn.interactable = false;
        //resetting the progress bar
        progressBar.fillAmount = 0f;
        progressCount.text = "0";
        PlayerPrefs.SetFloat("bar progress", 0f);
    }
}
