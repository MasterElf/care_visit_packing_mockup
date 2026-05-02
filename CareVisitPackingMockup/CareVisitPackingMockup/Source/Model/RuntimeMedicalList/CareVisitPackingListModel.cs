using System.Collections.ObjectModel;

namespace CareVisitPackingMockup
{
    public class CareVisitPackingListModel
    {
        // A collection of packing list items that are merged from a set of care types.
        public ObservableCollection<PackingListTemplateItemModel> PackingItems { get; set; } = new();
    }
}
