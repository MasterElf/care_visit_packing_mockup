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

            // After all data is loaded, it is time to initialize the data models with runtime data.
            RuntimeModelBuilder.BuildRuntimeModels(MainModel);
        }
    }
}
