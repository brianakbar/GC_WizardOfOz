namespace Creazen.Wizard.UI {
    using Creazen.Wizard.SceneManagement;
    using UnityEngine;
    
    public class LosePopupUI : MonoBehaviour {
        public void Retry() {
            FindObjectOfType<LevelManager>().StartNewGame();
        }

        public void Flee() {
            Application.Quit();
        }
    }
}