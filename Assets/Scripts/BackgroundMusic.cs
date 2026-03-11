using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private float minPitch = 1.0f;
    [SerializeField] private float maxPitch = 1.5f;
    [SerializeField] private int minScore = 500;
    [SerializeField] private int maxScore = 1000;

    private AudioSource audioSource;
    private GameManager gameManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        float t = Mathf.Clamp01((float)(gameManager.score - minScore) / (maxScore - minScore));
        audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, t);
    }
}
