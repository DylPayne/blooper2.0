using System.Diagnostics.CodeAnalysis;

namespace BlooperFE.Data
{
    public class UserState
    {
        public UserModel user { get; private set; }
        public bool isLoggedIn { get; set; }

        public event Action OnChange;
        public UserState(UserModel user)
        {
            this.user = user;
            this.isLoggedIn = true;
        }

        public void Login(UserModel userInput)
        {
            user = userInput;
            isLoggedIn = true;
            NotifyStateChanged();
        }
        public void Logout()
        {
            this.isLoggedIn = false;
        }

        public UserState GetUserState()
        {
            return this;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
