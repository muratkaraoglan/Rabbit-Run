using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Instance { get { return instance; } }

    public Transform rabbit;
    public Transform feverRabbit;

    public Image feverBar;

    public Button playButton;
    public Button retryButton;
    public int carrotCount;
    private void Awake()
    {
        if ( instance != null && instance != this )
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        playButton.onClick.AddListener(OnPlayButtonClick);
        retryButton.onClick.AddListener(OnRetryButtonClick);
    }
    
    void OnPlayButtonClick() 
    {
        playButton.gameObject.SetActive(false);
        rabbit.gameObject.SetActive(true);
    }
    
    void OnRetryButtonClick()
    {
        SceneManager.LoadScene(0);
        OnPlayButtonClick();
    }

    public void Retry()
    {
        retryButton.gameObject.SetActive(true);
    }


    public void SetFeverBar(int count)
    {
        float amount = count / 5f;
        carrotCount++;
        feverBar.fillAmount = amount;
        feverBar.color = Remap(10, 100, Color.yellow, new Color32(255, 69, 0, 255), (amount * 100));

        if ( count % 5 == 0 )
        {
            StartCoroutine(FeverMode());
        }
    }

    Color Remap(float iMin, float iMax, Color oMin, Color oMax, float barValue)
    {
        float t = Mathf.InverseLerp(iMin, iMax, barValue);
        return Color.Lerp(oMin, oMax, t);
    }

    IEnumerator FeverMode()
    {
        //change rabbit to fever rabbit
        Vector3 position = rabbit.transform.position;
        rabbit.gameObject.SetActive(false);
        feverRabbit.gameObject.SetActive(true);
        feverRabbit.position = position;

        yield return new WaitForSeconds(7f);

        Vector3 feverPos = feverRabbit.transform.position;
        feverRabbit.gameObject.SetActive(false);
        rabbit.gameObject.SetActive(true);
        rabbit.position = feverPos;

    }
}
