using System.Collections;
using UnityEngine;

namespace HazzardGameworks.ProjectInfrastructure.UI
{
    /// <summary>
    /// This class animates ugui rect trans panels, usually when used as a mask.
    /// </summary>
    public class PanelMaskAnimation : MonoBehaviour
    {
        // The rect trans to animate.
        [SerializeField]
        private RectTransform panelRectTrans;
        // Time over which transition is applied.
        [SerializeField]
        private float transitionTime;
        // Current transition time.
        [SerializeField]
        private float transitionTimer;
        // If true, start with the panel fully open.
        [SerializeField]
        private bool startOpen = false;
        // If true, start open transition when the gameobject is enabled.
        [SerializeField]
        private bool openOnEnable = false;
        // Size of the rect trans when it's opened.
        private Vector2 size;

        private void Awake()
        {
            // Set the opened size to the current size.
            size = new Vector2((panelRectTrans.rect.width), (panelRectTrans.rect.height));
            // If startOpen is true, then set the panel size to the opened size.
            if (!startOpen) panelRectTrans.sizeDelta = size * (-1);
        }

        private void OnEnable()
        {
            // If openOnEnable is true, then begin the opening transition.
            if (openOnEnable) OpenPanel();
        }
        /// <summary>
        /// Controls the opening transition of the panel.
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="xEnd"></param>
        /// <param name="yEnd"></param>
        /// <returns></returns>
        private IEnumerator Transitioning(float xStart, float yStart, float xEnd, float yEnd)
        {
            Vector2 size = new(xStart, yStart);

            while (transitionTimer < transitionTime)
            {
                size.y = Mathf.Lerp(yStart, yEnd, transitionTimer / transitionTime);
                size.x = Mathf.Lerp(xStart, xEnd, transitionTimer / transitionTime);
                panelRectTrans.sizeDelta = size;
                transitionTimer += Time.unscaledDeltaTime;
                yield return null;
            }

            size.x = xEnd;
            size.y = yEnd;
            panelRectTrans.sizeDelta = size;

            transitionTimer = 0f;
        }
        /// <summary>
        /// Begins the panel opening transition.
        /// </summary>
        public void OpenPanel()
        {
            StartCoroutine(Transitioning(0, size.y * (-1), 0, 0));
        }
    }
}
