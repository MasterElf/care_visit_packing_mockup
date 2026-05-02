namespace CareVisitPackingMockup
{
    internal partial class CareDataManager
    {
        public MainModel MainModel { get; } = new MainModel();

        public void Initialize()
        {
            CareHandbookDataModel? careHandbookDataModel = CareHandbookJsonDataLoader.LoadFromFile("ContentData/care-handbook-example-data.json");

            if (careHandbookDataModel != null)
            {
                MainModel.CareHandbookDataModel = careHandbookDataModel;
            }

            // After all data is loaded, it is time to initialize the data models with runtime data.
            RuntimeModelBuilder.BuildRuntimeModels(MainModel);

            // Hook up the package list generation machine
            MainModel.AppointmentCollectionModel.PropertyChanged += SelectedAppointment_PropertyChanged;
        }

        private void SelectedAppointment_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        { 
            if (e.PropertyName == nameof(AppointmentCollectionModel.SelectedAppointment))
            {
                if (sender is AppointmentCollectionModel appointmentCollectionModel)
                {
                    CalendarAppointmentModel? selectedAppointment = appointmentCollectionModel.SelectedAppointment;
                    if (selectedAppointment != null)
                    {
                        CareVisitPackingListBuilder.BuildCareVisitPackingList(selectedAppointment);
                    }
                }
            }
        }
    }
}
