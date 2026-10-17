using Buttons.Pause;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Buttons
{
    /// <summary>
    /// Closes an active panel when its background is clicked, if that panel
    /// contains a ContinueButton.
    /// </summary>
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
