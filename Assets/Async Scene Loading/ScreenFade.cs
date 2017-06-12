using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AsyncSceneTransition
{
    public class ScreenFade : MonoBehaviour
    {
        //[Tooltip("To begin immediately use 0, increase it to delay the start time")]
        private float _fadeMultiplier = 1;
        [SerializeField]
        private float _fadeDuration = 1;
        public float fadeProgress = 0;

        // These are the components manipulated by during the fade and scene loading.
        private Image _fadingImage;
        private Text _loadingText;
        private Slider _loadingSlider;
        private Image _loadingSliderImage;


        void OnEnable()
        {
            _fadingImage = GetComponent<Image>();
            _loadingSlider = GetComponentInChildren<Slider>();
            _loadingText = GetComponentInChildren<Text>();
            _loadingSliderImage = _loadingSlider.gameObject.GetComponentInChildren<Image>();

            _fadingImage.enabled = true;
        }

        void Start()
        {
            // Performed in Start to allow all variables to be cached first.
            EnableLoadingUI(false);
            BeginFadeToClear();
            SceneTransitionManager.Instance.fader = this;
        }

        public void EnableLoadingUI(bool set)
        {
            _loadingText.gameObject.SetActive(set);
            _loadingText.enabled = set;
            _loadingSlider.gameObject.SetActive(set);
            _loadingSlider.enabled = set;
            _loadingSliderImage.gameObject.SetActive(set);
            _loadingSliderImage.enabled = set;
        }

        public void UpdateSlider(float progress)
        {
            _loadingSlider.value = progress;
            _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b, Mathf.Lerp(0, 1, progress));
        }

        public void BeginFadeToBlack(bool fadeToClearFlag)
        {
            StartCoroutine(FadeToBlack(fadeToClearFlag));
        }

        public void BeginFadeToClear()
        {
            StartCoroutine(FadeToClear());
        }

        private IEnumerator FadeToClear()
        {
            _fadingImage.enabled = true;
            _fadeMultiplier = 1 / _fadeDuration;

            for (float f = 1; f >= 0; f -= ( _fadeMultiplier * Time.deltaTime))
            {
                fadeProgress = f;
                Color c = _fadingImage.color;
                c.a = f;
                _fadingImage.color = c;
                yield return null;
            }

            _fadingImage.enabled = false;
        }

        private IEnumerator FadeToBlack(bool fadeToClearFlag)
        {
            _fadingImage.enabled = true;

            for (float f = 0f; f <= _fadeDuration; f += (_fadeMultiplier * Time.deltaTime))
            {
                fadeProgress = f;
                Color c = _fadingImage.color;
                c.a = f;
                _fadingImage.color = c;
                yield return null;
            }
            // Included in the unlikely event that scenes will fade to black only. 
            if (fadeToClearFlag)
            {
                BeginFadeToClear();
            }
        }
    }
}