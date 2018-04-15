using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Event;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IMeetingLookupDataService _meetingLookupService;
        private IFriendLookupDataService _friendLookupService;
        private IEventAggregator _eventAggregator;

        public ObservableCollection<NavigationItemViewModel> Friends { get; set; }

        public ObservableCollection<NavigationItemViewModel> Meetings { get; set; }

        public NavigationViewModel(IFriendLookupDataService friendLookupService,
            IMeetingLookupDataService meetingLookupService,
            IEventAggregator eventAggregator)
        {
            _friendLookupService = friendLookupService;
            _eventAggregator = eventAggregator;
            _meetingLookupService = meetingLookupService;

            Friends = new ObservableCollection<NavigationItemViewModel>();
            Meetings = new ObservableCollection<NavigationItemViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var lookupItem in lookup)
            {
                Friends.Add(new NavigationItemViewModel(lookupItem.Id, lookupItem.DisplayMember,
                    _eventAggregator, nameof(FriendDetailViewModel)));
            }

            lookup = await _meetingLookupService.GetMeetingLookupAsync();
            Meetings.Clear();
            foreach (var lookupItem in lookup)
            {
                Meetings.Add(new NavigationItemViewModel(lookupItem.Id, lookupItem.DisplayMember,
                    _eventAggregator, nameof(MeetingDetailViewModel)));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs obj)
        {
            switch (obj.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailSaved(Friends, obj);
                    break;

                case nameof(MeetingDetailViewModel):
                    AfterDetailSaved(Meetings, obj);
                    break;
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items, AfterDetailSavedEventArgs obj)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == obj.Id);
            if (lookupItem == null)
            {
                items.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember, _eventAggregator,
                    obj.ViewModelName));
            }
            else
                lookupItem.DisplayMember = obj.DisplayMember;
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailDeleted(Friends, args);
                    break;

                case nameof(MeetingDetailViewModel):
                    AfterDetailDeleted(Meetings, args);
                    break;
            }

        }

        private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items, AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id);
            if (item != null)
            {
                Friends.Remove(item);
            }
        }
    }
}
