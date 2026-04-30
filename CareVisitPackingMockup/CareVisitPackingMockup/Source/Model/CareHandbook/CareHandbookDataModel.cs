using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CareVisitPackingMockup
{
    public sealed partial class CareHandbookDataModel : ObservableObject
    {
        public ObservableCollection<CareTypeModel> CareTypes { get; set; } = new();

        [ObservableProperty]
        private MedicalItemCatalogModel medicalItemCatalogModel = new MedicalItemCatalogModel();
    }
}
