using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CareVisitPackingMockup
{
    internal partial class AppointmentCollectionModel : ObservableObject
    {
        public ObservableCollection<CalendarAppointmentModel> Appointments { get; } = new();

        [ObservableProperty]
        private CalendarAppointmentModel? selectedAppointment;

        // TODO: Move this test data creation to a separate test data initializer class or method
        public AppointmentCollectionModel()
        {
            DateTime today = DateTime.Today;

            Appointments.Add(new CalendarAppointmentModel
            {
                Id = Guid.NewGuid(),
                Subject = "Home care visit - Anna Andersson",
                StartTime = today.AddHours(10),
                EndTime = today.AddHours(11),
                Location = "Sickla",
                Notes = "Bring wound dressing material.",
                CareTypeIds = new List<Guid> { Guid.Parse("22222222-2222-2222-2222-222222222221"), Guid.Parse("22222222-2222-2222-2222-222222222222") }
            });
        }
    }
}
