namespace Creazen.Wizard.Preference {
    using UnityEngine;

    public abstract class Preference {
        [SerializeField] protected string tag = "Player";

        public abstract void Handle();
    }
}