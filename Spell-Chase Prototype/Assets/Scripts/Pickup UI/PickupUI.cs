using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupUI : MonoBehaviour
{
    public static PickupUI Instance;

    [SerializeField] 
    private GameObject pickupUIPanel; // The UI panel

    [SerializeField] 
    private TextMeshProUGUI pickupNameText; // Text for pickup name

    [SerializeField] 
    private TextMeshProUGUI countdownText; // Text for countdown

    [SerializeField] 
    private float popupDuration = 3f; // Display time for non-timed pickups

    [SerializeField] 
    private float popupScaleDelay = 0.1f; // Brief delay before scaling to full size

    [SerializeField] 
    private Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f); // Small size for pop-up

    [SerializeField] 
    private Vector3 endScale = Vector3.one; // Full size after pop-up

    private CanvasGroup canvasGroup; // For transparency

    private Image panelImage; // For changing panel color

    private Coroutine activeCoroutine;

    private readonly Color pointBoosterColor = Color.green;

    private readonly Color immunityColor = Color.red;

    private readonly Color speedColor = Color.blue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        canvasGroup = pickupUIPanel.GetComponent<CanvasGroup>();

        panelImage = pickupUIPanel.GetComponent<Image>();

        pickupUIPanel.SetActive(false);

    }

    // Call from your pickup code with name, type, and duration (0 for non-timed)
    public void ShowPickupUI(string pickupName, string pickupType, float duration = 0f)
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        activeCoroutine = StartCoroutine(PopupCoroutine(pickupName, duration));
    }

    private IEnumerator PopupCoroutine(string pickupName, float duration)
    {
        //reset UI
        pickupUIPanel.SetActive(true);

        canvasGroup.alpha = 1f;

        pickupNameText.text = pickupName;

        countdownText.gameObject.SetActive(duration > 0f);

        pickupUIPanel.transform.localScale = startScale; // Instantly set to small scale

        // Wait briefly, then instantly scale to full size
        yield return new WaitForSeconds(popupScaleDelay);

        pickupUIPanel.transform.localScale = endScale; //set to full scale

        //countdown for speed/immunity pickups 
        if (duration > 0f)
        {
            float timeLeft = duration;

            while (timeLeft > 0f)
            {
                countdownText.text = "Time Left: " + timeLeft + "s";
                timeLeft -= Time.deltaTime;
                yield return null;
            }

            countdownText.text = "Time Left: 0s";
        }
        else
        {
            yield return new WaitForSeconds(popupDuration);
        }

        // Instantly fade out (set transparency to 0)
        canvasGroup.alpha = 0f;

        // Deactivate UI
        pickupUIPanel.SetActive(false);

        activeCoroutine = null;

    }
}
//https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html
//https://docs.unity3d.com/ScriptReference/CanvasGroup.html
