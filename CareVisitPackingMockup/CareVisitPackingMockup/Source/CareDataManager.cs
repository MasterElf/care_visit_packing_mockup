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
        }
    }
}
