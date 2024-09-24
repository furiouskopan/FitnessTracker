using FitnessTrackerMAUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackerMAUI.ViewModels
{
    //public class UserProfileViewModel : INotifyPropertyChanged
    //{
    //    private readonly FitnessTrackerApiService _apiService;

    //    private UserProfileDTO _userProfile;
    //    public UserProfileDTO UserProfile
    //    {
    //        get => _userProfile;
    //        set
    //        {
    //            _userProfile = value;
    //            OnPropertyChanged(nameof(UserProfile));
    //        }
    //    }

    //    public UserProfileViewModel(FitnessTrackerApiService apiService)
    //    {
    //        _apiService = apiService;
    //        LoadUserProfile();
    //    }

    //    public async void LoadUserProfile()
    //    {
    //        UserProfile = await _apiService.GetUserProfileAsync();
    //    }

    //    public async Task UpdateUserProfile(UserProfileUpdateDTO updateDTO)
    //    {
    //        await _apiService.UpdateUserProfileAsync(updateDTO);
    //        LoadUserProfile(); // Refresh profile after update
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected virtual void OnPropertyChanged(string propertyName)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //}
}
