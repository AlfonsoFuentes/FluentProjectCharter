﻿global using LazyCache;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Query;
global using Server.Database.Contracts;
global using Server.Database.Entities;
global using Server.Database.Entities.BudgetItems;
global using Server.Database.Entities.BudgetItems.Commons;
global using Server.Database.Entities.BudgetItems.EngineeringContingency;
global using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams;
global using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Equipments;
global using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Instruments;
global using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Nozzles;
global using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Pipings;
global using Server.Database.Entities.BudgetItems.ProcessFlowDiagrams.Valves;
global using Server.Database.Entities.BudgetItems.Taxes;
global using Server.EndPoint.Assumptions.Queries;
global using Server.EndPoint.BackGrounds.Queries;
global using Server.EndPoint.Bennefits.Queries;
global using Shared.Models.Templates.Pipings.Records;
global using Shared.Models.Templates.Pipings.Responses;
global using Server.ExtensionsMethods.Pipings;
global using Server.EndPoint.ExpertJudgements.Queries;
global using Server.EndPoint.KnownRisks.Queries;
global using Server.EndPoint.MeetingsGroup.Meetings.Queries;
global using Server.EndPoint.Projects.Queries;
global using Server.EndPoint.Requirements.Queries;
global using Server.EndPoint.Scopes.Queries;
global using Server.ExtensionsMethods.ExportsExcel;
global using Server.Interfaces.Database;
global using Server.Interfaces.EndPoints;
global using Server.Interfaces.UserServices;
global using Server.Repositories;
global using Shared.Commons;
global using Shared.Enums.ExportFiles;
global using Shared.Enums.ProjectNeedTypes;
global using Shared.Enums.StakeHolderTypes;
global using Shared.Models.Assumptions.Records;
global using Shared.Models.Assumptions.Responses;
global using Shared.Models.Backgrounds.Responses;
global using Shared.Models.Bennefits.Records;
global using Shared.Models.Bennefits.Requests;
global using Shared.Models.Bennefits.Responses;
global using Shared.Models.Constrainsts.Records;
global using Shared.Models.Constrainsts.Responses;
global using Shared.Models.Deliverables.Responses;
global using Shared.Models.ExpertJudgements.Responses;
global using Shared.Models.FileResults;
global using Shared.Models.KnownRisks.Responses;
global using Shared.Models.Meetings.Records;
global using Shared.Models.Meetings.Responses;

global using Shared.Models.Projects.Exports;
global using Shared.Models.Projects.Records;
global using Shared.Models.Projects.Reponses;
global using Shared.Models.Projects.Request;
global using Shared.Models.Projects.Validators;
global using Shared.Models.Requirements.Records;
global using Shared.Models.Requirements.Responses;
global using Shared.Models.Scopes.Responses;
global using Shared.Models.StakeHolders.Responses;
global using Shared.StaticClasses;
global using System.Linq.Expressions;
global using System.Reflection;
global using UnitSystem;
global using IResult = Shared.Commons.IResult;
global using Server.ExtensionsMethods.Nozzles;
global using Server.Database.Entities.ProjectManagements;
global using Server.EndPoint.BudgetItems.IndividualItems.Alterations.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.EHSs.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Electricals.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.EngineeringDesigns.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Equipments.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Foundations.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Instruments.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Paintings.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Pipes.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Structurals.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Taxs.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Testings.Queries;
global using Server.EndPoint.BudgetItems.IndividualItems.Valves.Queries;
global using Shared.Enums.TasksRelationTypes;
global using Shared.Models.GanttTasks.Records;
global using Shared.Models.GanttTasks.Responses;
global using System.Collections.Concurrent;
global using System.Diagnostics;
global using Server.ExtensionsMethods.ValveTemplateMappers;

global using Server.ExtensionsMethods.EquipmentTemplateMapper;
global using Shared.Models.BudgetItems.IndividualItems.Equipments.Responses;
