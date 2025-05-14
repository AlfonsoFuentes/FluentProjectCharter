using Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Responses;
using Shared.Models.NewDeliverablesGanttTask.NewGanttTask.Responses;

namespace Shared.Models.NewDeliverablesGanttTask.NewDeliverables.Helpers
{

    public static class NewDeliverableReorderHelper
    {


        public static bool DisableButtonCanMoveDown(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {

            if (selectedRow == null) return true;

            bool result = true;
            if (selectedRow.DeliverableId.HasValue)
            {
                if (selectedRow.ParentTaskId.HasValue)
                {
                    var selectedParent = Response.GetDeliverableTask(selectedRow.ParentTaskId.Value);
                    if (selectedParent != null)
                    {
                        result = selectedParent.LastMainOrder == selectedRow.MainOrder;
                    }


                }
                else
                {
                    var selectedDeliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);
                    if (selectedDeliverable != null)
                        result = selectedDeliverable.LastMainOrder == selectedRow.MainOrder;
                }

            }
            return result;
        }
        public static bool DisableButtonCanMoveUp(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {

            if (selectedRow == null) return true;

            bool result = true;
            if (selectedRow.DeliverableId.HasValue)
            {
                if (selectedRow.ParentTaskId.HasValue)
                {
                    var selectedParent = Response.GetDeliverableTask(selectedRow.ParentTaskId.Value);
                    if (selectedParent != null)
                    {
                        result = selectedParent.FirstMainOrder == selectedRow.MainOrder;
                    }


                }
                else
                {
                    var selectedDeliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);
                    if (selectedDeliverable != null)
                        result = selectedDeliverable.FirstMainOrder == selectedRow.MainOrder;
                }

            }
            return result;
        }
        public static NewTaskResponse? FindParent(this NewDeliverablesResponseList Response, NewTaskResponse child)
        {
            if (!child.TaskParentId.HasValue) return null;

            return Response.GetDeliverableTask(child.TaskParentId.Value);

        }
        public static NewDeliverableGanttTaskRow? FindParentFromOverallList(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow child)
        {
            if (child.IsTask && child.Task.TaskParentId.HasValue)
            {
                return Response.GetDeliverableTaskFromOverallList(child.Task.TaskParentId, true);
            }
            //if (child.DeliverableId.HasValue)
            //{
            //    return Response.GetDeliverableTaskFromOverallList(child.DeliverableId, false);
            //}
            return null!;


        }

        public static bool DisableButtonCanMoveRight(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {
            // Validación inicial: Si no hay fila seleccionada, no se puede mover
            if (selectedRow == null || selectedRow.TaskId == null || selectedRow.DeliverableId == null) return true;


            var deliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);



            // Obtener la lista plana de todos los elementos
            var flatlist = deliverable.OrderedFlatDeliverableTasks;
            if (!flatlist.Any(x => x.Id == selectedRow.TaskId.Value)) return true;//verifica que la fila seleccionada este en la listaplana
            // Obtener el índice de la fila seleccionada en la lista plana


            int currentIndex = flatlist.FindIndex(x => x.Id == selectedRow.TaskId.Value);
            // Si no hay fila anterior, no se puede mover a la derecha
            if (currentIndex == 0) return true;

            // Obtener la fila actual con el indice de la fila seleccionada en la lista plana
            var currentRow = flatlist[currentIndex];


            // Obtener la fila anterior
            var previousRow = flatlist[currentIndex - 1];

            // No permitir moverse hacia la derecha si la fila anterior es igual a la fila seleccionada
            if (currentRow == previousRow) return true;

            // Encontrar el padre actual del SelectedRow
            var currentParent = Response.FindParent(currentRow);

            // Validar si el padre actual tiene subdeliverables
            if (currentParent?.SubTasks != null)
            {
                // Si el SelectedRow es el primer hijo del padre, no permitir el movimiento
                if (currentParent.OrderedSubTasks.FirstOrDefault() == currentRow)
                {
                    return true; // No permitir mover el primer hijo hacia la derecha
                }
            }

            // Verificar si la fila anterior puede actuar como un nuevo padre
            // La fila anterior puede actuar como padre si:
            // 1. Tiene una lista de SubGanttTasks (incluso si está vacía).
            // 2. No es el mismo que el padre actual del SelectedRow.
            if (previousRow.SubTasks == null || previousRow == currentParent)
            {
                return true; // La fila anterior no puede actuar como padre
            }

            // No permitir moverse hacia la derecha si la fila seleccionada ya es hija de la fila anterior
            if (previousRow.SubTasks.Contains(currentRow))
            {
                return true;
            }

            // Permitir el movimiento si todas las condiciones anteriores se cumplen
            return false;
        }
        public static bool DisableButtonCanMoveLeft(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {
            // Validación inicial: Si no hay fila seleccionada, no se puede mover
            if (selectedRow == null || selectedRow.TaskId == null || selectedRow.DeliverableId == null) return true;


            var deliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);



            // Obtener la lista plana de todos los elementos
            var flatlist = deliverable.OrderedFlatDeliverableTasks;
            if (!flatlist.Any(x => x.Id == selectedRow.TaskId.Value)) return true;//verifica que la fila seleccionada este en la listaplana
            // Obtener el índice de la fila seleccionada en la lista plana

            int currentIndex = flatlist.FindIndex(x => x.Id == selectedRow.TaskId.Value);
            // Si no hay fila anterior, no se puede mover a la derecha


            // Obtener la fila actual con el indice de la fila seleccionada en la lista plana
            var currentRow = flatlist[currentIndex];

            var currentParent = Response.FindParent(currentRow);

            if (currentParent == null) return true;

            // Permitir el movimiento si todas las condiciones anteriores se cumplen
            return false;
        }
        /// <summary>
        /// Mueve un elemento hacia arriba. Si está en el primer lugar de una lista de subdeliverables profunda,
        /// sube al nivel del padre anterior y se coloca al final de los subdeliverables del padre del padre actual.
        /// De lo contrario, intercambia su posición con el elemento anterior.
        /// </summary>

        public static void RemoveSubTask(this NewTaskResponse ParentTask, NewTaskResponse subTask)
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
        public static void RemoveSubTask(this NewDeliverableResponse ParentTask, NewTaskResponse subTask)
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

        public static bool MoveUp(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {
            if (selectedRow == null || selectedRow.TaskId == null || selectedRow.DeliverableId == null) return false;
            var deliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);



            // Obtener la lista plana de todos los elementos
            var flatlist = deliverable.OrderedFlatDeliverableTasks;
            if (!flatlist.Any(x => x.Id == selectedRow.TaskId.Value)) return false;//verifica que la fila seleccionada este en la listaplana
            // Obtener el índice de la fila seleccionada en la lista plana

            int selectedIndex = flatlist.FindIndex(x => x.Id == selectedRow.TaskId.Value);
            // Si no hay fila anterior, no se puede mover a la derecha


            // Obtener la fila actual con el indice de la fila seleccionada en la lista plana
            var currentRow = flatlist[selectedIndex];
            var parent = Response.FindParent(currentRow);

            var list = parent == null ? deliverable.OrderedSubTasks : parent.OrderedSubTasks;

            var previous = list.FirstOrDefault(x => x.InternalOrder == currentRow.InternalOrder - 1);
            if (previous == null) return false;

            var currentorder = previous.InternalOrder;
            currentRow.InternalOrder = currentorder;
            previous.InternalOrder = currentorder + 1;

            return true;
        }
        /// <summary>
        /// Mueve un elemento hacia abajo en su lista.
        /// </summary>
        public static bool MoveDown(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {
            if (selectedRow == null || selectedRow.TaskId == null || selectedRow.DeliverableId == null) return false;
            var deliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);



            // Obtener la lista plana de todos los elementos
            var flatlist = deliverable.OrderedFlatDeliverableTasks;
            if (!flatlist.Any(x => x.Id == selectedRow.TaskId.Value)) return false;//verifica que la fila seleccionada este en la listaplana
            // Obtener el índice de la fila seleccionada en la lista plana

            int selectedIndex = flatlist.FindIndex(x => x.Id == selectedRow.TaskId.Value);
            // Si no hay fila anterior, no se puede mover a la derecha


            // Obtener la fila actual con el indice de la fila seleccionada en la lista plana
            var currentRow = flatlist[selectedIndex];
            var parent = Response.FindParent(currentRow);

            var list = parent == null ? deliverable.OrderedSubTasks : parent.OrderedSubTasks;

            var next = list.FirstOrDefault(x => x.InternalOrder == currentRow.InternalOrder + 1);
            if (next == null) return false;

            var currentorder = next.InternalOrder;
            currentRow.InternalOrder = currentorder;
            next.InternalOrder = currentorder - 1;

            return true;
        }
        public static bool MoveLeft(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {
            if (selectedRow == null || selectedRow.TaskId == null || selectedRow.DeliverableId == null) return false;
            var deliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);



            // Obtener la lista plana de todos los elementos
            var flatlist = deliverable.OrderedFlatDeliverableTasks;
            if (!flatlist.Any(x => x.Id == selectedRow.TaskId.Value)) return false;//verifica que la fila seleccionada este en la listaplana
            // Obtener el índice de la fila seleccionada en la lista plana

            int selectedIndex = flatlist.FindIndex(x => x.Id == selectedRow.TaskId.Value);
            // Si no hay fila anterior, no se puede mover a la derecha


            // Obtener la fila actual con el indice de la fila seleccionada en la lista plana
            var currentRow = flatlist[selectedIndex];


            var currentParent = Response.FindParent(currentRow);
            if (currentParent == null) return false; // Ya está en el nivel raíz, no se puede mover más a la izquierda

            // Eliminar el SelectedRow de su ubicación actual
            currentParent.RemoveSubTask(currentRow);

            // Encontrar el padre del padre actual
            var parentOfCurrentParent = Response.FindParent(currentParent);

            if (parentOfCurrentParent != null)
            {
                parentOfCurrentParent.AddSubTask(currentRow);



            }
            else
            {
                deliverable.AddSubtask(currentRow);

            }
            return true;

        }
        public static bool MoveRight(this NewDeliverablesResponseList Response, NewDeliverableGanttTaskRow selectedRow)
        {
            if (selectedRow == null || selectedRow.TaskId == null || selectedRow.DeliverableId == null) return false;
            var deliverable = Response.GetDeliverable(selectedRow.DeliverableId.Value);


            // Obtener la lista plana de todos los elementos
            var flatlist = deliverable.OrderedFlatDeliverableTasks;
            if (!flatlist.Any(x => x.Id == selectedRow.TaskId.Value)) return false;//verifica que la fila seleccionada este en la listaplana
            // Obtener el índice de la fila seleccionada en la lista plana

            int selectedIndex = flatlist.FindIndex(x => x.Id == selectedRow.TaskId.Value);
            // Si no hay fila anterior, no se puede mover a la derecha


            // Obtener la fila actual con el indice de la fila seleccionada en la lista plana
            var currentRow = flatlist[selectedIndex];

            // Si el selectedRow es el primer elemento, no se puede mover a la derecha
            if (selectedIndex == 0) return false;

            // Encontrar el primer ancestro común en la jerarquía
            NewTaskResponse? ancestor = currentRow.FindAncestor(flatlist, selectedIndex);

            if (ancestor == null)
            {
                // Si no hay ances  tro, no se puede mover a la derecha
                return false;
            }

            // Remover el selectedRow de su ubicación actual
            var currentParent = Response.FindParent(currentRow);

            if (currentParent != null)
            {
                currentParent.RemoveSubTask(currentRow);

            }
            else
            {
                deliverable.RemoveSubTask(currentRow);

            }
            ////Si selected row tiene dependencia del ancestor se debe remover porque crea referencia circular
            //var dependantSelectedRow = selectedRow.Dependants.FirstOrDefault(x => x.Id == ancestor.Id);
            //if (dependantSelectedRow != null)
            //{

            //    selectedRow.Dependants.Remove(dependantSelectedRow);
            //}

            // Agregar el selectedRow como subdeliverable del ancestro encontrado

            ancestor.AddSubTask(currentRow);
            return true;
        }
        private static NewTaskResponse? FindAncestor(this NewTaskResponse selectedRow, List<NewTaskResponse> flatList, int currentIndex)
        {
            // Recorrer la lista plana hacia atrás para encontrar el primer ancestro
            for (int i = currentIndex - 1; i >= 0; i--)
            {
                var candidate = flatList[i];

                // Verificar si el candidato es un ancestro válido
                if (IsAncestor(candidate, selectedRow))
                {
                    return candidate;
                }
            }

            return null; // No se encontró un ancestro válido
        }
        private static bool IsAncestor(NewTaskResponse candidate, NewTaskResponse selectedRow)
        {
            // Un candidato es un ancestro si está en un nivel superior en la jerarquía
            // y no es un descendiente del selectedRow.

            var anterior = candidate.WBS.Split('.').Length;
            var selected = selectedRow.WBS.Split('.').Length;
            return anterior == selected;
        }
    }
}
