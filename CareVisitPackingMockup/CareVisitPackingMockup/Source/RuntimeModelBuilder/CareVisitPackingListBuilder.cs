namespace CareVisitPackingMockup
{
    /// <summary>
    /// Helps to merge packing items from multiple care types.
    /// </summary>
    public class CareVisitPackingListBuilder
    {
        // Make the packing list ready for the appointment. Done by merging and sorting packing items.
        public static void BuildCareVisitPackingList(CalendarAppointmentModel appointment)
        {
            if (appointment is not null)
            {
                // Clear the current set of packing items.
                appointment.CareVisitPackingListModel.PackingItems.Clear();

                // Create merged packing items and add them to the appointment
                foreach (PackingListTemplateItemModel mergedPackingItem in BuildMergedPackingItems(appointment))
                {
                    appointment.CareVisitPackingListModel.PackingItems.Add(mergedPackingItem);
                }
            }
        }

        private static List<PackingListTemplateItemModel> BuildMergedPackingItems(CalendarAppointmentModel appointment)
        {
            // Create lists of packing items grouped by their medical item ids.
            Dictionary<Guid, List<PackingListTemplateItemModel>> packingItemsByMedicalItemId = CollectPackingItemsByMedicalItemId(appointment);

            List<PackingListTemplateItemModel> mergedPackingItems = new();

            // Create one merged packing item for each medical item.
            foreach (List<PackingListTemplateItemModel> packingItems in packingItemsByMedicalItemId.Values)
            {
                mergedPackingItems.Add(CreateMergedPackingItem(packingItems));
            }

            // Sort the merged packing items.
            mergedPackingItems.Sort(ComparePackingItems);

            return mergedPackingItems;
        }

        /// <summary>
        /// Comparison method to help sort the packing items.
        /// </summary>
        private static int ComparePackingItems(PackingListTemplateItemModel left, PackingListTemplateItemModel right)
        {
            int categoryComparison = StringComparer.CurrentCultureIgnoreCase.Compare(left.MedicalItem.Category, right.MedicalItem.Category);

            if (categoryComparison != 0)
            {
                return categoryComparison;
            }

            int nameComparison = StringComparer.CurrentCultureIgnoreCase.Compare(left.MedicalItem.Name, right.MedicalItem.Name);

            if (nameComparison != 0)
            {
                return nameComparison;
            }

            // Fallback if the medical items have the same category and name.
            return left.MedicalItemId.CompareTo(right.MedicalItemId);
        }

        /// <summary>
        /// Creates a dictionary with the grouped medical items.
        /// </summary>
        private static Dictionary<Guid, List<PackingListTemplateItemModel>> CollectPackingItemsByMedicalItemId(CalendarAppointmentModel appointment)
        {
            Dictionary<Guid, List<PackingListTemplateItemModel>> packingItemsByMedicalItemId = new();

            foreach (CareTypeModel careType in appointment.CareTypes)
            {
                foreach (PackingListTemplateItemModel packingItem in careType.PackingItems)
                {
                    Guid medicalItemId = packingItem.MedicalItem.Id;

                    if (medicalItemId != Guid.Empty)
                    {
                        // If a list for the medical item is missing, create it.
                        if (!packingItemsByMedicalItemId.TryGetValue(medicalItemId, out List<PackingListTemplateItemModel>? packingItems))
                        {
                            packingItems = new List<PackingListTemplateItemModel>();
                            packingItemsByMedicalItemId.Add(medicalItemId, packingItems);
                        }

                        // Now add the packing item to the list for the medical item.
                        packingItems.Add(packingItem);
                    }
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
                    MedicalItemId = firstPackingItem.MedicalItem.Id,
                    MedicalItem = firstPackingItem.MedicalItem,
                    DefaultQuantity = packingItems.Sum(packingItem => packingItem.DefaultQuantity),
                    IsMandatory = packingItems.Any(packingItem => packingItem.IsMandatory),
                    Reason = MergeReasons(packingItems)
                };
            }
        }

        private static string MergeReasons(IEnumerable<PackingListTemplateItemModel> packingItems)
        {
            HashSet<string> uniqueReasons = new(StringComparer.CurrentCultureIgnoreCase);
            List<string> reasons = new();

            foreach (PackingListTemplateItemModel packingItem in packingItems)
            {
                string? reason = packingItem.Reason;

                if (!string.IsNullOrWhiteSpace(reason) && uniqueReasons.Add(reason))
                {
                    reasons.Add(reason);
                }
            }

            return string.Join(Environment.NewLine, reasons);
        }
    }
}
