using CommunityToolkit.Mvvm.ComponentModel;

namespace CareVisitPackingMockup
{
    public sealed partial class PackingListTemplateItemModel : ObservableObject
    {
        [ObservableProperty]
        private Guid medicalItemId;

        [ObservableProperty]
        private int defaultQuantity;

        [ObservableProperty]
        private bool isMandatory;

        [ObservableProperty]
        private string reason = string.Empty;
    }
}
