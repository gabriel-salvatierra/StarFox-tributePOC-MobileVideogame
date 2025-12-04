using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    private const string BannerAdID = "Banner_Android";

    private void Awake()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions options = new BannerLoadOptions()
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadedError
        };
        Advertisement.Banner.Load(BannerAdID, options);
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions()
        {
            showCallback = BannerShown,
            clickCallback = BannerClicked,
            hideCallback = BannerHidden
        };
        Advertisement.Banner.Show(BannerAdID, options);
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    private void BannerLoaded() { }
    private void BannerLoadedError(string error) { }
    private void BannerShown() { }
    private void BannerClicked() { }
    private void BannerHidden() { }
}
