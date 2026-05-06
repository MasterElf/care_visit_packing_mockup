using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace CareVisitPackingMockup
{
    public sealed partial class CalendarAppointmentModel : ObservableObject
    {
        [ObservableProperty]
        private object? schedulerId;

        [ObservableProperty]
        private Guid careVisitId = Guid.NewGuid();

        [ObservableProperty]
        private string subject = string.Empty;

        [ObservableProperty]
        private DateTime startTime;

        [ObservableProperty]
        private DateTime endTime;

        [ObservableProperty]
        private string? location;

        [ObservableProperty]
        private string? notes;

        [ObservableProperty]
        private Brush appointmentBackground = Brushes.SteelBlue;

        [ObservableProperty]
        private Brush foreground = Brushes.White;

        // Persisted care type id properties
        public List<Guid> CareTypeIds { get; set; } = new();

        // Runtime binding properties - not persisted
        public ObservableCollection<CareTypeModel> CareTypes { get; } = new();

        // Generated set of packing items based on the care types - not persisted
        [ObservableProperty]
        private CareVisitPackingListModel careVisitPackingListModel = new();
    }
}