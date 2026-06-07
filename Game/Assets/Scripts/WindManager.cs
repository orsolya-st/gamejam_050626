using UnityEngine;
using System.Collections;

public class WindManager : MonoBehaviour
{
    public static WindManager Instance;

    public GameObject[] windEffects;

    public float windStrength = 5f;
    public float windDuration = 10f;

    public float minTimeBetweenWind = 5f;
    public float maxTimeBetweenWind = 20f;

    public float CurrentWindForce { get; private set; }

    private bool windStarted = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach (GameObject effect in windEffects)
        {
            effect.SetActive(false);
        }
    }

    void Update()
    {
        if (!windStarted && Input.anyKeyDown)
        {
            windStarted = true;
            StartCoroutine(WindRoutine());
        }
    }

    IEnumerator WindRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenWind, maxTimeBetweenWind);
            yield return new WaitForSeconds(waitTime);

            bool windGoesLeft = Random.value < 0.5f;

            CurrentWindForce = windGoesLeft ? -windStrength : windStrength;

            foreach (GameObject effect in windEffects)
            {
                effect.transform.localScale = windGoesLeft
                    ? new Vector3(1, 1, 1)
                    : new Vector3(-1, 1, 1);

                effect.SetActive(true);
            }

            yield return new WaitForSeconds(windDuration);

            CurrentWindForce = 0f;

            foreach (GameObject effect in windEffects)
            {
                effect.SetActive(false);
            }
        }
    }
}