using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IFriendDataService _friendDataService;
        public ObservableCollection<Friend> Friends { get; set; }

        private Friend _selectedFriend;

        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                _selectedFriend = value; 
                OnPropertyChanged();
            }
        }

        public MainViewModel(IFriendDataService friendDataSerice)
        {
            Friends = new ObservableCollection<Friend>();
            _friendDataService = friendDataSerice;
        }

        public async Task LoadAsync()
        {
            var friends = await _friendDataService.GetAllAsync();
            Friends.Clear();
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }
    }
}
