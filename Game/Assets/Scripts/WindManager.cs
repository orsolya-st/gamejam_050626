using UnityEngine;
using System.Collections;

public class WindManager : MonoBehaviour
{
    public static WindManager Instance;

    public float windStrength = 5f;
    public float windDuration = 10f;

    public float minTimeBetweenWind = 5f;
    public float maxTimeBetweenWind = 20f;

    public float CurrentWindForce { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(WindRoutine());
    }

    IEnumerator WindRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(
                minTimeBetweenWind,
                maxTimeBetweenWind
            );

            yield return new WaitForSeconds(waitTime);

            CurrentWindForce =
                Random.value < 0.5f
                ? -windStrength
                : windStrength;

            yield return new WaitForSeconds(windDuration);

            CurrentWindForce = 0f;
        }
    }
}