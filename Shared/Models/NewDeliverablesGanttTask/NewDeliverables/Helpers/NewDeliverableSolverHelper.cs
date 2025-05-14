using Shared.Commons;
using Shared.Enums.TasksRelationTypes;
using Shared.Models.GanttTasks.Responses;
using Shared.Models.NewDeliverablesGanttTask.Interfaces;
using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;
using System.Text;

namespace Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Helpers
{
    public static class NewDeliverableSolverHelper
    {
        public static void Calculate(this NewDeliverablesResponseList response)
        {
            response.ReOrder();
        }

        public static void SetPlannedStartDate(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow row/*,DateTime? StartDate*/)
        {
            //row.Task.PlannedStartDate = StartDate;
            //Dependende si tiene dependencia
            row.Task.CalculateEndDate();

            response.Calculate();
        }
        public static void SetPlannedEndDate(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow row/*,DateTime? EndDate*/)
        {
            //row.Task.PlannedEndDate = EndDate;
            //Dependende si tiene dependencia
            row.Task.CalculateDurationFromDates();

            response.Calculate();
        }
        public static void SetDuration(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow row/*,string? duration*/)
        {
            //row.Task.Duration = duration;
            //Dependende si tiene dependencia
            row.Task.CalculateEndDate();

            response.Calculate();
        }
        public static void SetLag(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow row/*,string? Lag*/)
        {
            //row.Lag = Lag;

            response.Calculate();
        }
        public static void SetDependencyType(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow row/*,TasksRelationTypeEnum tasksRelationType*/)
        {
            //row.Task.DependencyType = tasksRelationType;

            response.Calculate();
        }
        public static string SetDependencyList(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow row/*,string? dependencylistFromUI*/)
        {
            var dependencylistFromUI = row.Task.DependencyList;
            response.RemovePublisherObserved(row);
            
            if (string.IsNullOrEmpty(dependencylistFromUI))
                return "";
            if (row.Task.DependencyType.Id == TasksRelationTypeEnum.None.Id)
            {
                row.Task.DependencyType = TasksRelationTypeEnum.FinishStart;
            }
            var dependencyList = dependencylistFromUI.Split(',')
                                               .Select(dep => dep.Trim())
                                               .Where(dep => !string.IsNullOrEmpty(dep))
                                               .ToList();


            StringBuilder result = new StringBuilder();
            StringBuilder resultMessages = new StringBuilder();
            foreach (var dependency in dependencyList)
            {
                var publisher = response.GetRowFromMainOrder(dependency);
                if (publisher != null)
                {
                    var resultDependency = response.IsValidDependency(row, publisher);
                    if (!string.IsNullOrEmpty(resultDependency))
                    {
                        Console.WriteLine(resultDependency);
                        resultMessages.Append(resultDependency);
                    }
                    else
                    {
                        response.CreatePublisherObserved(row, publisher);
                        
                        if (result.Length > 0)
                        {
                            result.Append(", ");
                        }
                        result.Append($"{dependency}");
                    }
                }


            }
            row.Task.DependencyList = result.ToString();
            return resultMessages.ToString();
        }
        private static string IsValidDependency(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow current, NewDeliverableGanttTaskRow dependency)
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
        private static bool HasAncestor(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow child, NewDeliverableGanttTaskRow potentialAncestor)
        {
            // Buscar el padre del elemento actual
            var parent = response.FindParentFromOverallList(child);

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
        private static bool HasCircularDependency(this NewDeliverablesResponseList response, NewDeliverableGanttTaskRow current, NewDeliverableGanttTaskRow dependency)
        {
            //return false;
            //Conjunto para rastrear los nodos visitados durante la búsqueda
            var visited = new HashSet<int>();
            var stack = new Stack<NewDeliverableGanttTaskRow>();

            // Guardar el estado original de los Dependants
            var originalDependants = new List<PublisherObserverResponse>(current.Task.Observers);
            PublisherObserverResponse taskDependencyResponse = new PublisherObserverResponse()
            {
                PublisherId = dependency.Task.Id,
                ObservedId = current.Task.Id,
                PlannedStartDate = dependency.Task.PlannedStartDate,
                PlannedEndDate = dependency.Task.PlannedEndDate,
                MainOrder = dependency.MainOrder

            };
            current.Task.AddObserver(taskDependencyResponse);

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
                foreach (var dependent in node.Task.Observers)
                {
                    // Si encontramos el nodo inicial (current), hay un ciclo
                    if (dependent.MainOrder == current.MainOrder)
                    {
                        current.Task.SetObservers(originalDependants);
                        return true; // Ciclo detectado
                    }
                    var dependentTask = response.GetTaskResponseFromDependencies(dependent);
                    if(dependentTask != null)

                    {
                        // Agregar el dependiente al stack para seguir explorando
                        stack.Push(dependentTask);
                    }
                    
                }
            }

            // Si no se encontró ningún ciclo, retornar false


            current.Task.SetObservers(originalDependants);
            return false;
        }

    }
}
