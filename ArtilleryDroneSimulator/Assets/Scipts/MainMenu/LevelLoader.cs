using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LevelLoader : MonoBehaviour
    {
        public void LoadFirstLevel()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
