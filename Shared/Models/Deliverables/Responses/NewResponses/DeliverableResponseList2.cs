﻿using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Responses.NewResponses
{
    public class DeliverableResponseList : UpdateMessageResponse, IResponseAll, IRequest
    {
        public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateEDT;

        public override string Legend => "Deliverables";

        public override string ClassName => StaticClass.Deliverables.ClassName;
        public Guid ProjectId { get; set; }
        public List<DeliverableResponse> Items { get; set; } = new();
        public List<DeliverableResponse> OrderedItems => Items.Count == 0 ? new() : Items.OrderBy(x => x.LabelOrder).ToList();
        public List<DeliverableResponse> FlatOrderedItems => DeliverableHelper.FlattenCompletedOrderedItems(Items);
        //public List<DeliverableResponse> FlattenWithoutDependencesOrSubDeliverablesItems => DeliverableHelper.FlattenWithoutDependencesOrSubDeliverablesOrderedItems(Items);
        //public List<DeliverableResponse> FlattenWithDependencesItems => DeliverableHelper.FlattenWithDependencesOrderedItems(Items);
        //public List<DeliverableResponse> FlattenWithDeliverablesItems => DeliverableHelper.FlattenWithDeliverablesOrderedItems(Items);

        public void Calculate()
        {
            UpdateSOrder();
            CalculateWithoutDependencesOrSubDeliverables();
            CalculateWithDependences();
            CalculateWithDeliverables();
        }
        void CalculateWithoutDependencesOrSubDeliverables()
        {

            var flatlist = DeliverableHelper.FlattenWithoutDependencesOrSubDeliverablesOrderedItems(FlatOrderedItems);
            foreach (var item in flatlist)
            {
                item.CalculateWithoutDependencesOrDeliverables();
            }
        }
        void CalculateWithDependences()
        {

            var flatlist = DeliverableHelper.FlattenWithDependencesOrderedItems(FlatOrderedItems);
            foreach (var item in flatlist)
            {
                CalculateWithDependences(item);
                foreach (var dependence in item.Dependants)
                {
                    CalculateWithDependences(dependence);
                }
            }
        }
        void CalculateWithDependences(DeliverableResponse item)
        {
            var minStartDate = item.Dependants
               .Where(x => x.StartDate.HasValue)
               .Select(x => x.StartDate!.Value)
               .DefaultIfEmpty(DateTime.MaxValue)
               .Min();

            var maxEndDate = item.Dependants
                .Where(x => x.EndDate.HasValue)
                .Select(x => x.EndDate!.Value)
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();


            // Aplicar las reglas según el tipo de dependencia
            switch (item.DependencyType.Id)
            {
                case 0://Start Start
                    item.StartDate = minStartDate;
                    item.CalculateEndDate();
                    break;

                case 1: //Start Finish
                    item.StartDate = maxEndDate;
                    item.CalculateEndDate();
                    break;

                case 2: //Finish Start
                    item.StartDate = maxEndDate.AddDays(1);
                    item.CalculateEndDate();
                    break;

                case 3: //End End
                    item.EndDate = maxEndDate;
                    item.CalculateStartDate();
                    break;

                default:
                    // Si no hay un tipo de dependencia válido, no hacer nada
                    return;
            }

        }
        void CalculateWithDeliverables()
        {

            var flatlist = DeliverableHelper.FlattenWithDeliverablesOrderedItems(Items);
            CalculateWithDeliverables(flatlist);

        }
        void CalculateWithDeliverables(List<DeliverableResponse> list)
        {
            if (list == null || list.Count == 0) return;
            foreach (var row in list)
            {
                CalculateWithDeliverables(row);
                var parent = FindParent(row);

                if (parent != null) CalculateWithDeliverables(parent.SubDeliverables);

            }
        }


        void CalculateWithDeliverables(DeliverableResponse item)
        {
            var minSubStartDate = item.SubDeliverables
                    .Where(sd => sd.StartDate.HasValue)
                    .Select(sd => sd.StartDate!.Value)
                    .DefaultIfEmpty(DateTime.MaxValue)
                    .Min();

            var maxSubEndDate = item.SubDeliverables
                .Where(sd => sd.EndDate.HasValue)
                .Select(sd => sd.EndDate!.Value)
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();

            if (minSubStartDate != DateTime.MaxValue)
            {
                item.StartDate = minSubStartDate;
            }
            if (maxSubEndDate != DateTime.MinValue)
            {
                item.EndDate = maxSubEndDate;
            }
            item.CalculateDurationFromDates();
            foreach (var sub in item.SubDeliverables)
            {

            }
        }
        private void UpdateSOrder()
        {
            int globalOrder = 0; // Inicializar el contador global
            UpdateSOrder(OrderedItems, null, ref globalOrder);
        }
        private void UpdateSOrder(List<DeliverableResponse> list, DeliverableResponse? parent, ref int globalOrder)
        {
            // Validar si la lista es nula o vacía
            if (list == null || list.Count == 0)
                return;

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];

                // Asignar el campo Order basado en la posición en la lista
                item.Order = i + 1;

                // Asignar el campo WBS basado en la jerarquía
                item.WBS = string.IsNullOrEmpty(parent?.WBS)
                    ? item.Order.ToString() // Nivel raíz
                    : $"{parent.WBS}.{item.Order}"; // Subnivel

                // Asignar LabelOrder usando el índice global
                item.LabelOrder = ++globalOrder;

                // Si el elemento tiene subelementos, actualizar recursivamente
                if (item.SubDeliverables != null && item.SubDeliverables.Count > 0)
                {
                    UpdateSOrder(item.SubDeliverables, item, ref globalOrder);
                }
            }
        }

        public DeliverableResponse AddDeliverableResponse(Guid projectid)
        {

            var flatlist = FlatOrderedItems;
            var lasorder = flatlist.Count == 0 ? 1 : flatlist.Last().LabelOrder + 1;
            // Crear un nuevo DeliverableResponse
            var newDeliverable = new DeliverableResponse
            {
                Id = Guid.NewGuid(), // Generar un nuevo Id único
                Name = $"New Deliverable-{lasorder}",
                ProjectId = projectid,
                StartDate = DateTime.Now,
                Duration = "1d",
                DependencyType = TasksRelationTypeEnum.FinishStart,
                EndDate = DateTime.Now.AddDays(1),
                LabelOrder = lasorder

            };


            Items.Add(newDeliverable);

            // Actualizar la jerarquía completa
            UpdateSOrder();

            return newDeliverable;
        }
        public void RemoveDeliverableResponse(DeliverableResponse deliverable)
        {
            // Encontrar el deliverable a eliminar
            Items.Remove(deliverable);

            // Actualizar la jerarquía completa

            UpdateSOrder();
            Calculate();
        }
        public void UpdateStartDate(DeliverableResponse current, DateTime? start)
        {
            current.StartDate = start;
            Calculate();
        }
        public void UpdateEndDate(DeliverableResponse current, DateTime? end)
        {
            current.StartDate = end;
            Calculate();
        }
        public void UpdateDuration(DeliverableResponse current, string duration)
        {
            current.Duration = duration;
            Calculate();
        }
        public void UpdateDependencies(DeliverableResponse current, string dependencies)
        {
            current.Dependants = new();
            if (string.IsNullOrWhiteSpace(dependencies))
            {
                // Si no hay dependencias, limpiar la lista de dependientes y salir
                Console.WriteLine("No dependencies provided.");

                return;
            }

            // Dividir las dependencias por comas y limpiar espacios en blanco
            var dependencyList = dependencies.Split(',')
                                              .Select(dep => dep.Trim())
                                              .Where(dep => !string.IsNullOrEmpty(dep))
                                              .ToList();

            // Limpiar la lista de dependientes actual

            var flatlist = FlatOrderedItems;
            // Validar cada dependencia
            foreach (var dependency in dependencyList)
            {
                var dependencyFromFlatList = flatlist.FirstOrDefault(x => x.LabelOrder.ToString() == dependency);

                if (dependencyFromFlatList == null)
                {
                    Console.WriteLine($"Dependency with LabelOrder {dependency} not found.");
                    continue; // Saltar esta dependencia si no se encuentra
                }

                // Validar que no haya referencias cruzadas o dependencias inválidas
                if (!IsValidDependency(current, dependencyFromFlatList))
                {
                    Console.WriteLine($"Invalid dependency: {current.Name} cannot depend on {dependencyFromFlatList.Name}.");
                    continue; // Saltar esta dependencia si no es válida
                }

                // Agregar la dependencia válida a la lista de dependientes
                current.Dependants.Add(dependencyFromFlatList);
            }

            // Recalcular las fechas basadaCalculateDatesFromDependenciess en las nuevas dependencias
            Calculate();
        }
        private bool IsValidDependency(DeliverableResponse current, DeliverableResponse dependency)
        {
            // 1. Un elemento no puede depender de sí mismo
            if (current.Id == dependency.Id)
            {
                return false;
            }

            // 2. Un elemento no puede depender de su padre ni de ningún ancestro
            if (HasAncestor(current, dependency))
            {
                return false;
            }

            // 3. Detectar ciclos indirectos
            if (HasCircularDependency(current, dependency))
            {
                return false; // Dependencia inválida: ciclo detectado
            }

            return true;
        }
        private bool HasAncestor(DeliverableResponse child, DeliverableResponse potentialAncestor)
        {
            // Buscar el padre del elemento actual
            var parent = FindParent(child);

            // Si no hay padre, detener la recursión
            if (parent == null)
            {
                return false;
            }

            // Si el padre es el ancestro buscado, retornar true
            if (parent.Id == potentialAncestor.Id)
            {
                return true;
            }

            // Continuar buscando recursivamente hacia arriba
            return HasAncestor(parent, potentialAncestor);
        }
        private bool HasCircularDependency(DeliverableResponse current, DeliverableResponse dependency)
        {
            return false;
            // Conjunto para rastrear los nodos visitados durante la búsqueda
            //var visited = new HashSet<Guid>();
            //var stack = new Stack<DeliverableResponse>();

            //// Guardar el estado original de los Dependants
            //var originalDependants = new List<DeliverableResponse>(current.Dependants);

            //current.Dependants.Add(dependency);

            //// Comenzar la búsqueda desde el nodo "current"
            //stack.Push(current);

            //while (stack.Count > 0)
            //{
            //    var node = stack.Pop();

            //    // Si ya hemos visitado este nodo, continuar con el siguiente
            //    if (visited.Contains(node.Id))
            //    {
            //        continue;
            //    }

            //    // Marcar este nodo como visitado
            //    visited.Add(node.Id);

            //    // Agregar todos los dependientes de este nodo al stack para seguir explorando
            //    foreach (var dependent in node.Dependants)
            //    {
            //        // Si encontramos el nodo inicial (current), hay un ciclo
            //        if (dependent.Id == current.Id)
            //        {
            //            current.Dependants = originalDependants;
            //            return true; // Ciclo detectado
            //        }

            //        // Agregar el dependiente al stack para seguir explorando
            //        stack.Push(dependent);
            //    }
            //}

            //// Si no se encontró ningún ciclo, retornar false


            //current.Dependants = originalDependants;
            //return false;
        }
        public DeliverableResponse? FindParent(DeliverableResponse child)
        {

            return FlatOrderedItems.FirstOrDefault(x => x.Id == child.ParentDeliverableId);

        }
        public void AddSubDeliverable(DeliverableResponse parent, DeliverableResponse subDeliverable)
        {
            // Validar y limpiar dependencias que podrían causar ciclos circulares


            parent.AddSubDeliverable(subDeliverable);



        }
        /// <summary>
        /// Determina si un elemento puede moverse hacia arriba.
        /// </summary>
        public bool CanMoveUp(DeliverableResponse selectedRow)
        {

            if (selectedRow == null) return false;

            var currentIndex = FlatOrderedItems.IndexOf(selectedRow);

            return currentIndex > 0;
        }

        /// <summary>
        /// Mueve un elemento hacia arriba. Si está en el primer lugar de una lista de subdeliverables profunda,
        /// sube al nivel del padre anterior y se coloca al final de los subdeliverables del padre del padre actual.
        /// De lo contrario, intercambia su posición con el elemento anterior.
        /// </summary>
        public void MoveUp(DeliverableResponse selectedRow)
        {
            if (selectedRow == null) return;

            // Encontrar el padre actual del SelectedRow
            var currentParent = FindParent(selectedRow);

            if (currentParent != null)
            {
                // Obtener la lista de subdeliverables del padre actual
                var list = currentParent.SubDeliverables;
                int currentIndex = list.IndexOf(selectedRow);

                if (currentIndex == 0)
                {
                    // Si es el primer elemento en la lista, subir al nivel del padre del padre actual
                    currentParent.RemoveSubDeliverable(selectedRow);

                    // Encontrar el padre del padre actual
                    var parentOfCurrentParent = FindParent(currentParent);

                    if (parentOfCurrentParent != null)
                    {
                        AddSubDeliverable(parentOfCurrentParent, selectedRow);

                    }
                    else
                    {
                        // Si no hay padre del padre actual, agregar al nivel raíz
                        Items.Add(selectedRow);
                    }
                }
                else
                {
                    // Si no es el primer elemento, intercambiar con el elemento anterior
                    list.RemoveAt(currentIndex);
                    list.Insert(currentIndex - 1, selectedRow);
                }
            }
            else
            {
                // Si el SelectedRow está en el nivel raíz, realizar el movimiento normal dentro de la lista raíz
                var list = Items;
                int currentIndex = list.IndexOf(selectedRow);

                if (currentIndex > 0)
                {
                    list.RemoveAt(currentIndex);
                    list.Insert(currentIndex - 1, selectedRow);
                }
            }

            // Actualizar la jerarquía completa
            UpdateSOrder();
        }
        /// <summary>
        /// Determina si un elemento puede moverse hacia abajo.
        /// </summary>
        public bool CanMoveDown(DeliverableResponse selectedRow)
        {
            if (selectedRow == null) return false;

            var flatlist = FlatOrderedItems;
            int currentIndex = flatlist.IndexOf(selectedRow);

            return currentIndex < flatlist.Count - 1;
        }

        /// <summary>
        /// Mueve un elemento hacia abajo en su lista.
        /// </summary>
        public void MoveDown(DeliverableResponse selectedRow)
        {
            if (selectedRow == null) return;

            var parent = FindParent(selectedRow);
            var list = parent?.SubDeliverables ?? Items;

            int currentIndex = list.IndexOf(selectedRow);
            if (currentIndex < list.Count - 1)
            {
                list.RemoveAt(currentIndex);
                list.Insert(currentIndex + 1, selectedRow);

                // Actualizar la jerarquía completa
                UpdateSOrder();
            }
        }
        /// <summary>
        /// Determina si un elemento puede moverse hacia la derecha.
        /// </summary>

        public bool CanMoveRight(DeliverableResponse selectedRow)
        {
            // Validación inicial: Si no hay fila seleccionada, no se puede mover
            if (selectedRow == null) return false;

            // Obtener la lista plana de todos los elementos
            var flatlist = FlatOrderedItems;
            int currentIndex = flatlist.IndexOf(selectedRow);

            // Si no hay fila anterior, no se puede mover a la derecha
            if (currentIndex == 0) return false;

            // Obtener la fila anterior
            var previousRow = flatlist[currentIndex - 1];

            // No permitir moverse hacia la derecha si la fila anterior es igual a la fila seleccionada
            if (selectedRow == previousRow) return false;

            // Encontrar el padre actual del SelectedRow
            var currentParent = FindParent(selectedRow);

            // Validar si el padre actual tiene subdeliverables
            if (currentParent?.SubDeliverables != null)
            {
                // Si el SelectedRow es el primer hijo del padre, no permitir el movimiento
                if (currentParent.OrderedSubDeliverables.FirstOrDefault() == selectedRow)
                {
                    return false; // No permitir mover el primer hijo hacia la derecha
                }
            }

            // Verificar si la fila anterior puede actuar como un nuevo padre
            // La fila anterior puede actuar como padre si:
            // 1. Tiene una lista de SubDeliverables (incluso si está vacía).
            // 2. No es el mismo que el padre actual del SelectedRow.
            if (previousRow.SubDeliverables == null || previousRow == currentParent)
            {
                return false; // La fila anterior no puede actuar como padre
            }

            // No permitir moverse hacia la derecha si la fila seleccionada ya es hija de la fila anterior
            if (previousRow.SubDeliverables.Contains(selectedRow))
            {
                return false;
            }

            // Permitir el movimiento si todas las condiciones anteriores se cumplen
            return true;
        }
        /// <summary>
        /// Mueve un elemento hacia la derecha, agregándolo como subelemento del primer padre principal de la fila anterior.
        /// Si no hay padre principal, se agrega como subelemento de la fila anterior (si está en el raíz).
        /// </summary>
        public void MoveRight(DeliverableResponse selectedRow)
        {

            if (selectedRow == null) return;
            var flatlist = FlatOrderedItems;
            // Obtener la lista plana de todos los elementos

            int currentIndex = flatlist.IndexOf(selectedRow);

            // Si el selectedRow es el primer elemento, no se puede mover a la derecha
            if (currentIndex == 0) return;

            // Encontrar el primer ancestro común en la jerarquía
            DeliverableResponse? ancestor = FindAncestor(selectedRow, flatlist, currentIndex);

            if (ancestor == null)
            {
                // Si no hay ancestro, no se puede mover a la derecha
                return;
            }

            // Remover el selectedRow de su ubicación actual
            var currentParent = FindParent(selectedRow);
            if (currentParent != null)
            {
                currentParent.RemoveSubDeliverable(selectedRow);
            }
            else
            {
                Items.Remove(selectedRow);
            }

            // Agregar el selectedRow como subdeliverable del ancestro encontrado
            AddSubDeliverable(ancestor, selectedRow);

            // Actualizar la jerarquía completa
            UpdateSOrder();
        }
        public void MoveLeft(DeliverableResponse selectedRow)
        {
            if (selectedRow == null) return;

            // Encontrar el padre actual del SelectedRow
            var currentParent = FindParent(selectedRow);
            if (currentParent == null) return; // Ya está en el nivel raíz, no se puede mover más a la izquierda

            // Eliminar el SelectedRow de su ubicación actual
            currentParent.RemoveSubDeliverable(selectedRow);

            // Encontrar el padre del padre actual
            var parentOfCurrentParent = FindParent(currentParent);

            if (parentOfCurrentParent != null)
            {
                AddSubDeliverable(parentOfCurrentParent, selectedRow);


            }
            else
            {
                // Si no hay padre del padre actual, agregar al nivel raíz
                Items.Add(selectedRow);
            }

            // Actualizar la jerarquía completa
            UpdateSOrder();
        }
        private DeliverableResponse? FindAncestor(DeliverableResponse selectedRow, List<DeliverableResponse> flatList, int currentIndex)
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
        private bool IsAncestor(DeliverableResponse candidate, DeliverableResponse selectedRow)
        {
            // Un candidato es un ancestro si está en un nivel superior en la jerarquía
            // y no es un descendiente del selectedRow.

            var anterior = candidate.WBS.Split('.').Length;
            var selected = selectedRow.WBS.Split('.').Length;
            return anterior == selected;
        }
        public void UpdateRelationType(DeliverableResponse current, TasksRelationTypeEnum type)
        {
            current.DependencyType = type;

            Calculate();
        }
    }
}
