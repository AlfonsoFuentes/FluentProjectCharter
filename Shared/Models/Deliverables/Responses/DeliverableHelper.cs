namespace Shared.Models.Deliverables.Responses
{
    public static class DeliverableHelper
    {
        /// <summary>
        /// Convierte una jerarquía de DeliverableResponse en una lista plana.
        /// </summary>
        public static List<DeliverableResponse> FlattenCompletedOrderedItems(IEnumerable<DeliverableResponse> items)
        {
            var flatList = new List<DeliverableResponse>();
            FlattenCompletedItems(items, flatList);
            return flatList.Count == 0 ? new() : flatList.OrderBy(x => x.LabelOrder).ToList();
        }
        public static List<DeliverableResponse> FlattenWithoutDependencesOrSubDeliverablesOrderedItems(IEnumerable<DeliverableResponse> items)
        {
            var flatList = new List<DeliverableResponse>();
            FlattenWithoutDependencesOrSubDeliverablesdItems(items, flatList);
            return flatList.Count == 0 ? new() : flatList.OrderBy(x => x.LabelOrder).ToList();
        }
        public static List<DeliverableResponse> FlattenWithDependencesOrderedItems(IEnumerable<DeliverableResponse> items)
        {
            var flatList = new List<DeliverableResponse>();
            FlattenWithDependencesItems(items, flatList);
            return flatList.Count == 0 ? new() : flatList.OrderBy(x => x.LabelOrder).ToList();
        }
        public static List<DeliverableResponse> FlattenWithDeliverablesOrderedItems(IEnumerable<DeliverableResponse> items)
        {
            var flatList = new List<DeliverableResponse>();
            FlattenWithDeliverablesItems(items, flatList);
            return flatList.Count == 0 ? new() : flatList.OrderBy(x => x.LabelOrder).ToList();
        }
        /// <summary>
        /// Función recursiva para aplanar la jerarquía.
        /// </summary>
        private static void FlattenCompletedItems(IEnumerable<DeliverableResponse> items, List<DeliverableResponse> flatList)
        {
            foreach (var item in items)
            {
                // Añadir el elemento actual a la lista plana
                flatList.Add(item);

                // Procesar los subdeliverables recursivamente
                if (item.SubDeliverables != null && item.SubDeliverables.Any())
                {
                    FlattenCompletedItems(item.OrderedSubDeliverables, flatList);
                }
            }
        }
        private static void FlattenWithoutDependencesOrSubDeliverablesdItems(IEnumerable<DeliverableResponse> items, List<DeliverableResponse> flatList)
        {
            foreach (var item in items)
            {
                if (item.SubDeliverables.Any())
                {
                    foreach (var row in item.SubDeliverables)
                    {
                        FlattenWithoutDependencesOrSubDeliverablesdItems(item.SubDeliverables, flatList);
                    }
                }
                else
                {
                    if (!item.Dependants.Any())
                    {
                        flatList.Add(item);
                    }

                }


            }


        }
        private static void FlattenWithDependencesItems(IEnumerable<DeliverableResponse> items, List<DeliverableResponse> flatList)
        {
            foreach (var item in items)
            {
                if (item.SubDeliverables.Any())
                {
                    foreach (var row in item.SubDeliverables)
                    {
                        FlattenWithDependencesItems(item.SubDeliverables, flatList);
                    }
                }
                else
                {
                    if (item.Dependants.Any())
                        flatList.Add(item);
                }


            }


        }
        private static void FlattenWithDeliverablesItems(IEnumerable<DeliverableResponse> items, List<DeliverableResponse> flatList)
        {
            foreach (var item in items)
            {
                // Procesar los subdeliverables recursivamente
                if (item.SubDeliverables != null && item.SubDeliverables.Any())
                {
                    FlattenCompletedItems(item.OrderedSubDeliverables, flatList);
                    flatList.Add(item);
                }

            }

        }
    }


}
