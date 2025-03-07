namespace Shared.Models.GanttTasks.Responses
{
    public static class GanttTaskHelper
    {
        /// <summary>
        /// Convierte una jerarquía de GanttTaskResponse en una lista plana.
        /// </summary>
        public static List<GanttTaskResponse> FlattenCompletedOrderedItems(IEnumerable<GanttTaskResponse> items)
        {
            var flatList = new List<GanttTaskResponse>();
            FlattenCompletedItems(items, flatList);
            return flatList.Count == 0 ? new() : flatList.OrderBy(x => x.LabelOrder).ToList();
        }
        public static List<GanttTaskResponse> FlattenWithoutDependencesOrSubGanttTasksOrderedItems(IEnumerable<GanttTaskResponse> items)
        {
            var flatList = new List<GanttTaskResponse>();
            FlattenWithoutDependencesOrSubGanttTasksdItems(items, flatList);
            return flatList.Count == 0 ? new() : flatList.OrderBy(x => x.LabelOrder).ToList();
        }
        public static List<GanttTaskResponse> FlattenWithDependencesOrderedItems(IEnumerable<GanttTaskResponse> items)
        {
            var flatList = new List<GanttTaskResponse>();
            FlattenWithDependencesItems(items, flatList);
            return flatList.Count == 0 ? new() : flatList.OrderBy(x => x.LabelOrder).ToList();
        }
        public static List<GanttTaskResponse> FlattenWithGanttTasksOrderedItems(IEnumerable<GanttTaskResponse> items)
        {
            var flatList = new List<GanttTaskResponse>();
            FlattenWithGanttTasksItems(items, flatList);
            return flatList.Count == 0 ? new() : flatList.OrderBy(x => x.LabelOrder).ToList();
        }
        /// <summary>
        /// Función recursiva para aplanar la jerarquía.
        /// </summary>
        private static void FlattenCompletedItems(IEnumerable<GanttTaskResponse> items, List<GanttTaskResponse> flatList)
        {
            foreach (var item in items)
            {
                // Añadir el elemento actual a la lista plana
                flatList.Add(item);

                // Procesar los subdeliverables recursivamente
                if (item.SubGanttTasks != null && item.SubGanttTasks.Any())
                {
                    FlattenCompletedItems(item.OrderedSubGanttTasks, flatList);
                }
            }
        }
        private static void FlattenWithoutDependencesOrSubGanttTasksdItems(IEnumerable<GanttTaskResponse> items, List<GanttTaskResponse> flatList)
        {
            foreach (var item in items)
            {
                if (item.SubGanttTasks.Any())
                {
                    foreach (var row in item.SubGanttTasks)
                    {
                        FlattenWithoutDependencesOrSubGanttTasksdItems(item.SubGanttTasks, flatList);
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
        private static void FlattenWithDependencesItems(IEnumerable<GanttTaskResponse> items, List<GanttTaskResponse> flatList)
        {
            foreach (var item in items)
            {
                if (item.SubGanttTasks.Any())
                {
                    foreach (var row in item.SubGanttTasks)
                    {
                        FlattenWithDependencesItems(item.SubGanttTasks, flatList);
                    }
                }
                else
                {
                    if (item.Dependants.Any())
                        flatList.Add(item);
                }


            }


        }
        private static void FlattenWithGanttTasksItems(IEnumerable<GanttTaskResponse> items, List<GanttTaskResponse> flatList)
        {
            foreach (var item in items)
            {
                // Procesar los subdeliverables recursivamente
                if (item.SubGanttTasks != null && item.SubGanttTasks.Any())
                {
                    FlattenCompletedItems(item.OrderedSubGanttTasks, flatList);
                    flatList.Add(item);
                }

            }

        }
    }


}
