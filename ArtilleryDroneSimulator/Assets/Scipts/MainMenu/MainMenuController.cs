using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class MainMenuController : MonoBehaviour
    {
        public static MainMenuController Instance { get; private set; }

        [SerializeField] private GameObject menuObject;
        [SerializeField] private GameObject levelSelectionObject;
        [SerializeField] private GameObject settingsObject;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("more than one MainMenuController in scene");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            ChangeToMenu();
        }

        public void ChangeToMenu()
        {
            menuObject.SetActive(true);
            levelSelectionObject.SetActive(false);
            settingsObject.SetActive(false);
        }

        public void ChangeToLevelSelection()
        {
            menuObject.SetActive(false);
            levelSelectionObject.SetActive(true);
            settingsObject.SetActive(false);
        }

        public void ChangeToSettings()
        {
            menuObject.SetActive(false);
            levelSelectionObject.SetActive(false);
            settingsObject.SetActive(true);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }

    public enum MenuState
    {
        Menu,
        LevelSelection,
        Settings
    }
}
