namespace CareVisitPackingMockup
{
    public class CareVisitPackingListBuilder
    {
        public static void BuildCareVisitPackingList(CalendarAppointmentModel appointment)
        {
            // Clear the existing packing list
            appointment.CareVisitPackingListModel.PackingItems.Clear();

            foreach (CareTypeModel careType in appointment.CareTypes)
            {
                foreach (PackingListTemplateItemModel packingItem in careType.PackingItems)
                {
                    // Add the packing item to the packing list
                    appointment.CareVisitPackingListModel.PackingItems.Add(packingItem);

                    // TODO: join duplicate items
                }
            }
        }
    }
}
