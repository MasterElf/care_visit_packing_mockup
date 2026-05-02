using CommunityToolkit.Mvvm.ComponentModel;

namespace CareVisitPackingMockup
{
    internal partial class MainModel : ObservableObject
    {
        [ObservableProperty]
        private CareHandbookDataModel careHandbookDataModel = new CareHandbookDataModel();

        public AppointmentCollectionModel AppointmentCollectionModel { get; private set; } = new AppointmentCollectionModel();
    }
}
