using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour
{
    public List<Sprite> tutorialImages = new List<Sprite>();
    public Canvas canvas;
    public Image actualImage;
    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject closeButton;
    public int index = 0;

    public static TutorialControl Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(Instance);
    }

    public void Start()
    {
        setTutorial(1, false);

    }

    public void setTutorial(int index, bool galleryButtons)
    {
        actualImage.enabled = true;
        this.index = index;
        actualImage.sprite = tutorialImages[index];
        nextButton.SetActive(galleryButtons);
        prevButton.SetActive(galleryButtons);
        closeButton.SetActive(true);

    }

    public void closeTutorial()
    {
        actualImage.enabled = false;
        closeButton.SetActive(false);
        nextButton.SetActive(false);
        prevButton.SetActive(false);
    }

    public void openTutorial()
    {
        index = 1;
        actualImage.sprite = tutorialImages[index];
        actualImage.enabled = true;
        closeButton.SetActive(true);
        nextButton.SetActive(true);
        prevButton.SetActive(true);
    }

    public void nextButtonFunction()
    {
        if (index < tutorialImages.Count - 1)
        {
            index++;
            actualImage.sprite = tutorialImages[index];
        }
            
    }

    public void prevButtonFunction()
    {
        if (index > 1)
        {
            index--;
            actualImage.sprite = tutorialImages[index];
        }
    }
}
