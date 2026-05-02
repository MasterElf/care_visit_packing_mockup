namespace CareVisitPackingMockup
{
    public class CareVisitPackingListBuilder
    {
        public static void BuildCareVisitPackingList(CalendarAppointmentModel appointment)
        {
            if (appointment is not null)
            {
                appointment.CareVisitPackingListModel.PackingItems.Clear();

                foreach (PackingListTemplateItemModel mergedPackingItem in BuildMergedPackingItems(appointment))
                {
                    appointment.CareVisitPackingListModel.PackingItems.Add(mergedPackingItem);
                }
            }
        }

        private static List<PackingListTemplateItemModel> BuildMergedPackingItems(CalendarAppointmentModel appointment)
        {
            Dictionary<Guid, List<PackingListTemplateItemModel>> packingItemsByMedicalItemId = CollectPackingItemsByMedicalItemId(appointment);

            return packingItemsByMedicalItemId.Values
                .Select(packingItems => CreateMergedPackingItem(packingItems))
                .OrderBy(packingItem => packingItem.MedicalItem.Category, StringComparer.CurrentCultureIgnoreCase)
                .ThenBy(packingItem => packingItem.MedicalItem.Name, StringComparer.CurrentCultureIgnoreCase)
                .ThenBy(packingItem => packingItem.MedicalItemId)
                .ToList();
        }

        private static Dictionary<Guid, List<PackingListTemplateItemModel>> CollectPackingItemsByMedicalItemId(CalendarAppointmentModel appointment)
        {
            Dictionary<Guid, List<PackingListTemplateItemModel>> packingItemsByMedicalItemId = new();

            foreach (CareTypeModel careType in appointment.CareTypes)
            {
                foreach (PackingListTemplateItemModel packingItem in careType.PackingItems)
                {
                    Guid medicalItemId = GetMedicalItemId(packingItem);

                    if (medicalItemId == Guid.Empty)
                    {
                        // A packing item without a medical item id cannot be safely merged with others.
                        // Give it a temporary group key so it is still included as its own copied item.
                        medicalItemId = Guid.NewGuid();
                    }

                    if (!packingItemsByMedicalItemId.TryGetValue(medicalItemId, out List<PackingListTemplateItemModel>? packingItems))
                    {
                        packingItems = new List<PackingListTemplateItemModel>();
                        packingItemsByMedicalItemId.Add(medicalItemId, packingItems);
                    }

                    packingItems.Add(packingItem);
                }
            }

            return packingItemsByMedicalItemId;
        }

        private static PackingListTemplateItemModel CreateMergedPackingItem(IReadOnlyList<PackingListTemplateItemModel> packingItems)
        {
            if (packingItems.Count == 0)
            {
                throw new ArgumentException("At least one packing item is required.", nameof(packingItems));
            }
            else if (packingItems.Count == 1)
            {
                return packingItems[0]; // No merging needed, just return the single item.
            }
            else
            {
                PackingListTemplateItemModel firstPackingItem = packingItems[0];

                return new PackingListTemplateItemModel
                {
                    MedicalItemId = GetMedicalItemId(firstPackingItem),
                    MedicalItem = firstPackingItem.MedicalItem,
                    DefaultQuantity = packingItems.Sum(packingItem => packingItem.DefaultQuantity),
                    IsMandatory = packingItems.Any(packingItem => packingItem.IsMandatory),
                    Reason = MergeReasons(packingItems)
                };
            }
        }

        private static Guid GetMedicalItemId(PackingListTemplateItemModel packingItem)
        {
            if (packingItem.MedicalItem.Id != Guid.Empty)
            {
                return packingItem.MedicalItem.Id;
            }

            return packingItem.MedicalItemId;
        }

        private static string MergeReasons(IEnumerable<PackingListTemplateItemModel> packingItems)
        {
            return string.Join(
                Environment.NewLine,
                packingItems
                    .Select(packingItem => packingItem.Reason)
                    .Where(reason => !string.IsNullOrWhiteSpace(reason))
                    .Distinct(StringComparer.CurrentCulture));
        }
    }
}
