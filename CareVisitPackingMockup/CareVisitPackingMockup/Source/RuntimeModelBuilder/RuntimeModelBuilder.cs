namespace CareVisitPackingMockup
{
    internal class RuntimeModelBuilder
    {
        public static void BuildRuntimeModels(MainModel mainModel)
        {
            SetupAppointmentModels(mainModel.AppointmentCollectionModel, mainModel.CareHandbookDataModel);
            SetupPackingListTemplateModels(mainModel.CareHandbookDataModel);
        }

        private static void SetupAppointmentModels(AppointmentCollectionModel appointmentCollectionModel, CareHandbookDataModel careHandbookDataModel)
        {
            foreach (CalendarAppointmentModel appointment in appointmentCollectionModel.Appointments)
            {
                if (appointment.CareTypeIds != null)
                {
                    foreach (Guid careTypeId in appointment.CareTypeIds)
                    {
                        CareTypeModel? careType = careHandbookDataModel.CareTypes.FirstOrDefault(ct => ct.Id == careTypeId);
                        if (careType != null)
                        {
                            appointment.CareTypes.Add(careType);
                        }
                    }
                }
            }
        }

        private static void SetupPackingListTemplateModels(CareHandbookDataModel careHandbookDataModel)
        {
            foreach (CareTypeModel careType in careHandbookDataModel.CareTypes)
            {
                foreach (PackingListTemplateItemModel packingListTemplate in careType.PackingItems)
                {
                    MedicalItemModel? medicalItem = careHandbookDataModel.MedicalItemCatalogModel.Items.FirstOrDefault(mi => mi.Id == packingListTemplate.MedicalItemId);

                    if (medicalItem != null)
                    {
                        packingListTemplate.MedicalItem = medicalItem;
                    }
                }
            }
        }
    }
}
