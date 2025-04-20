using Shared.Enums.TasksRelationTypes;
using System.Text;

namespace Shared.Models.GanttTasks.Responses
{
    public class DeliverableWithGanttTaskResponse
    {
        public Guid ProjectId { get; set; }
        public Guid DeliverableId { get; set; }
        public string Name { get; set; } = string.Empty;
        // Propiedad calculada para las líneas de texto
        private List<string> _textLines = null!;
        public List<string> TextLines(int maxWidth, int averageCharWidth = 7)
        {
            if (_textLines == null)
            {
                _textLines = TextHelper.SplitTextIntoLines($"{WBS} - {Name}", maxWidth, averageCharWidth);
            }
            return _textLines;
        }
        public int LabelOrder { get; set; }
        public string WBS { get; set; } = string.Empty;
        public DateTime StartDate => FlatOrderedItems.Count == 0 ? DateTime.Now : FlatOrderedItems.Where(x => x.StartDate.HasValue)
               .Select(x => x.StartDate!.Value)
               .DefaultIfEmpty(DateTime.MaxValue)
               .Min();
        public DateTime EndDate => FlatOrderedItems.Count == 0 ? DateTime.Now : FlatOrderedItems.Where(x => x.EndDate.HasValue)
                .Select(x => x.EndDate!.Value)
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
        public TimeSpan Duration => EndDate - StartDate;

        public bool IsExpanded { get; set; }
        public List<GanttTaskResponse> Items { get; set; } = new();
        public int LastOrder => Items.Count == 0 ? 1 : OrderedItems.Last().Order;
        public List<GanttTaskResponse> OrderedItems => Items.Count == 0 ? new() : Items.OrderBy(x => x.Order).ToList();
        public List<GanttTaskResponse> FlatOrderedItems => GanttTaskHelper.FlattenCompletedOrderedItems(Items);

        public double TotalBudget => OrderedItems.Count == 0 ? 0 : OrderedItems.Sum(x => x.TotalBudget);
        public string sTotalBudget => string.Format(new CultureInfo("en-US"), "{0:C0}", TotalBudget);
        public void Calculate()
        {
            UpdateSOrder();
            CalculateWithoutDependencesOrSubGanttTasks(Items);
            CalculateWithDependences(Items);
            CalculateWithGanttTasks(Items);
        }

        void CalculateWithoutDependencesOrSubGanttTasks(List<GanttTaskResponse> items)
        {
            foreach (var item in items)
            {
                if (item.SubGanttTasks.Any())
                {
                    foreach (var row in item.SubGanttTasks)
                    {
                        CalculateWithoutDependencesOrSubGanttTasks(item.SubGanttTasks);
                    }
                }
                else
                {
                    if (!item.Dependants.Any())
                    {
                        item.CalculateEndDate();
                    }

                }


            }
        }

        void CalculateWithDependences(List<GanttTaskResponse> items)
        {
            foreach (var item in items)
            {
                if (item.SubGanttTasks.Any())
                {
                    foreach (var row in item.SubGanttTasks)
                    {
                        CalculateWithDependences(item.SubGanttTasks);
                    }
                }
                else
                {
                    if (item.Dependants.Any())
                        CalculateWithDependences(item);
                }


            }
        }


        private List<GanttTaskResponse> GetListFromFlaten(List<GanttTaskResponse> items)
        {
            return items.Select(x => FlatOrderedItems.FirstOrDefault(y => y.Id == x.Id)!).ToList();
        }

        void CalculateWithDependences(GanttTaskResponse item)
        {

            var dependencies = GetListFromFlaten(item.Dependants);


            var minStartDate = dependencies
               .Where(x => x.StartDate.HasValue)
               .Select(x => x.StartDate!.Value)
               .DefaultIfEmpty(DateTime.MaxValue)
               .Min();

            var maxEndDate = dependencies
                .Where(x => x.EndDate.HasValue)
                .Select(x => x.EndDate!.Value)
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();


            // Aplicar las reglas según el tipo de dependencia
            switch (item.DependencyType.Id)
            {
                case 0://Start Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya comenzado
                    item.SetStartDateWithLag(minStartDate);
                    item.CalculateEndDate();
                    break;

                case 1: //Start Finish  La tarea dependiente debe terminar cuando la tarea precedente comience.
                    item.SetEndDateWithlag(minStartDate);
                    item.CalculateStartDate();
                    break;

                case 2: //Finish Start La tarea dependiente puede comenzar solo cuando la tarea precedente haya terminado.
                    item.SetStartDateWithLag(maxEndDate.AddDays(1));
                    item.CalculateEndDate();
                    break;

                case 3: //End End  La tarea dependiente debe terminar al mismo tiempo que la tarea precedente.
                    item.SetEndDateWithlag(maxEndDate);
                    item.CalculateStartDate();
                    break;

                default:
                    // Si no hay un tipo de dependencia válido, no hacer nada
                    return;
            }
            if (item.DependantId != null)
            {
                var dependant = FlatOrderedItems.FirstOrDefault(x => x.Id == item.DependantId);
                if (dependant != null)
                {
                    CalculateWithDependences(dependant);
                }
            }

        }



        public void CalculateWithGanttTasks(List<GanttTaskResponse> items)
        {
            // Calcular las fechas para cada elemento en el orden correcto
            foreach (var item in items)
            {
                // Procesar los subdeliverables recursivamente
                if (item.SubGanttTasks != null && item.SubGanttTasks.Any())
                {
                    CalculateWithGanttTasks(item.OrderedSubGanttTasks);
                    CalculateDatesForItem(item);
                }

            }
        }

        private void CalculateDatesForItem(GanttTaskResponse item)
        {
            var subdeliverables = GetListFromFlaten(item.SubGanttTasks);
            // Si tiene subdeliverables, calcular StartDate y EndDate basados en ellos
            if (subdeliverables.Any())
            {
                var minSubStartDate = subdeliverables
                    .Where(sd => sd.StartDate.HasValue)
                    .Select(sd => sd.StartDate!.Value)
                    .DefaultIfEmpty(DateTime.MaxValue)
                    .Min();

                var maxSubEndDate = subdeliverables
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
            }


        }

        public void UpdateSOrder()
        {
            int globalOrder = 0; // Inicializar el contador global
            UpdateSOrder(OrderedItems, null, LabelOrder, ref globalOrder);
        }
        private void UpdateSOrder(List<GanttTaskResponse> list, GanttTaskResponse? parent, int deliverableOrder, ref int globalOrder)
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
                    ? $"{LabelOrder}.{item.Order.ToString()}"  // Nivel raíz
                    : $"{parent.WBS}.{item.Order}"; // Subnivel

                // Asignar LabelOrder usando el índice global
                item.LabelOrder = ++globalOrder;

                // Si el elemento tiene subelementos, actualizar recursivamente
                if (item.SubGanttTasks != null && item.SubGanttTasks.Count > 0)
                {
                    UpdateSOrder(item.OrderedSubGanttTasks, item, LabelOrder, ref globalOrder);
                }
            }
        }
        public GanttTaskResponse AddGanttTaskResponse(Guid deliverableId, GanttTaskResponse? selectedRow)
        {
            // Crear un nuevo GanttTaskResponse
            var newGanttTask = new GanttTaskResponse
            {
                Id = Guid.NewGuid(), // Generar un nuevo Id único

                DeliverableId = deliverableId,
                StartDate = DateTime.Now,
                Duration = "1d",
                DependencyType = TasksRelationTypeEnum.FinishStart,
                EndDate = DateTime.Now.AddDays(1),
                IsEditing = true,
                IsCreating = true,

            };
            if (selectedRow == null)
            {
                // Si selectedRow es null, lo ponemos de ultimo en Items
                var lastOrder = OrderedItems.Count == 0 ? 1 : OrderedItems.Last().Order + 1;
                var lastLabelOrder = FlatOrderedItems.Count == 0 ? 1 : FlatOrderedItems.Last().LabelOrder + 1;
                newGanttTask.Name = $"New GanttTask-{lastOrder}";
                newGanttTask.Order = lastOrder;
                newGanttTask.LabelOrder = lastLabelOrder;
                Items.Add(newGanttTask);
                Calculate();
                return newGanttTask;
            }
            else
            {
                //Nos aseguramos que el item este dentro de los objetos
                var item = FlatOrderedItems.FirstOrDefault(x => x.Id == selectedRow.Id);
                if (item != null)
                {
                    if (item.SubGanttTasks.Any())
                    {
                        // Si selectedRow tiene subdeliverables lo pnemos de primero 
                        newGanttTask.Order = 1;
                        newGanttTask.ParentGanttTaskId = item.Id;
                        newGanttTask.Name = $"SubGanttTask-1";
                        item.OrderedSubGanttTasks.ForEach(x => { x.Order += 1; });
                        item.SubGanttTasks.Add(newGanttTask);
                        Calculate();

                        return newGanttTask;
                    }
                    else
                    {
                        // Si selectedRow es un subdeliverable lo ponemos despues del selectedRow
                        var parentedItem = FindParent(item);
                        if (parentedItem != null)
                        {
                            newGanttTask.Order = item.Order + 1;
                            newGanttTask.ParentGanttTaskId = parentedItem.Id;
                            newGanttTask.Name = $"SubGanttTask-{item.Order + 1}";

                            var subdeliverablesparent = parentedItem.OrderedSubGanttTasks.Where(x => x.Order > item.Order).ToList();
                            subdeliverablesparent.ForEach(x => { x.Order += 1; });
                            parentedItem.SubGanttTasks.Add(newGanttTask);
                            Calculate();
                            return newGanttTask;
                        }
                        else
                        {
                            // Si selectedRow no tiene subdeliverables y no tiene parent lo pnemos de primero 
                            var lastOrder = OrderedItems.Count == 0 ? 1 : OrderedItems.Last().Order + 1;
                            var lastLabelOrder = FlatOrderedItems.Count == 0 ? 1 : FlatOrderedItems.Last().LabelOrder + 1;
                            newGanttTask.Name = $"New GanttTask-{lastOrder}";
                            newGanttTask.Order = lastOrder;
                            newGanttTask.LabelOrder = lastLabelOrder;
                            Items.Add(newGanttTask);
                            Calculate();
                            return newGanttTask;
                        }


                    }
                }


            }





            return null!;
        }
        public GanttTaskResponse AddGanttTaskResponse(GanttTaskResponse newGanttTask, GanttTaskResponse? selectedRow)
        {
            // Crear un nuevo GanttTaskResponse
            
            if (selectedRow == null)
            {
                // Si selectedRow es null, lo ponemos de ultimo en Items
                var lastOrder = OrderedItems.Count == 0 ? 1 : OrderedItems.Last().Order + 1;
                var lastLabelOrder = FlatOrderedItems.Count == 0 ? 1 : FlatOrderedItems.Last().LabelOrder + 1;
                newGanttTask.Name = $"New GanttTask-{lastOrder}";
                newGanttTask.Order = lastOrder;
                newGanttTask.LabelOrder = lastLabelOrder;
                Items.Add(newGanttTask);
                Calculate();
                return newGanttTask;
            }
            else
            {
                //Nos aseguramos que el item este dentro de los objetos
                var item = FlatOrderedItems.FirstOrDefault(x => x.Id == selectedRow.Id);
                if (item != null)
                {
                    if (item.SubGanttTasks.Any())
                    {
                        // Si selectedRow tiene subdeliverables lo pnemos de primero 
                        newGanttTask.Order = 1;
                        newGanttTask.ParentGanttTaskId = item.Id;
                        newGanttTask.Name = $"SubGanttTask-1";
                        item.OrderedSubGanttTasks.ForEach(x => { x.Order += 1; });
                        item.SubGanttTasks.Add(newGanttTask);
                        Calculate();

                        return newGanttTask;
                    }
                    else
                    {
                        // Si selectedRow es un subdeliverable lo ponemos despues del selectedRow
                        var parentedItem = FindParent(item);
                        if (parentedItem != null)
                        {
                            newGanttTask.Order = item.Order + 1;
                            newGanttTask.ParentGanttTaskId = parentedItem.Id;
                            newGanttTask.Name = $"SubGanttTask-{item.Order + 1}";

                            var subdeliverablesparent = parentedItem.OrderedSubGanttTasks.Where(x => x.Order > item.Order).ToList();
                            subdeliverablesparent.ForEach(x => { x.Order += 1; });
                            parentedItem.SubGanttTasks.Add(newGanttTask);
                            Calculate();
                            return newGanttTask;
                        }
                        else
                        {
                            // Si selectedRow no tiene subdeliverables y no tiene parent lo pnemos de primero 
                            var lastOrder = OrderedItems.Count == 0 ? 1 : OrderedItems.Last().Order + 1;
                            var lastLabelOrder = FlatOrderedItems.Count == 0 ? 1 : FlatOrderedItems.Last().LabelOrder + 1;
                            newGanttTask.Name = $"New GanttTask-{lastOrder}";
                            newGanttTask.Order = lastOrder;
                            newGanttTask.LabelOrder = lastLabelOrder;
                            Items.Add(newGanttTask);
                            Calculate();
                            return newGanttTask;
                        }


                    }
                }


            }





            return null!;
        }
        public void RemoveGanttTaskResponse(GanttTaskResponse deliverable)
        {
            var parent = FindParent(deliverable);
            if (parent == null)
            {
                Items.Remove(deliverable);
            }
            else
            {
                parent.SubGanttTasks.Remove(deliverable);
            }
            Calculate();

        }

        public void UpdateStartDate(GanttTaskResponse current, DateTime? start)
        {
            current.StartDate = start;
            Calculate();
        }
        public void UpdateEndDate(GanttTaskResponse current, DateTime? end)
        {
            current.StartDate = end;
            Calculate();
        }
        public void UpdateDuration(GanttTaskResponse current, string duration)
        {
            current.Duration = duration;
            Calculate();
        }
        public string UpdateDependencies(GanttTaskResponse current, string dependencies)
        {
            current.Dependants.ForEach(dep =>
            {
                dep.DependantId = null;

            });
            current.Dependants = new();
            StringBuilder result = new StringBuilder();
            if (string.IsNullOrWhiteSpace(dependencies))
            {
                // Si no hay dependencias, limpiar la lista de dependientes y salir
                Console.WriteLine("No dependencies provided.");
                result.Append("No dependencies provided.");
                Calculate();
                return string.Empty;
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
                    result.Append($"Dependency #{dependency} not found.");
                    return result.ToString();

                }

                // Validar que no haya referencias cruzadas o dependencias inválidas
                var resultDependency = IsValidDependency(current, dependencyFromFlatList);
                if (!string.IsNullOrEmpty(resultDependency))
                {
                    Console.WriteLine(resultDependency);

                    return resultDependency;

                }

                dependencyFromFlatList.DependantId = current.Id;
                // Agregar la dependencia válida a la lista de dependientes
                current.Dependants.Add(dependencyFromFlatList);
            }

            // Recalcular las fechas basadaCalculateDatesFromDependenciess en las nuevas dependencias
            Calculate();
            return string.Empty;
        }
        public void UpdateLag(GanttTaskResponse current, string lag)
        {
            current.Lag = lag;
            Calculate();

        }
        private string IsValidDependency(GanttTaskResponse current, GanttTaskResponse dependency)
        {
            // 1. Un elemento no puede depender de sí mismo
            if (current.Id == dependency.Id)
            {
                return "Element can not depend from itself";
            }

            // 2. Un elemento no puede depender de su padre ni de ningún ancestro
            if (HasAncestor(current, dependency))
            {
                return "Element can not depend from any parent";
            }

            // 3. Detectar ciclos indirectos
            if (HasCircularDependency(current, dependency))
            {
                return "Reference circular detected"; // Dependencia inválida: ciclo detectado
            }

            return string.Empty;
        }
        private bool HasAncestor(GanttTaskResponse child, GanttTaskResponse potentialAncestor)
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
        private bool HasCircularDependency(GanttTaskResponse current, GanttTaskResponse dependency)
        {
            //return false;
            //Conjunto para rastrear los nodos visitados durante la búsqueda
            var visited = new HashSet<Guid?>();
            var stack = new Stack<GanttTaskResponse>();

            // Guardar el estado original de los Dependants
            var originalDependants = new List<GanttTaskResponse>(current.Dependants);

            current.Dependants.Add(dependency);

            // Comenzar la búsqueda desde el nodo "current"
            stack.Push(current);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                // Si ya hemos visitado este nodo, continuar con el siguiente
                if (visited.Contains(node.Id))
                {
                    continue;
                }

                // Marcar este nodo como visitado
                visited.Add(node.Id);

                // Agregar todos los dependientes de este nodo al stack para seguir explorando
                foreach (var dependent in node.Dependants)
                {
                    // Si encontramos el nodo inicial (current), hay un ciclo
                    if (dependent.Id == current.Id)
                    {
                        current.Dependants = originalDependants;
                        return true; // Ciclo detectado
                    }

                    // Agregar el dependiente al stack para seguir explorando
                    stack.Push(dependent);
                }
            }

            // Si no se encontró ningún ciclo, retornar false


            current.Dependants = originalDependants;
            return false;
        }
        public GanttTaskResponse? FindParent(GanttTaskResponse child)
        {

            return FlatOrderedItems.FirstOrDefault(x => x.Id == child.ParentGanttTaskId);

        }
        public void AddSubGanttTask(GanttTaskResponse parent, GanttTaskResponse subGanttTask)
        {
            // Validar y limpiar dependencias que podrían causar ciclos circulares


            parent.AddSubGanttTask(subGanttTask);

            parent.Dependants = new();

        }
        /// <summary>
        /// Determina si un elemento puede moverse hacia arriba.
        /// </summary>
        public bool CanMoveUp(GanttTaskResponse selectedRow)
        {

            if (selectedRow == null) return false;
            var flatlist = FlatOrderedItems;
            if (!flatlist.Any(x => x.Id == selectedRow.Id)) return false;
            var currentIndex = flatlist.IndexOf(selectedRow);

            return currentIndex > 0;
        }

        /// <summary>
        /// Mueve un elemento hacia arriba. Si está en el primer lugar de una lista de subdeliverables profunda,
        /// sube al nivel del padre anterior y se coloca al final de los subdeliverables del padre del padre actual.
        /// De lo contrario, intercambia su posición con el elemento anterior.
        /// </summary>
        public void MoveUp(GanttTaskResponse selectedRow)
        {
            if (selectedRow == null) return;

            // Encontrar el padre actual del SelectedRow
            var currentParent = FindParent(selectedRow);

            if (currentParent != null)
            {
                // Obtener la lista de subdeliverables del padre actual
                var list = currentParent.OrderedSubGanttTasks;

                int currentIndex = list.IndexOf(selectedRow);

                if (currentIndex == 0)
                {
                    // Si es el primer elemento en la lista, subir al nivel del padre del padre actual
                    currentParent.RemoveSubGanttTask(selectedRow);

                    // Encontrar el padre del padre actual
                    var parentOfCurrentParent = FindParent(currentParent);

                    if (parentOfCurrentParent != null)
                    {
                        AddSubGanttTask(parentOfCurrentParent, selectedRow);

                    }
                    else
                    {
                        selectedRow.Order = LastOrder + 1;
                        // Si no hay padre del padre actual, agregar al nivel raíz
                        Items.Add(selectedRow);
                    }
                }
                else
                {
                    var previous = list.FirstOrDefault(x => x.Order == selectedRow.Order - 1);


                    if (previous != null)
                    {

                        var currentorder = previous.Order;
                        selectedRow.Order = currentorder;
                        previous.Order = currentorder + 1;
                    }
                    // Si no es el primer elemento, intercambiar con el elemento anterior

                }
            }
            else
            {
                // Si el SelectedRow está en el nivel raíz, realizar el movimiento normal dentro de la lista raíz
                var list = Items;

                var previous = list.FirstOrDefault(x => x.Order == selectedRow.Order - 1);


                if (previous != null)
                {

                    var currentorder = previous.Order;
                    selectedRow.Order = currentorder;
                    previous.Order = currentorder + 1;
                }
            }

            // Actualizar la jerarquía completa

        }
        /// <summary>
        /// Determina si un elemento puede moverse hacia abajo.
        /// </summary>
        public bool CanMoveDown(GanttTaskResponse selectedRow)
        {
            if (selectedRow == null) return false;

            var flatlist = FlatOrderedItems;
            if (!flatlist.Any(x => x.Id == selectedRow.Id)) return false;
            int currentIndex = flatlist.IndexOf(selectedRow);

            return currentIndex < flatlist.Count - 1;
        }

        /// <summary>
        /// Mueve un elemento hacia abajo en su lista.
        /// </summary>
        public void MoveDown(GanttTaskResponse selectedRow)
        {
            if (selectedRow == null) return;

            var parent = FindParent(selectedRow);
            var list = parent?.OrderedSubGanttTasks ?? Items;

            var next = list.FirstOrDefault(x => x.Order == selectedRow.Order + 1);
            if (next != null)
            {

                var currentorder = next.Order;
                selectedRow.Order = currentorder;
                next.Order = currentorder - 1;
            }

        }
        /// <summary>
        /// Determina si un elemento puede moverse hacia la derecha.
        /// </summary>

        public bool CanMoveRight(GanttTaskResponse selectedRow)
        {
            // Validación inicial: Si no hay fila seleccionada, no se puede mover
            if (selectedRow == null) return false;

            // Obtener la lista plana de todos los elementos
            var flatlist = FlatOrderedItems;
            if (!flatlist.Any(x => x.Id == selectedRow.Id)) return false;

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
            if (currentParent?.SubGanttTasks != null)
            {
                // Si el SelectedRow es el primer hijo del padre, no permitir el movimiento
                if (currentParent.OrderedSubGanttTasks.FirstOrDefault() == selectedRow)
                {
                    return false; // No permitir mover el primer hijo hacia la derecha
                }
            }

            // Verificar si la fila anterior puede actuar como un nuevo padre
            // La fila anterior puede actuar como padre si:
            // 1. Tiene una lista de SubGanttTasks (incluso si está vacía).
            // 2. No es el mismo que el padre actual del SelectedRow.
            if (previousRow.SubGanttTasks == null || previousRow == currentParent)
            {
                return false; // La fila anterior no puede actuar como padre
            }

            // No permitir moverse hacia la derecha si la fila seleccionada ya es hija de la fila anterior
            if (previousRow.SubGanttTasks.Contains(selectedRow))
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
        public void MoveRight(GanttTaskResponse selectedRow)
        {

            if (selectedRow == null) return;
            var flatlist = FlatOrderedItems;
            // Obtener la lista plana de todos los elementos

            int currentIndex = flatlist.IndexOf(selectedRow);

            // Si el selectedRow es el primer elemento, no se puede mover a la derecha
            if (currentIndex == 0) return;

            // Encontrar el primer ancestro común en la jerarquía
            GanttTaskResponse? ancestor = FindAncestor(selectedRow, flatlist, currentIndex);

            if (ancestor == null)
            {
                // Si no hay ancestro, no se puede mover a la derecha
                return;
            }

            // Remover el selectedRow de su ubicación actual
            var currentParent = FindParent(selectedRow);
            if (currentParent != null)
            {
                currentParent.RemoveSubGanttTask(selectedRow);
            }
            else
            {

                Items.Remove(selectedRow);
            }
            //Si selected row tiene dependencia del ancestor se debe remover porque crea referencia circular
            var dependantSelectedRow = selectedRow.Dependants.FirstOrDefault(x => x.Id == ancestor.Id);
            if (dependantSelectedRow != null)
            {
          
                selectedRow.Dependants.Remove(dependantSelectedRow);
            }

            // Agregar el selectedRow como subdeliverable del ancestro encontrado
            AddSubGanttTask(ancestor, selectedRow);

            // Actualizar la jerarquía completa

        }
        public void MoveLeft(GanttTaskResponse selectedRow)
        {
            if (selectedRow == null) return;

            // Encontrar el padre actual del SelectedRow
            var currentParent = FindParent(selectedRow);
            if (currentParent == null) return; // Ya está en el nivel raíz, no se puede mover más a la izquierda

            // Eliminar el SelectedRow de su ubicación actual
            currentParent.RemoveSubGanttTask(selectedRow);

            // Encontrar el padre del padre actual
            var parentOfCurrentParent = FindParent(currentParent);

            if (parentOfCurrentParent != null)
            {
                AddSubGanttTask(parentOfCurrentParent, selectedRow);


            }
            else
            {
                selectedRow.Order = LastOrder + 1;
                // Si no hay padre del padre actual, agregar al nivel raíz
                Items.Add(selectedRow);
            }

            // Actualizar la jerarquía completa

        }
        private GanttTaskResponse? FindAncestor(GanttTaskResponse selectedRow, List<GanttTaskResponse> flatList, int currentIndex)
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
        private bool IsAncestor(GanttTaskResponse candidate, GanttTaskResponse selectedRow)
        {
            // Un candidato es un ancestro si está en un nivel superior en la jerarquía
            // y no es un descendiente del selectedRow.

            var anterior = candidate.WBS.Split('.').Length;
            var selected = selectedRow.WBS.Split('.').Length;
            return anterior == selected;
        }
        public void UpdateRelationType(GanttTaskResponse current, TasksRelationTypeEnum type)
        {
            current.DependencyType = type;

            Calculate();
        }



    }
}
