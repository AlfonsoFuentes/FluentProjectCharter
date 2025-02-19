using Shared.Models.Deliverables.Responses.NewResponses;

namespace Shared.Models.Deliverables.Responses
{
    //public static class DeliverableHelper2
    //{
    //    /// <summary>
    //    /// Convierte una jerarquía de DeliverableResponse2 en una lista plana.
    //    /// </summary>
    //    public static List<DeliverableResponse2> FlattenHierarchy(IEnumerable<DeliverableResponse2> items)
    //    {
    //        var flatList = new List<DeliverableResponse2>();
    //        FlattenItems(items, flatList);
    //        return flatList.Count == 0 ? new() : flatList.OrderBy(x => x.Order).ToList();
    //    }

    //    /// <summary>
    //    /// Función recursiva para aplanar la jerarquía.
    //    /// </summary>
    //    private static void FlattenItems(IEnumerable<DeliverableResponse2> items, List<DeliverableResponse2> flatList)
    //    {
    //        foreach (var item in items)
    //        {
    //            // Añadir el elemento actual a la lista plana
    //            flatList.Add(item);

    //            // Procesar los subdeliverables recursivamente
    //            if (item.SubDeliverables != null && item.SubDeliverables.Any())
    //            {
    //                FlattenItems(item.OrderedSubDeliverables, flatList);
    //            }
    //        }
    //    }
    //}


}
