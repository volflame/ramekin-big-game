using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour
{

    // This variable is marked as `static`, which means it is
    // accessible by any script, anywhere! They can just write
    // Clickable.Clicks to read/write to this value.
    //
    // See an example in `ClickTracker.cs`, which is on a UI
    // object titled `Tracker Text (TMP)`.
    public static int Clicks = 0;
    public static int Upgrades = 0;
    public static int UpgradeCost = 20;
    public AudioClip upgradeAudio;
    public AudioClip audioClip;
    private int clickPower = 1;
    public Button upgradeButton;
    public GameObject catFood;
    public GameObject catTree;
    public GameObject yellowBowl;
    public GameObject blueBowl;
    public GameObject purpleBowl;
    float minX = -5f;
    float maxX = 5f;
    float minY = -5f;
    float maxY = 0f;
    GameObject[] cats;
    public GameObject popUpText;
    public Canvas canvas;
    AudioSource audioSource;

    private void Awake()
    {
        cats = GameObject.FindGameObjectsWithTag("cat");
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// This function is called when the mouse button clicks
    /// on this object.
    /// </summary>
    private void OnMouseDown()
    {
        Clicks += clickPower;  // add one point
        AudioSource.PlayClipAtPoint(audioClip, new Vector3(0, 0, -5), 1.0f);
        Vector3 spawnPos = Input.mousePosition;
        spawnPos += new Vector3(Random.Range(-40f, 40f), Random.Range(-10f, 10f), 0);

        GameObject obj = Instantiate(popUpText, spawnPos, Quaternion.identity, canvas.transform);
        var popup = obj.GetComponent<PopUpText>();
        
        popup.Play("+" + clickPower);
        transform.DOBlendableScaleBy(new Vector3(0.05f, 0.05f, 0.05f), 0.05f).OnComplete(ScaleBack);

        if (Clicks % 10 == 0)
        {
            GameObject catPrefab = cats[Random.Range(0, cats.Length)];
            Instantiate(catPrefab, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
        }

        if (Clicks % 20 == 0)
        {
            Instantiate(yellowBowl, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
        }

        if (Clicks % 25 == 0)
        {
            Instantiate(blueBowl, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
        }

        if (Clicks % 30 == 0)
        {
            Instantiate(purpleBowl, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
        }

        if (Clicks % 40 == 0)
        {
            Instantiate(catFood, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
        }

        if (Clicks % 50 == 0)
        {
            Instantiate(catTree, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
            catTree.GetComponent<SpriteRenderer>().sortingLayerName = "Just in Front of Background";
        }
    }

    private void ScaleBack()
    {
        transform.DOBlendableScaleBy(new Vector3(-0.05f, -0.05f, -0.05f), 0.05f);
    }

    public void Upgrade()
    {
        if (Clicks >= UpgradeCost)
        {
            Clicks -= UpgradeCost;
            AudioSource.PlayClipAtPoint(upgradeAudio, new Vector3(0, 0, -10), 1.0f);
            Upgrades += 1;
            clickPower += 5 * Upgrades;
            UpgradeCost = UpgradeCost += 10 * Upgrades;
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade for " + UpgradeCost + " Yarn";
        }
    }
}
