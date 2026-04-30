using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CareVisitPackingMockup
{
    public sealed partial class CareTypeModel : ObservableObject
    {
        public Guid Id { get; set; }

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private string handbookReference = string.Empty;

        public ObservableCollection<PackingListTemplateItemModel> PackingItems { get; set; } = new();
    }
}