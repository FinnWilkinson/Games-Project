using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRateDisplay : MonoBehaviour
{
    TextMesh text = null;
    AudioSource audioSource = null;
    private int currentHeartRate;
    private float heartSoundRate;
    public int baseHeartRate;

    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TextMesh>();
        audioSource = this.GetComponent<AudioSource>();
        baseHeartRate = 70;
        currentHeartRate = 70;
        StartCoroutine(HeartRateChange());
    }


    private IEnumerator HeartRateChange()
    {
        while (true) 
        {
            yield return new WaitForSecondsRealtime(1);
            currentHeartRate = Random.Range(baseHeartRate - 2, baseHeartRate + 2);
            text.text = currentHeartRate.ToString();
            heartSoundRate = 60.0f / currentHeartRate;
            audioSource.Play();
        }
    }

}
