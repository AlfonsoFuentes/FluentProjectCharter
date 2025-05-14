using Shared.Enums.TasksRelationTypes;

namespace Shared.Models.DeliverableGanttTasks.Responses
{
    public static class DeliverableGanttTaskResponseListCreationalHelpers
    {

        public static DeliverableGanttTaskResponse? Create(this DeliverableGanttTaskResponseList response, DeliverableGanttTaskResponse ParentTask = null!)
        {
            var result = new DeliverableGanttTaskResponse()
            {
                InternalOrder = ParentTask == null ? response.LastDeliverableOrder + 1 : ParentTask.LastInternalOrder + 1,
                MainOrder = response.LastMainOrder + 1,

                ParentWBS = ParentTask == null ? "" : ParentTask.WBS,
                StartDate = response.InitialProjectStartDate,
                Duration = "1d",
                EndDate = response.InitialProjectStartDate?.AddDays(1),
                DependencyType = TasksRelationTypeEnum.FinishStart,
                IsDeliverable = ParentTask == null,


            };
            if (ParentTask != null)
            {
                ParentTask.AddSubTask(result);

            }

            response.Items.Add(result);

            response.Reorder();
            return result;
        }
        public static void Delete(this DeliverableGanttTaskResponseList response, DeliverableGanttTaskResponse row)
        {

            if (row.TaskParentId.HasValue)
            {
                var parentrow = response.FindRow(row.TaskParentId.Value);
                if (parentrow != null)
                {
                    parentrow.RemoveSubTask(row);
                }
            }
            response.Items.Remove(row);
            response.Reorder();

        }
        public static DeliverableGanttTaskResponse? FindRow(this DeliverableGanttTaskResponseList response, Guid? Id)
        {
            if (Id == null || !Id.HasValue) return null;

            var row = response.Items.FirstOrDefault(x => x.Id == Id);
            return row;
        }
        public static void RemoveSubTask(this DeliverableGanttTaskResponse ParentTask, DeliverableGanttTaskResponse subTask)
        {
            if (ParentTask.SubTasks.Contains(subTask))
            {
                ParentTask.SubTasks.Remove(subTask);
                subTask.TaskParentId = null;

            }
            int i = 1;
            foreach (var row in ParentTask.OrderedSubTasks)
            {
                row.InternalOrder = i;
                i++;
            }

        }
        public static void AddSubTask(this DeliverableGanttTaskResponse parentask, DeliverableGanttTaskResponse subTask)
        {
            subTask.DeliverableId = parentask.DeliverableId;
            subTask.IsParentDeliverable = parentask.IsDeliverable;
            subTask.TaskParentId = parentask.Id;
            subTask.InternalOrder = parentask.LastInternalOrder + 1;
            subTask.ParentWBS = parentask.WBS;

            parentask.SubTasks.Add(subTask);

        }
        public static DeliverableGanttTaskResponse GetRowFromMainOrder(this DeliverableGanttTaskResponseList response, string mainorder)
        {

            return response.Items.FirstOrDefault(x => x.MainOrder.ToString() == mainorder) ?? null!;
        }
        public static string IsValidDependency(this DeliverableGanttTaskResponseList response, DeliverableGanttTaskResponse current, DeliverableGanttTaskResponse dependency)
        {
            try
            {
                // 1. Un elemento no puede depender de sí mismo
                if (current.MainOrder == dependency.MainOrder)
                {
                    return "Element can not depend from itself";
                }

                // 2. Un elemento no puede depender de su padre ni de ningún ancestro
                if (response.HasAncestor(current, dependency))
                {
                    return "Element can not depend from any parent";
                }

                // 3. Detectar ciclos indirectos
                if (response.HasCircularDependency(current, dependency))
                {
                    return "Reference circular detected"; // Dependencia inválida: ciclo detectado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return string.Empty;
        }
        private static bool HasAncestor(this DeliverableGanttTaskResponseList response, DeliverableGanttTaskResponse child, DeliverableGanttTaskResponse potentialAncestor)
        {
            // Buscar el padre del elemento actual
            var parent = response.FindRow(child.TaskParentId);

            // Si no hay padre, detener la recursión
            if (parent == null)
            {
                return false;
            }

            // Si el padre es el ancestro buscado, retornar true
            if (parent.MainOrder == potentialAncestor.MainOrder)
            {
                return true;
            }

            // Continuar buscando recursivamente hacia arriba
            return response.HasAncestor(parent, potentialAncestor);
        }
        private static bool HasCircularDependency(this DeliverableGanttTaskResponseList response, DeliverableGanttTaskResponse current, DeliverableGanttTaskResponse dependency)
        {
            //return false;
            //Conjunto para rastrear los nodos visitados durante la búsqueda
            var visited = new HashSet<int>();
            var stack = new Stack<DeliverableGanttTaskResponse>();

            // Guardar el estado original de los Dependants
            var originalDependants = new List<DeliverableGanttTaskResponse>(current.Dependencies);

            current.Dependencies.Add(dependency);

            // Comenzar la búsqueda desde el nodo "current"
            stack.Push(current);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                // Si ya hemos visitado este nodo, continuar con el siguiente
                if (visited.Contains(node.MainOrder))
                {
                    continue;
                }

                // Marcar este nodo como visitado
                visited.Add(node.MainOrder);

                // Agregar todos los dependientes de este nodo al stack para seguir explorando
                foreach (var dependent in node.Dependencies)
                {
                    // Si encontramos el nodo inicial (current), hay un ciclo
                    if (dependent.MainOrder == current.MainOrder)
                    {
                        current.Dependencies = originalDependants;
                        return true; // Ciclo detectado
                    }

                    // Agregar el dependiente al stack para seguir explorando
                    stack.Push(dependent);
                }
            }

            // Si no se encontró ningún ciclo, retornar false


            current.Dependencies = originalDependants;
            return false;
        }
        public static void UpdateSubTaskAndDependencies(this DeliverableGanttTaskResponseList response)
        {
            response.UpdateSubTasks();
            response.UpdateDependencies();
        }
        private static void UpdateSubTasks(this DeliverableGanttTaskResponseList response)
        {
            var flatList = response.Items.ToList();

            var deliverables = response.Items.Where(x => x.IsDeliverable).ToList();
            var idToItemMap = flatList.ToDictionary(item => item.Id);



            foreach (var deliverable in deliverables)
            {
                var tasks = flatList.Where(x => x.IsTask && x.DeliverableId == deliverable.Id).ToList();
                foreach (var item in tasks)
                {

                    if (item.IsParentDeliverable && item.TaskParentId == deliverable.Id)
                    {
                        deliverable.SubTasks.Add(item);

                    }
                    else if (item.TaskParentId.HasValue && idToItemMap.TryGetValue(item.TaskParentId.Value, out var parent))
                    {
                        parent.SubTasks.Add(item);
                    }
                }
            }


        }
        private static void UpdateDependencies(this DeliverableGanttTaskResponseList response)
        {
            foreach (var item in response.Items)
            {
                if (!string.IsNullOrEmpty(item.LoadDependencyList))
                {
                    response.SetDependeyList(item, item.LoadDependencyList);
                }
            }
        }
        public static void CancelCreate(this DeliverableGanttTaskResponseList response, DeliverableGanttTaskResponse row)
        {
            if (row.IsCreating)
            {
                if (row.TaskParentId.HasValue)
                {
                    var parentrow = response.FindRow(row.TaskParentId.Value);
                    if (parentrow != null)
                    {
                        parentrow.RemoveSubTask(row);
                    }
                }
                response.Items.Remove(row);
            }
        }
        public static DeliverableGanttTaskResponse MapRowCreational(this DeliverableGanttTaskResponse row)
        {
            var result= new DeliverableGanttTaskResponse()
            {
                Id = row.Id,
                IsDeliverable = row.IsDeliverable,
                IsParentDeliverable = row.IsParentDeliverable,
                MainOrder = row.MainOrder,
                InternalOrder = row.InternalOrder,
                Name = row.Name,
                ParentWBS = row.ParentWBS,
                StartDate = row.StartDate,
                EndDate = row.EndDate,
                Duration = row.Duration,
                DependencyType = row.DependencyType,
                TaskParentId = row.TaskParentId,
                DeliverableId = row.DeliverableId,
                LagInDays = row.LagInDays,
                LagInUnits = row.LagInUnits,
                LagUnit = row.LagUnit,
                DurationInDays = row.DurationInDays,
                DurationInUnit = row.DurationInUnit,
                DurationUnit = row.DurationUnit,
                


            };
            result.Dependencies = row.Dependencies.Select(x => x).ToList();
            return result;
        }
        public static DeliverableGanttTaskResponse MapRow(this DeliverableGanttTaskResponse source, DeliverableGanttTaskResponse destination)
        {
            destination.Id = source.Id;
            destination.IsDeliverable = source.IsDeliverable;
            destination.IsParentDeliverable = source.IsParentDeliverable;
            destination.MainOrder = source.MainOrder;
            destination.InternalOrder = source.InternalOrder;
            destination.Name = source.Name;
            destination.ParentWBS = source.ParentWBS;
            destination.StartDate = source.StartDate;
            destination.EndDate = source.EndDate;
            destination.Duration = source.Duration;
            destination.DependencyType = source.DependencyType;
            destination.TaskParentId = source.TaskParentId;
            destination.DeliverableId = source.DeliverableId;
            destination.LagInDays = source.LagInDays;
            destination.LagInUnits = source.LagInUnits;
            destination.LagUnit = source.LagUnit;
            destination.DurationInDays = source.DurationInDays;
            destination.DurationInUnit = source.DurationInUnit;
            destination.DurationUnit = source.DurationUnit;
            destination.Dependencies = source.Dependencies.Select(x=>x).ToList();


            return destination;


        }
    }
}
