using CommunityToolkit.Mvvm.ComponentModel;

namespace CareVisitPackingMockup
{
    public sealed partial class MedicalItemModel : ObservableObject
    {
        [ObservableProperty]
        private Guid id = Guid.NewGuid(); // Use a default value in case loading is faulty

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string category = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;
    }
}