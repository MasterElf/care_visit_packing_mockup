using CommunityToolkit.Mvvm.ComponentModel;

namespace CareVisitPackingMockup
{
    public sealed partial class MedicalItemModel : ObservableObject
    {
        [ObservableProperty]
        private Guid id;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string category = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;
    }
}