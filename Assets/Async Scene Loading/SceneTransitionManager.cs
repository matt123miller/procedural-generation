using AsyncSceneTransition;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
    This class is designed to manage all scene transitions in an asynchronous way.
    It nicely wraps all of the complexity and accessed via the singleton instance SceneTransitionManager.Instance
    
    This class belongs on a canvas with an Image child. 
    In the provided example I used a black texture, however feel free to substitute whatever you want.
    This texture's alpha is then faded up and down to ease UX of transitions.
    Async loading should start once the texture fades into site, follow that the desired level will load.
    The screen will remain black until loading is finished at which point the texutre will fade out.
    
    The methods for fading the screen to or from black have been left public if, for some reason, 
    you want to fade the screen without changing the level.
    
    There is a loading bar and flashing loading text included as well, this is optional.
    If you do not wish to use these then remove the all the code concerning loadingText and loadingSlider.
    To turn these off and on call the ScreenFade.EnableLoadingUI() method, 
    passing it a boolean for whether you want them on (true) or off (false).
*/

namespace AsyncSceneTransition
{
    public class SceneTransitionManager : MonoBehaviour
    {
        private static SceneTransitionManager _instance;

        // Singleton object, access this via SceneTransitionManager.Instance whenever you need to call a scene transition method.
        public static SceneTransitionManager Instance
        {
            get
            {
                if (_instance == null) { _instance = new SceneTransitionManager(); }
                return _instance;
            }
        }

        [HideInInspector]
        public ScreenFade fader;


        void Awake()
        {
            _instance = this;
        }


        #region Level loading functionality

        /// <summary>
        /// Asynchronously begins loading the chosen level in the build settings
        /// Assumes that the class calling the method knows which scene it wants to go to.
        /// </summary>
        /// <param name="targetScene"></param>
        public void LoadTargetLevel(int targetScene)
        {
            if (targetScene >= SceneManager.sceneCountInBuildSettings)
            {
                // returns to main menu.
                StartCoroutine(AsyncLoadLevel(0));
            }
            else
            {
                StartCoroutine(AsyncLoadLevel(targetScene));
            }
        }

        /// <summary>
        /// Asynchronously begins loading the next level in the build settings, assuming there are levels left to load.
        /// </summary>
        public void LoadNextLevel()
        {
            int targetScene = SceneManager.GetActiveScene().buildIndex + 1;

            if (targetScene >= SceneManager.sceneCountInBuildSettings)
            {
                // There are no scenes left. Maybe return to main menu?
                // Handle this error however you like.
                StartCoroutine(AsyncLoadLevel(0)); // returns to the main menu

            }
            else
            {
                StartCoroutine(AsyncLoadLevel(targetScene));
            }
        }

        public void ReloadCurrentLevel()
        {
            int targetScene = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(AsyncLoadLevel(targetScene));
        }

        private IEnumerator AsyncLoadLevel(int targetScene)
        {
            fader.BeginFadeToBlack(false);

            while (fader.fadeProgress < 0.95)
            {
                yield return null;
            }

            fader.EnableLoadingUI(true);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);

            while (!asyncLoad.isDone)
            {
                // Here you put your loading screen code.
                fader.UpdateSlider(asyncLoad.progress);

                yield return null;
            }

            // You don't need to turn the text and slider back off or call BeginFadeToClear() here as the old scene will now be destroyed.
            // The new scene that was just loaded asynchonously will replace it and should have a SceneManager object in it to handle fading etc.
        }
        #endregion


    }
}