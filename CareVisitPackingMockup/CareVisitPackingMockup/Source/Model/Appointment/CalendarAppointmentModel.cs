using CommunityToolkit.Mvvm.ComponentModel;

namespace CareVisitPackingMockup
{
    public sealed partial class CalendarAppointmentModel : ObservableObject
    {
        [ObservableProperty]
        private Guid id;

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
    }
}
