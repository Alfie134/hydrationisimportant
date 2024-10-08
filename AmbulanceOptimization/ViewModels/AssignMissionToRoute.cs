using System.Collections.ObjectModel;
using System.ComponentModel;
using Models;

public class AssignMissionToRouteViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Mission> Missions { get; set; }

    public AssignMissionToRouteViewModel(IEnumerable<Mission> selectedMissions)
    {
        Missions = new ObservableCollection<Mission>(selectedMissions);
    }

    // Implementer INotifyPropertyChanged her
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
