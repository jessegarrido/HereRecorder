namespace SmartaCam.App.Services
{
    public class AppData
    {
        private int _myState;
        public int MyState 
        {
            get
            {
                return _myState;
            }
            set
            {
                _myState = value;
                NotifyDataChanged();
            }
        }
		public event Action OnChange;

		private void NotifyDataChanged() => OnChange?.Invoke();
	}
}
