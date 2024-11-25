using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI darkTimerText;
    public float remainingTime;

    public bool hasWon;
    public bool last30;

    public static Timer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.DecreaseLevelTime();
    }

    void Update()
    {
        if (!hasWon)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            if (remainingTime <= 30 && !last30)
            {
                last30 = true;
                GameManager.Instance.DecreaseLevelTime();
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            darkTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
