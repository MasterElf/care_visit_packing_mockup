namespace CareVisitPackingMockup
{
    internal partial class CareDataManager
    {
        public MainModel MainModel { get; } = new MainModel();

        public void Initialize()
        {
            CareHandbookJsonDataLoader careHandbookJsonDataLoader = new CareHandbookJsonDataLoader();

            MainModel.CareHandbookDataModel = careHandbookJsonDataLoader.LoadFromFile("ContentData/care-handbook-example-data.json");
        }
    }
}
