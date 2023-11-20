using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExplainRenderer : MonoBehaviour
{
    [System.Serializable]
    public class ExplainDataProperty
    {
        public Sprite sprite;
		[TextArea]
        public string descriptions;
    }

    public Button previousPageButton, nextpageButton;
    public Image explainImg;
    public TextMeshProUGUI explainText;

    public List<ExplainDataProperty> explainData;

    int currentPage;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        previousPageButton.interactable = currentPage > 0;
        nextpageButton.interactable = currentPage < explainData.Count - 1;

        UpdateContent();
    }

    void UpdateContent()
    {
        explainImg.sprite = explainData[currentPage].sprite;

        StopAllCoroutines();
        StartCoroutine(AppearTextOneByOne(0.1f));
    }

    IEnumerator AppearTextOneByOne(float interval)
    {
        int index = 1;
        string description = explainData[currentPage].descriptions;

        while (index <= description.Length)
        {
            explainText.text = description.Substring(0, index);
            yield return new WaitForSeconds(interval);
            index++;
        }

    }

    public void OnClickPrevious()
    {
        currentPage--;
        UpdateUI();
    }

    public void OnClickNext()
    {
        currentPage++;
        UpdateUI();
    }
}
