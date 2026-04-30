using System.Collections.ObjectModel;

namespace CareVisitPackingMockup
{
    public sealed class MedicalItemCatalogModel
    {
        public ObservableCollection<MedicalItemModel> Items { get; set; } = new();
    }
}
