using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] Button _adButton;

    [SerializeField] private int _rewardedGems = 100;
    [SerializeField] private string _gameID;
    public string pID = "Rewarded_Android";
    private Player _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _adButton.interactable = Advertisement.IsReady(pID);
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameID, true);

        if (_player == null)
            Debug.LogError("Player Script is Null");
    }

    public void ShowRewardAd()
    {
        Advertisement.Show(pID);
    }

    void IUnityAdsListener.OnUnityAdsReady(string placementId)
    {
        if (placementId == pID)
            _adButton.interactable = true;
    }

    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                Debug.LogError("Ad Errored");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad Skipped, No Reward");
                break;
            case ShowResult.Finished:
                _player.Score(_rewardedGems);
                UIManager.Instance.UpdatePlayerGemCount(_player.Gems());
                break;
            default:
                break;
        }
    }

    void IUnityAdsListener.OnUnityAdsDidError(string message)
    {
        Debug.LogError("Ad Errored!");
    }

    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    {
        // not implemented
    }
}
