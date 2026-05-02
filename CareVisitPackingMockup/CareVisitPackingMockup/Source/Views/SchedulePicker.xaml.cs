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
            if (e.Appointment?.Data is not CalendarAppointmentModel appointment)
            {
                return;
            }

            if (DataContext is not AppointmentCollectionModel appointmentCollectionModel)
            {
                return;
            }

            //mainModel.SelectedAppointment = appointment;
        }
    }
}
