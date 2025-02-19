using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.FileResults.Generics.Request;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Shared.StaticClasses.StaticClass;

namespace Shared.Models.Deliverables.Responses
{
    //public class Sample : UpdateMessageResponse, IResponseAll, IRequest
    //{
    //    public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateEDT;

    //    public override string Legend => "Deliverables";

    //    public override string ClassName => StaticClass.Deliverables.ClassName;
    //    public Guid ProjectId { get; set; }
    //}
    //public class DeliverableResponse2List2 : UpdateMessageResponse, IResponseAll, IRequest
    //{
    //    public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateEDT;

    //    public override string Legend => "Deliverables";

    //    public override string ClassName => StaticClass.Deliverables.ClassName;
    //    public Guid ProjectId { get; set; }

    //    /// <summary>
    //    /// Lista principal de elementos.
    //    /// </summary>
    //    public List<DeliverableResponse2> Items { get; set; } = new();
    //    public List<DeliverableResponse2> OrderedItems => Items.Count == 0 ? new() : Items.OrderBy(x => x.LabelOrder).ToList();

    //    /// <summary>
    //    /// Obtiene una lista plana de todos los elementos, incluidos los elementos de las sublistas.
    //    /// </summary>
    //    public List<DeliverableResponse2> FlatList => DeliverableHelper.FlattenHierarchy(Items);


    //    /// <summary>
    //    /// Agrega un nuevo DeliverableResponse2 a la lista principal o a una sublista.
    //    /// </summary>
    //    public DeliverableResponse2 AddDeliverableResponse2(Guid projectid)
    //    {
    //        var lasorder = FlatList.Count == 0 ? 1 : FlatList.Last().LabelOrder + 1;
    //        // Crear un nuevo DeliverableResponse2
    //        var newDeliverable = new DeliverableResponse2
    //        {
    //            Id = Guid.NewGuid(), // Generar un nuevo Id único
    //            Name = $"New Deliverable-{lasorder}",
    //            ProjectId = projectid,
    //            StartDate = DateTime.Now,
    //            DurationInput = "1d",
    //            DependencyType = TasksRelationTypeEnum.FinishStart,
    //            EndDate = DateTime.Now.AddDays(1),
    //            LabelOrder = lasorder
    //        };


    //        Items.Add(newDeliverable);

    //        // Actualizar la jerarquía completa
    //        UpdateSOrder();

    //        return newDeliverable;
    //    }

    //    /// <summary>
    //    /// Determina si un elemento puede moverse hacia arriba.
    //    /// </summary>
    //    public bool CanMoveUp(DeliverableResponse2 selectedRow)
    //    {
    //        if (selectedRow == null) return false;

    //        var currentIndex = FlatList.IndexOf(selectedRow);

    //        return currentIndex > 0;
    //    }

    //    /// <summary>
    //    /// Mueve un elemento hacia arriba. Si está en el primer lugar de una lista de subdeliverables profunda,
    //    /// sube al nivel del padre anterior y se coloca al final de los subdeliverables del padre del padre actual.
    //    /// De lo contrario, intercambia su posición con el elemento anterior.
    //    /// </summary>
    //    public void MoveUp(DeliverableResponse2 selectedRow)
    //    {
    //        if (selectedRow == null) return;

    //        // Encontrar el padre actual del SelectedRow
    //        var currentParent = FindParent(selectedRow);

    //        if (currentParent != null)
    //        {
    //            // Obtener la lista de subdeliverables del padre actual
    //            var list = currentParent.SubDeliverables;
    //            int currentIndex = list.IndexOf(selectedRow);

    //            if (currentIndex == 0)
    //            {
    //                // Si es el primer elemento en la lista, subir al nivel del padre del padre actual
    //                currentParent.RemoveSubDeliverable(selectedRow);

    //                // Encontrar el padre del padre actual
    //                var parentOfCurrentParent = FindParent(currentParent);

    //                if (parentOfCurrentParent != null)
    //                {
    //                    AddSubDeliverable(parentOfCurrentParent,selectedRow);
                        
    //                }
    //                else
    //                {
    //                    // Si no hay padre del padre actual, agregar al nivel raíz
    //                    Items.Add(selectedRow);
    //                }
    //            }
    //            else
    //            {
    //                // Si no es el primer elemento, intercambiar con el elemento anterior
    //                list.RemoveAt(currentIndex);
    //                list.Insert(currentIndex - 1, selectedRow);
    //            }
    //        }
    //        else
    //        {
    //            // Si el SelectedRow está en el nivel raíz, realizar el movimiento normal dentro de la lista raíz
    //            var list = Items;
    //            int currentIndex = list.IndexOf(selectedRow);

    //            if (currentIndex > 0)
    //            {
    //                list.RemoveAt(currentIndex);
    //                list.Insert(currentIndex - 1, selectedRow);
    //            }
    //        }

    //        // Actualizar la jerarquía completa
    //        UpdateSOrder();
    //    }
    //    /// <summary>
    //    /// Determina si un elemento puede moverse hacia abajo.
    //    /// </summary>
    //    public bool CanMoveDown(DeliverableResponse2 selectedRow)
    //    {
    //        if (selectedRow == null) return false;

    //        int currentIndex = FlatList.IndexOf(selectedRow);

    //        return currentIndex < FlatList.Count - 1;
    //    }

    //    /// <summary>
    //    /// Mueve un elemento hacia abajo en su lista.
    //    /// </summary>
    //    public void MoveDown(DeliverableResponse2 selectedRow)
    //    {
    //        if (selectedRow == null) return;

    //        var parent = FindParent(selectedRow);
    //        var list = parent?.SubDeliverables ?? Items;

    //        int currentIndex = list.IndexOf(selectedRow);
    //        if (currentIndex < list.Count - 1)
    //        {
    //            list.RemoveAt(currentIndex);
    //            list.Insert(currentIndex + 1, selectedRow);

    //            // Actualizar la jerarquía completa
    //            UpdateSOrder();
    //        }
    //    }
    //    /// <summary>
    //    /// Determina si un elemento puede moverse hacia la derecha.
    //    /// </summary>

    //    public bool CanMoveRight(DeliverableResponse2 selectedRow)
    //    {
    //        // Validación inicial: Si no hay fila seleccionada, no se puede mover
    //        if (selectedRow == null) return false;

    //        // Obtener la lista plana de todos los elementos
    //        var flatList = FlatList;
    //        int currentIndex = flatList.IndexOf(selectedRow);

    //        // Si no hay fila anterior, no se puede mover a la derecha
    //        if (currentIndex == 0) return false;

    //        // Obtener la fila anterior
    //        var previousRow = flatList[currentIndex - 1];

    //        // No permitir moverse hacia la derecha si la fila anterior es igual a la fila seleccionada
    //        if (selectedRow == previousRow) return false;

    //        // Encontrar el padre actual del SelectedRow
    //        var currentParent = FindParent(selectedRow);

    //        // Validar si el padre actual tiene subdeliverables
    //        if (currentParent?.SubDeliverables != null)
    //        {
    //            // Si el SelectedRow es el primer hijo del padre, no permitir el movimiento
    //            if (currentParent.OrderedSubDeliverables.FirstOrDefault() == selectedRow)
    //            {
    //                return false; // No permitir mover el primer hijo hacia la derecha
    //            }
    //        }

    //        // Verificar si la fila anterior puede actuar como un nuevo padre
    //        // La fila anterior puede actuar como padre si:
    //        // 1. Tiene una lista de SubDeliverables (incluso si está vacía).
    //        // 2. No es el mismo que el padre actual del SelectedRow.
    //        if (previousRow.SubDeliverables == null || previousRow == currentParent)
    //        {
    //            return false; // La fila anterior no puede actuar como padre
    //        }

    //        // No permitir moverse hacia la derecha si la fila seleccionada ya es hija de la fila anterior
    //        if (previousRow.SubDeliverables.Contains(selectedRow))
    //        {
    //            return false;
    //        }

    //        // Permitir el movimiento si todas las condiciones anteriores se cumplen
    //        return true;
    //    }
    //    /// <summary>
    //    /// Mueve un elemento hacia la derecha, agregándolo como subelemento del primer padre principal de la fila anterior.
    //    /// Si no hay padre principal, se agrega como subelemento de la fila anterior (si está en el raíz).
    //    /// </summary>
    //    public void MoveRight(DeliverableResponse2 selectedRow)
    //    {
    //        if (selectedRow == null) return;

    //        // Obtener la lista plana de todos los elementos
    //        var flatList = FlatList;
    //        int currentIndex = flatList.IndexOf(selectedRow);

    //        // Si el selectedRow es el primer elemento, no se puede mover a la derecha
    //        if (currentIndex == 0) return;

    //        // Encontrar el primer ancestro común en la jerarquía
    //        DeliverableResponse2? ancestor = FindAncestor(selectedRow, flatList, currentIndex);

    //        if (ancestor == null)
    //        {
    //            // Si no hay ancestro, no se puede mover a la derecha
    //            return;
    //        }

    //        // Remover el selectedRow de su ubicación actual
    //        var currentParent = FindParent(selectedRow);
    //        if (currentParent != null)
    //        {
    //            currentParent.RemoveSubDeliverable(selectedRow);
    //        }
    //        else
    //        {
    //            Items.Remove(selectedRow);
    //        }

    //        // Agregar el selectedRow como subdeliverable del ancestro encontrado
    //        AddSubDeliverable(ancestor, selectedRow);

    //        // Actualizar la jerarquía completa
    //        UpdateSOrder();
    //    }

    //    /// <summary>
    //    /// Encuentra el primer ancestro común en la jerarquía para el selectedRow.
    //    /// </summary>
    //    private DeliverableResponse2? FindAncestor(DeliverableResponse2 selectedRow, List<DeliverableResponse2> flatList, int currentIndex)
    //    {
    //        // Recorrer la lista plana hacia atrás para encontrar el primer ancestro
    //        for (int i = currentIndex - 1; i >= 0; i--)
    //        {
    //            var candidate = flatList[i];

    //            // Verificar si el candidato es un ancestro válido
    //            if (IsAncestor(candidate, selectedRow))
    //            {
    //                return candidate;
    //            }
    //        }

    //        return null; // No se encontró un ancestro válido
    //    }

    //    /// <summary>
    //    /// Determina si un candidato es un ancestro válido para el selectedRow.
    //    /// </summary>
    //    private bool IsAncestor(DeliverableResponse2 candidate, DeliverableResponse2 selectedRow)
    //    {
    //        // Un candidato es un ancestro si está en un nivel superior en la jerarquía
    //        // y no es un descendiente del selectedRow.

    //        var anterior = candidate.WBS.Split('.').Length;
    //        var selected = selectedRow.WBS.Split('.').Length;
    //        return anterior == selected;
    //    }


    //    /// <summary>
    //    /// Mueve un elemento hacia la izquierda, moviéndolo al nivel del padre anterior.
    //    /// Si hay otros subdeliverables en el padre, el SelectedRow se coloca al final de la lista.
    //    /// </summary>
    //    public void MoveLeft(DeliverableResponse2 selectedRow)
    //    {
    //        if (selectedRow == null) return;

    //        // Encontrar el padre actual del SelectedRow
    //        var currentParent = FindParent(selectedRow);
    //        if (currentParent == null) return; // Ya está en el nivel raíz, no se puede mover más a la izquierda

    //        // Eliminar el SelectedRow de su ubicación actual
    //        currentParent.RemoveSubDeliverable(selectedRow);

    //        // Encontrar el padre del padre actual
    //        var parentOfCurrentParent = FindParent(currentParent);

    //        if (parentOfCurrentParent != null)
    //        {
    //            AddSubDeliverable(parentOfCurrentParent, selectedRow);

            
    //        }
    //        else
    //        {
    //            // Si no hay padre del padre actual, agregar al nivel raíz
    //            Items.Add(selectedRow);
    //        }

    //        // Actualizar la jerarquía completa
    //        UpdateSOrder();
    //    }


    //    /// <summary>
    //    /// Actualiza los campos Order y sOrder de todos los elementos en la jerarquía.
    //    /// </summary>
    //    /// 
    //    private void UpdateSOrder()
    //    {
    //        int globalOrder = 0; // Inicializar el contador global
    //        UpdateSOrder(OrderedItems, null, ref globalOrder);
    //    }

    //    private void UpdateSOrder(List<DeliverableResponse2> list, DeliverableResponse2? parent, ref int globalOrder)
    //    {
    //        // Validar si la lista es nula o vacía
    //        if (list == null || list.Count == 0)
    //            return;

    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            var item = list[i];

    //            // Asignar el campo Order basado en la posición en la lista
    //            item.Order = i + 1;

    //            // Asignar el campo WBS basado en la jerarquía
    //            item.WBS = string.IsNullOrEmpty(parent?.WBS)
    //                ? item.Order.ToString() // Nivel raíz
    //                : $"{parent.WBS}.{item.Order}"; // Subnivel

    //            // Asignar LabelOrder usando el índice global
    //            item.LabelOrder = ++globalOrder;

    //            // Si el elemento tiene subelementos, actualizar recursivamente
    //            if (item.SubDeliverables != null && item.SubDeliverables.Count > 0)
    //            {
    //                UpdateSOrder(item.SubDeliverables, item, ref globalOrder);
    //            }
    //        }
    //    }


    //    public DeliverableResponse2? FindParent(DeliverableResponse2 child)
    //    {
    //        return FlatList.FirstOrDefault(x => x.Id == child.ParentDeliverableId);

    //    }

    //    /// <summary>
    //    /// Elimina un DeliverableResponse2 de la jerarquía principal cuando CancelCreate.
    //    /// </summary>
    //    public void RemoveDeliverableResponse2(DeliverableResponse2 deliverable)
    //    {
    //        // Encontrar el deliverable a eliminar
    //        Items.Remove(deliverable);

    //        // Actualizar la jerarquía completa
         
    //        UpdateSOrder();
    //    }

       

    //    public void UpdateStartDate(DeliverableResponse2 current, DateTime? start)
    //    {
    //        current.UpdateStartDate(start);
    //        CalculateDatesFromDependencies();
    //    }
    //    public void UpdateEndDate(DeliverableResponse2 current, DateTime? end)
    //    {
    //        current.UpdateEndDate(end);
    //        CalculateDatesFromDependencies();
    //    }
    //    public void UpdateDuration(DeliverableResponse2 current, string duration)
    //    {
    //        current.UpdateDuration(duration);
    //        CalculateDatesFromDependencies();
    //    }
    //    public void UpdateDependencies(DeliverableResponse2 current, string dependencies)
    //    {
    //        current.Dependants = new();
    //        if (string.IsNullOrWhiteSpace(dependencies))
    //        {
    //            // Si no hay dependencias, limpiar la lista de dependientes y salir
    //            Console.WriteLine("No dependencies provided.");

    //            return;
    //        }

    //        // Dividir las dependencias por comas y limpiar espacios en blanco
    //        var dependencyList = dependencies.Split(',')
    //                                          .Select(dep => dep.Trim())
    //                                          .Where(dep => !string.IsNullOrEmpty(dep))
    //                                          .ToList();

    //        // Limpiar la lista de dependientes actual


    //        // Validar cada dependencia
    //        foreach (var dependency in dependencyList)
    //        {
    //            var dependencyFromFlatList = FlatList.FirstOrDefault(x => x.LabelOrder.ToString() == dependency);

    //            if (dependencyFromFlatList == null)
    //            {
    //                Console.WriteLine($"Dependency with LabelOrder {dependency} not found.");
    //                continue; // Saltar esta dependencia si no se encuentra
    //            }

    //            // Validar que no haya referencias cruzadas o dependencias inválidas
    //            if (!IsValidDependency(current, dependencyFromFlatList))
    //            {
    //                Console.WriteLine($"Invalid dependency: {current.Name} cannot depend on {dependencyFromFlatList.Name}.");
    //                continue; // Saltar esta dependencia si no es válida
    //            }

    //            // Agregar la dependencia válida a la lista de dependientes
    //            current.Dependants.Add(dependencyFromFlatList);
    //        }

    //        // Recalcular las fechas basadas en las nuevas dependencias
    //        CalculateDatesFromDependencies();
    //    }
    //    private bool HasAncestor(DeliverableResponse2 child, DeliverableResponse2 potentialAncestor)
    //    {
    //        // Buscar el padre del elemento actual
    //        var parent = FindParent(child);

    //        // Si no hay padre, detener la recursión
    //        if (parent == null)
    //        {
    //            return false;
    //        }

    //        // Si el padre es el ancestro buscado, retornar true
    //        if (parent.Id == potentialAncestor.Id)
    //        {
    //            return true;
    //        }

    //        // Continuar buscando recursivamente hacia arriba
    //        return HasAncestor(parent, potentialAncestor);
    //    }

    //    private bool IsValidDependency(DeliverableResponse2 current, DeliverableResponse2 dependency)
    //    {
    //        // 1. Un elemento no puede depender de sí mismo
    //        if (current.Id == dependency.Id)
    //        {
    //            return false;
    //        }

    //        // 2. Un elemento no puede depender de su padre ni de ningún ancestro
    //        if (HasAncestor(current, dependency))
    //        {
    //            return false;
    //        }

    //        // 3. Detectar ciclos indirectos
    //        if (HasCircularDependency(current, dependency))
    //        {
    //            return false; // Dependencia inválida: ciclo detectado
    //        }

    //        return true;
    //    }
    //    private bool HasCircularDependency(DeliverableResponse2 current, DeliverableResponse2 dependency)
    //    {
    //        // Conjunto para rastrear los nodos visitados durante la búsqueda
    //        var visited = new HashSet<Guid>();
    //        var stack = new Stack<DeliverableResponse2>();

    //        // Guardar el estado original de los Dependants
    //        var originalDependants = new List<DeliverableResponse2>(current.Dependants);

    //        current.Dependants.Add(dependency);

    //        // Comenzar la búsqueda desde el nodo "current"
    //        stack.Push(current);

    //        while (stack.Count > 0)
    //        {
    //            var node = stack.Pop();

    //            // Si ya hemos visitado este nodo, continuar con el siguiente
    //            if (visited.Contains(node.Id))
    //            {
    //                continue;
    //            }

    //            // Marcar este nodo como visitado
    //            visited.Add(node.Id);

    //            // Agregar todos los dependientes de este nodo al stack para seguir explorando
    //            foreach (var dependent in node.Dependants)
    //            {
    //                // Si encontramos el nodo inicial (current), hay un ciclo
    //                if (dependent.Id == current.Id)
    //                {
    //                    current.Dependants = originalDependants;
    //                    return true; // Ciclo detectado
    //                }

    //                // Agregar el dependiente al stack para seguir explorando
    //                stack.Push(dependent);
    //            }
    //        }

    //        // Si no se encontró ningún ciclo, retornar false


    //        current.Dependants = originalDependants;
    //        return false;
    //    }
    //    public void UpdateRelationType(DeliverableResponse2 current, TasksRelationTypeEnum type)
    //    {
    //        current.DependencyType = type;

    //        CalculateDatesFromDependencies();
    //    }

    //    public void CalculateDatesFromDependencies2()
    //    {
    //        foreach (var item in Items)
    //        {
    //            CalculateDatesRecursively(item);
    //        }
    //    }
    //    private void CalculateDatesRecursively(DeliverableResponse2 item)
    //    {
    //        // 1. Calcular las fechas basadas en dependencias
    //        UpdateDatesFromDependencies(item);

    //        // 2. Calcular las fechas de los subdeliverables recursivamente
    //        if (item.SubDeliverables != null && item.SubDeliverables.Any())
    //        {
    //            foreach (var subDeliverable in item.SubDeliverables)
    //            {
    //                CalculateDatesRecursively(subDeliverable);
    //            }

    //            // 3. Actualizar las fechas del elemento actual basadas en los subdeliverables
    //            var minSubStartDate = item.SubDeliverables
    //                .Where(sd => sd.StartDate.HasValue)
    //                .Select(sd => sd.StartDate!.Value)
    //                .DefaultIfEmpty(DateTime.MaxValue)
    //                .Min();

    //            var maxSubEndDate = item.SubDeliverables
    //                .Where(sd => sd.EndDate.HasValue)
    //                .Select(sd => sd.EndDate!.Value)
    //                .DefaultIfEmpty(DateTime.MinValue)
    //                .Max();

    //            item.StartDate = minSubStartDate != DateTime.MaxValue ? minSubStartDate : (DateTime?)null;
    //            item.EndDate = maxSubEndDate != DateTime.MinValue ? maxSubEndDate : (DateTime?)null;
    //        }

    //        // 4. Propagar las fechas hacia arriba en la jerarquía
    //        var parent = FindParent(item);
    //        if (parent != null)
    //        {
    //            UpdateParentDates(parent);
    //        }
    //    }
    //    private void CalculateDatesRecursively2(DeliverableResponse2 item)
    //    {
    //        // 1. Calcular las fechas de los subdeliverables recursivamente
    //        if (item.SubDeliverables != null && item.SubDeliverables.Any())
    //        {
    //            foreach (var subDeliverable in item.SubDeliverables)
    //            {
    //                CalculateDatesRecursively(subDeliverable);
    //            }

    //            var minSubStartDate = item.SubDeliverables
    //                .Where(sd => sd.StartDate.HasValue)
    //                .Select(sd => sd.StartDate!.Value)
    //                .DefaultIfEmpty(DateTime.MaxValue)
    //                .Min();

    //            var maxSubEndDate = item.SubDeliverables
    //                .Where(sd => sd.EndDate.HasValue)
    //                .Select(sd => sd.EndDate!.Value)
    //                .DefaultIfEmpty(DateTime.MinValue)
    //                .Max();

    //            item.StartDate = minSubStartDate != DateTime.MaxValue ? minSubStartDate : (DateTime?)null;
    //            item.EndDate = maxSubEndDate != DateTime.MinValue ? maxSubEndDate : (DateTime?)null;
    //        }

    //        // 2. Actualizar fechas basadas en dependencias
    //        UpdateDatesFromDependencies(item);

    //        // 3. Propagar las fechas hacia arriba en la jerarquía
    //        var parent = FindParent(item);
    //        if (parent != null)
    //        {
    //            UpdateParentDates(parent);
    //        }


    //    }

    //    private void UpdateDatesFromDependencies2(DeliverableResponse2 item)
    //    {
    //        // Validar que haya dependencias
    //        if (item.Dependants == null || !item.Dependants.Any()) return;

    //        // Calcular las fechas mínimas y máximas de las dependencias
    //        var minStartDate = item.Dependants
    //            .Where(x => x.StartDate.HasValue)
    //            .Select(x => x.StartDate!.Value)
    //            .DefaultIfEmpty(DateTime.MaxValue)
    //            .Min();

    //        var maxEndDate = item.Dependants
    //            .Where(x => x.EndDate.HasValue)
    //            .Select(x => x.EndDate!.Value)
    //            .DefaultIfEmpty(DateTime.MinValue)
    //            .Max();

    //        // Aplicar las reglas según el tipo de dependencia
    //        switch (item.DependencyType.Id)
    //        {
    //            case 0:
    //                item.UpdateStartDate(minStartDate);
    //                break;

    //            case 1:
    //                item.UpdateStartDate(maxEndDate);
    //                break;

    //            case 2:
    //                item.UpdateStartDate(maxEndDate.AddDays(1));
    //                break;

    //            case 3:
    //                item.UpdateEndDate(maxEndDate);
    //                break;

    //            default:
    //                // Si no hay un tipo de dependencia válido, no hacer nada
    //                return;
    //        }


    //    }



    //    private void UpdateParentDates2(DeliverableResponse2 parent)
    //    {
    //        var minSubStartDate = parent.SubDeliverables
    //            .Where(sd => sd.StartDate.HasValue)
    //            .Select(sd => sd.StartDate!.Value)
    //            .DefaultIfEmpty(DateTime.MaxValue)
    //            .Min();

    //        var maxSubEndDate = parent.SubDeliverables
    //            .Where(sd => sd.EndDate.HasValue)
    //            .Select(sd => sd.EndDate!.Value)
    //            .DefaultIfEmpty(DateTime.MinValue)
    //            .Max();

    //        parent.StartDate = minSubStartDate != DateTime.MaxValue ? minSubStartDate : (DateTime?)null;
    //        parent.EndDate = maxSubEndDate != DateTime.MinValue ? maxSubEndDate : (DateTime?)null;

    //        var grandParent = FindParent(parent);
    //        if (grandParent != null)
    //        {
    //            UpdateParentDates(grandParent);
    //        }
    //    }

    //    private void UpdateSOrder(List<DeliverableResponse2> list, DeliverableResponse2? parent = null)
    //    {
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            var item = list[i];
    //            item.Order = i + 1;
    //            item.WBS = string.IsNullOrEmpty(parent?.WBS)
    //                ? item.Order.ToString()
    //                : $"{parent.WBS}.{item.Order}";

    //            if (item.SubDeliverables != null && item.SubDeliverables.Any())
    //            {
    //                UpdateSOrder(item.SubDeliverables, item);
    //            }
    //        }
    //    }
    //    public void AddSubDeliverable(DeliverableResponse2 parent, DeliverableResponse2 subDeliverable)
    //    {
    //        // Validar y limpiar dependencias que podrían causar ciclos circulares
    //        CleanCircularDependencies(parent, subDeliverable);

    //        subDeliverable.ParentDeliverableId = parent.Id;
    //        // Agregar el SubDeliverable al padre
    //        parent.SubDeliverables.Add(subDeliverable);

            
    //    }
    //    private void CleanCircularDependencies(DeliverableResponse2 parent, DeliverableResponse2 subDeliverable)
    //    {
    //        // Obtener todos los ancestros del padre (incluyendo el propio padre)
    //        var ancestors = GetAncestors(parent);

    //        // Iterar sobre los Dependants del SubDeliverable
    //        if (subDeliverable.Dependants != null && subDeliverable.Dependants.Any())
    //        {
    //            // Filtrar y eliminar los Dependants que son ancestros del padre
    //            subDeliverable.Dependants = subDeliverable.Dependants
    //                .Where(dependant => !ancestors.Contains(dependant))
    //                .ToList();
    //        }
    //    }
    //    private HashSet<DeliverableResponse2> GetAncestors(DeliverableResponse2 deliverable)
    //    {
    //        var ancestors = new HashSet<DeliverableResponse2>();
    //        var current = deliverable;

    //        while (current != null)
    //        {
    //            ancestors.Add(current);
    //            current = FindParent(current);
    //        }

    //        return ancestors;
    //    }

    //    public void CalculateDatesFromDependencies()
    //    {
    //        // Obtener una lista plana de todos los elementos ordenados por profundidad (de hojas a raíz)
    //        var orderedFlatList = GetOrderedFlatListByDepth(Items);

    //        // Calcular las fechas para cada elemento en el orden correcto
    //        foreach (var item in orderedFlatList)
    //        {
    //            CalculateDatesForItem(item);
    //        }
    //    }
    //    private List<DeliverableResponse2> GetOrderedFlatListByDepth(List<DeliverableResponse2> items)
    //    {
    //        var flatList = new List<DeliverableResponse2>();
    //        FlattenAndOrder(items, flatList);
    //        return flatList;
    //    }

    //    private void FlattenAndOrder(List<DeliverableResponse2> items, List<DeliverableResponse2> flatList)
    //    {
    //        foreach (var item in items)
    //        {
    //            // Procesar recursivamente los subdeliverables antes de agregar el elemento actual
    //            if (item.SubDeliverables != null && item.SubDeliverables.Any())
    //            {
    //                FlattenAndOrder(item.SubDeliverables, flatList);
    //            }

    //            // Agregar el elemento actual a la lista plana
    //            flatList.Add(item);
    //        }
    //    }
    //    private void CalculateDatesForItem(DeliverableResponse2 item)
    //    {
    //        // 1. Calcular las fechas basadas en dependencias
    //        UpdateDatesFromDependencies(item);

    //        // 2. Si tiene subdeliverables, ajustar las fechas del padre basadas en los subdeliverables
    //        if (item.SubDeliverables != null && item.SubDeliverables.Any())
    //        {
    //            var minSubStartDate = item.SubDeliverables
    //                .Where(sd => sd.StartDate.HasValue)
    //                .Select(sd => sd.StartDate!.Value)
    //                .DefaultIfEmpty(DateTime.MaxValue)
    //                .Min();

    //            var maxSubEndDate = item.SubDeliverables
    //                .Where(sd => sd.EndDate.HasValue)
    //                .Select(sd => sd.EndDate!.Value)
    //                .DefaultIfEmpty(DateTime.MinValue)
    //                .Max();

    //            if (minSubStartDate != DateTime.MaxValue)
    //            {
    //                item.StartDate = minSubStartDate;
    //            }
    //            if (maxSubEndDate != DateTime.MinValue)
    //            {
    //                item.EndDate = maxSubEndDate;
    //            }
    //        }
    //    }
    //    private void UpdateDatesFromDependencies(DeliverableResponse2 item)
    //    {
    //        if (item.Dependants == null || !item.Dependants.Any())
    //        {
    //            return; // No hay dependencias, no hay nada que actualizar
    //        }

    //        // Calcular la fecha de inicio mínima y la fecha de finalización máxima de las dependencias
    //        var minDependencyStartDate = item.Dependants
    //            .Where(d => d.StartDate.HasValue)
    //            .Select(d => d.StartDate!.Value)
    //            .DefaultIfEmpty(DateTime.MaxValue)
    //            .Min();

    //        var maxDependencyEndDate = item.Dependants
    //            .Where(d => d.EndDate.HasValue)
    //            .Select(d => d.EndDate!.Value)
    //            .DefaultIfEmpty(DateTime.MinValue)
    //            .Max();

    //        // Actualizar las fechas del elemento si son válidas
    //        if (minDependencyStartDate != DateTime.MaxValue)
    //        {
    //            item.StartDate = minDependencyStartDate;
    //        }
    //        if (maxDependencyEndDate != DateTime.MinValue)
    //        {
    //            item.EndDate = maxDependencyEndDate;
    //        }
    //    }
    //    private void UpdateParentDates(DeliverableResponse2 parent)
    //    {
    //        // Recalcular las fechas del padre basadas en sus subdeliverables
    //        var minSubStartDate = parent.SubDeliverables
    //            .Where(sd => sd.StartDate.HasValue)
    //            .Select(sd => sd.StartDate!.Value)
    //            .DefaultIfEmpty(DateTime.MaxValue)
    //            .Min();

    //        var maxSubEndDate = parent.SubDeliverables
    //            .Where(sd => sd.EndDate.HasValue)
    //            .Select(sd => sd.EndDate!.Value)
    //            .DefaultIfEmpty(DateTime.MinValue)
    //            .Max();

    //        // Asignar las fechas recalculadas
    //        if (minSubStartDate != DateTime.MaxValue)
    //        {
    //            parent.StartDate = minSubStartDate;
    //        }
    //        if (maxSubEndDate != DateTime.MinValue)
    //        {
    //            parent.EndDate = maxSubEndDate;
    //        }

    //        // Calcular la duración basada en las nuevas fechas
    //        // La duración se calcula automáticamente en función de las fechas y el DurationInput
    //        // No es necesario asignarla manualmente

    //        // Propagar las fechas hacia arriba si el padre tiene un padre
    //        var grandParent = FindParent(parent);
    //        if (grandParent != null)
    //        {
    //            UpdateParentDates(grandParent);
    //        }
    //    }
    //}


}
