namespace Creazen.Wizard.UI {
    using Creazen.Wizard.SceneManagement;
    using UnityEngine;
    
    public class WinPopupUI : MonoBehaviour {
        public void PlayAgain() {
            FindObjectOfType<LevelManager>().StartNewGame();
        }
    }
}