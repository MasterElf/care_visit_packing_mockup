using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Controls;

namespace CareVisitPackingMockup
{
    /// <summary>
    /// Interaction logic for SchedulePicker.xaml
    /// </summary>
    public partial class SchedulePicker : UserControl
    {
        public SchedulePicker()
        {
            InitializeComponent();
        }

        private void Scheduler_AppointmentTapped(object sender, AppointmentTappedArgs e)
        {
            if (DataContext is AppointmentCollectionModel appointmentCollectionModel)
            {
                appointmentCollectionModel.SelectedAppointment = e.Appointment?.Data as CalendarAppointmentModel;
            }
        }
    }
}
