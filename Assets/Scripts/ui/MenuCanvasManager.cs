using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZAGSTUDIO.GAME.UI
{
    public class MenuCanvasManager : MonoBehaviour
    {

        [SerializeField] protected Page[] allPages;

        public virtual void SetPage(PAGENAME pageName)
        {
            if (pageName != null)
            {
                foreach (var page in allPages)
                {
                    page.gameObject.SetActive(false);
                }
                Page currentPage = Array.Find(allPages, p => p.pageName == pageName);
                if (currentPage != null)
                {
                    currentPage.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (var page in allPages)
                {
                    page.gameObject.SetActive(false);
                }
            }
        }
        public virtual void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
