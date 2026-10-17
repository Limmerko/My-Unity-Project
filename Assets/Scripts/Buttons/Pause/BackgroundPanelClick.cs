using Buttons.Pause;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Buttons
{
    /**
    * Нажатие мимо панели - закрытвает саму панель
    */
    public class BackgroundPanelClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject[] panels;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (panels == null)
            {
                return;
            }

            foreach (GameObject panel in panels)
            {
                if (panel == null || !panel.activeInHierarchy)
                {
                    continue;
                }

                RectTransform panelRect = panel.GetComponent<RectTransform>();
                if (panelRect != null && RectTransformUtility.RectangleContainsScreenPoint(
                        panelRect, eventData.position, eventData.pressEventCamera))
                {
                    return;
                }
            }

            foreach (GameObject panel in panels)
            {
                if (panel == null || !panel.activeInHierarchy)
                {
                    continue;
                }

                ContinueButton continueButton = panel.GetComponentInChildren<ContinueButton>(true);
                if (continueButton != null && continueButton.isActiveAndEnabled)
                {
                    continueButton.Continue();
                    return;
                }
            }
        }
    }
}
