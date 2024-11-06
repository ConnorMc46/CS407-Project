using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMNG : MonoBehaviour
{
    [SerializeField] private GameObject currentMenu;
    [SerializeField] private GameObject sliderThing;
    [SerializeField] private GameObject TRANSITION_HOLDER;
    [SerializeField] private float slideSpeed = 10f;
    
    [SerializeField] private float waitPerGeneration = 0.1f;
    
    private bool lockMenuChanges;
    
    public void onChange(GameObject newMenuTarget)
    {
        if (lockMenuChanges) return;
        lockMenuChanges = true;
        
        Canvas canvas = FindObjectOfType<Canvas>();
        float h = canvas.GetComponent<RectTransform>().rect.height;
        float w = canvas.GetComponent<RectTransform>().rect.width;

        StartCoroutine(transition());
        return;
        IEnumerator transition()
        {
            var currentGenerationPosition = 0;
            while (currentGenerationPosition < h) {
                var _testGeneration = Instantiate(sliderThing, TRANSITION_HOLDER.transform);
                _testGeneration.GetComponent<RectTransform>().anchoredPosition 
                    = new Vector2(0, -currentGenerationPosition);
                StartCoroutine(slide(_testGeneration));
                
                yield return new WaitForSeconds(waitPerGeneration);
                currentGenerationPosition += 100;
            }
            
            yield return new WaitForSeconds(0.4f);
            currentMenu.SetActive(false);
            newMenuTarget.SetActive(true);
            currentMenu = newMenuTarget;

            lockMenuChanges = false;
        }


        IEnumerator slide(GameObject target)
        {
            var RECT = target.GetComponent<RectTransform>();
            while (RECT.sizeDelta.x < w) {
                RECT.sizeDelta = new Vector2(RECT.sizeDelta.x + slideSpeed, RECT.sizeDelta.y);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1.25f);
            RECT.pivot = new Vector2(1f, 1f);
            RECT.anchoredPosition = new Vector2(w, RECT.anchoredPosition.y);
            while (RECT.sizeDelta.x > 0) {
                RECT.sizeDelta = new Vector2(RECT.sizeDelta.x - slideSpeed, RECT.sizeDelta.y);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
