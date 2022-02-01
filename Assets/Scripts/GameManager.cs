using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject boosterParticleEffect;
    public bool openDescription;
    public CanvasGroup descriptionCanvasGroup;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        
    }

    void Update()
    {
        if (openDescription)
        {
            if (descriptionCanvasGroup.alpha < 1)
            {
                descriptionCanvasGroup.alpha += Time.deltaTime;
            }
            else
            {
                StartCoroutine(ClosePanel());
            }
        }
        else
        {
            if (descriptionCanvasGroup.alpha > 0)
            {
                descriptionCanvasGroup.alpha -= Time.deltaTime;
            }
        }
    }
    public void OpenPanel()
    {
        openDescription = true;
    }

    public void BoosterParticleInvoke(Vector3 thisPosition)
    {
        Destroy(Instantiate(boosterParticleEffect, thisPosition, Quaternion.identity), 1f);
    }

    IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(.5f);
        openDescription = false;
    }

}
