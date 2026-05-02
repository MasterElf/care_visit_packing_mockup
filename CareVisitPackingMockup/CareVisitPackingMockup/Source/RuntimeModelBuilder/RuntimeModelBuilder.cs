namespace CareVisitPackingMockup
{
    internal class RuntimeModelBuilder
    {
        public static void BuildRuntimeModels(MainModel mainModel)
        {
            SetupAppointmentModels(mainModel.AppointmentCollectionModel, mainModel.CareHandbookDataModel);
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
    }
}
