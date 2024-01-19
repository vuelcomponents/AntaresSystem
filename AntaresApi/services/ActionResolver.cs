using AntaresApi.Models;
using AntaresApi.Models.Position;
using AntaresApi.Models.Status;
using AntaresApi.Models.StatusAction;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AntaresApi.Services;

public class ActionResolver : IActionResolver
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;
    public ActionResolver(DataContext context, IMapper mapper, IMailService mailService)
    {
        _mapper = mapper;
        _context = context;
        _mailService = mailService;
    }

    public async Task ResolveStatusAction( Status? previousStatus, Status nextStatus, long objectId)
    {
        // if (!(nextStatus.StatusActions is { Count: > 0 }))
        // {
        //     return;
        // }
        if(nextStatus.StatusActions is { Count:> 0})
        {
            foreach (var statusAction in nextStatus.StatusActions)
            {
                StatusAction? dbStatusAction = _context.StatusActions
                    .Include(sa => sa.StatusActionTrigger)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.SystemFunction)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.Company)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.Position)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.Requirements)!
                    .ThenInclude(r=>r.VariantRealisation)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.Requirements)!
                    .ThenInclude(r=>r.Variant)
                    .ThenInclude(v=>v.VariantType)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.StoreModel)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.Mail)
                    .ThenInclude(m=>m.Documents)
                    
                    .FirstOrDefault(s => s.Id == statusAction.Id);
                if (statusAction == null)
                {
                    throw new ApplicationException("Status action has been specified but not found :/");
                }

                switch (dbStatusAction!.StatusActionTrigger.Code)
                {
                    case "Start":
                        switch (dbStatusAction.ActionFunction.StoreModel.Code)
                        {
                            case "Employee":
                                await DoEmployeeAction(objectId, dbStatusAction.ActionFunction);
                                break;
                            default:
                                throw new NotImplementedException(
                                    "Status has starting function, not implemented yet to this model");
                        }

                        break;
                }
            }
        }
        if(previousStatus != null && previousStatus.StatusActions is { Count:> 0})
        {
            foreach (var statusAction in previousStatus.StatusActions)
            {
                StatusAction? dbStatusAction = _context.StatusActions
                    .Include(sa => sa.StatusActionTrigger)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.SystemFunction)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.Company)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.Requirements)!
                    .ThenInclude(r=>r.VariantRealisation)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.Requirements)!
                    .ThenInclude(r=>r.Variant)
                    .ThenInclude(v=>v.VariantType)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.StoreModel)
                    .Include(sa => sa.ActionFunction)
                    .ThenInclude(af => af.Mail)
                    .FirstOrDefault(s => s.Id == statusAction.Id);
                if (statusAction == null)
                {
                    throw new ApplicationException("Status action has been specified but not found :/");
                }

                switch (dbStatusAction!.StatusActionTrigger.Code)
                {
                    case "End":
                        switch (dbStatusAction.ActionFunction.StoreModel.Code)
                        {
                            case "Employee":
                                await DoEmployeeAction(objectId, dbStatusAction.ActionFunction);
                                break;
                            default:
                                throw new NotImplementedException(
                                    "Status has ending function, not implemented yet to this model");
                        }

                        break;
                }
            }
        }
    }

    private async Task DoEmployeeAction(long employeeId, ActionFunction actionFunction)
    {
        if (actionFunction.SystemFunction?.Code == null)
        {
            return;
        }
        switch (actionFunction.SystemFunction.Code)
        {
            case "Hire":
                if (actionFunction.Company == null)
                {
                    throw new BadHttpRequestException("Company has not been specified to hire system function");
                }
                Company? company = _context.Companies.FirstOrDefault(c => c.Id == actionFunction.Company.Id);
                if (company == null)
                {
                    throw new BadHttpRequestException($"Company to hire has not been found #{actionFunction.Company.Id}");
                }
                if (actionFunction.Position == null)
                {
                    throw new BadHttpRequestException("Position has not been specified to hire system function");
                }
                Position? position = _context.Positions.FirstOrDefault(c => c.Id == actionFunction.Position.Id);
                if (position == null)
                {
                    throw new BadHttpRequestException($"Position to hire has not been found #{actionFunction.Company.Id}");
                }


                Employee? employee = _context.Employees
                    .Include(e=>e.Companies)
                    .Include(e=>e.Realisations)!
                    .ThenInclude(r=>r.VariantRealisation)
                    .Include(e=>e.Realisations)!
                    .ThenInclude(r=>r.Variant)
                    .ThenInclude(v=>v.VariantType)
                    .Include(e=>e.Positions)
                    .FirstOrDefault(e => e.Id == employeeId);
                if (employee == null)
                {
                    throw new BadHttpRequestException($"Employee #{employeeId} has not been found");
                }

                if (actionFunction.Requirements is { Count: > 0 })
                {
                    try
                    {
                        ValidRequirements(actionFunction.Requirements, employee.Realisations);
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }
                if (!(employee.Companies!.Any(c => c.Id == company.Id)))
                {
                    employee.Companies!.Add(company);
                }
                if (!(employee.Positions!.Any(p => p.Id == position.Id)))
                {
                    employee.Positions!.Add(position);
                }
                break;
            case "Terminate employment":
                // throw new BadHttpRequestException("Terminate employment");
                if (actionFunction.Company == null)
                {
                    throw new BadHttpRequestException("Company has not been specified to terminate system function");
                }
                Company? company2 = _context.Companies.FirstOrDefault(c => c.Id == actionFunction.Company.Id);
                if (company2 == null)
                {
                    throw new BadHttpRequestException($"Company to terminate has not been found #{actionFunction.Company.Id}");
                }

                Employee? employee2 = _context.Employees
                    .Include(e=>e.Companies)
                    .Include(e=>e.Realisations)!
                    .ThenInclude(r=>r.VariantRealisation)
                    .Include(e=>e.Realisations)!
                    .ThenInclude(r=>r.Variant)
                    .ThenInclude(v=>v.VariantType)
                    .FirstOrDefault(e => e.Id == employeeId);
                if (employee2 == null)
                {
                    throw new BadHttpRequestException($"Employee #{employeeId} has not been found");
                }
                if (actionFunction.Requirements is { Count: > 0 })
                {
                    try
                    {
                        ValidRequirements(actionFunction.Requirements, employee2.Realisations);
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }
                if (employee2.Companies == null)
                {
                    employee2.Companies = new List<Company>();
                }
                employee2.Companies.Remove(company2);
                _context.SaveChanges();
                // throw new NotImplementedException($"{employee2.Id} e/ {company2.Id} c");
                break;
            case "Mail":
                
                    Employee? employee3 = _context.Employees
                        .FirstOrDefault(e => e.Id == employeeId);
                    if (employee3 == null)
                    {
                        throw new BadHttpRequestException("Employee has not been found");
                    }
                    await _mailService.SendEmailFromActionFunction(actionFunction.Mail, employee3.Email, $"{employee3.FirstName} {employee3.LastName}");
               
                break;
        }
    }
    private void ValidRequirements(List<Realisation> requirements, List<Realisation>? realisations)
    {
        if (realisations == null)
        {
            throw new Exception("Expected realisation is null");
        }

        foreach (var requirement in requirements)
        {
            Realisation? rl  = realisations.Find(r => r.Variant.Id == requirement.Variant.Id);
            if (rl == null)
            {
                throw new Exception("Realisation with variant of requirement has not been found");
            }
            switch (requirement.Variant?.VariantType?.Code)
            {
                case "Numeric":
                    switch (requirement.Comparator!.Code)
                    {
                        case "<":
                            if (requirement.NumericValue < rl.NumericValue)
                            {
                                throw new Exception($"The value requirement  |{requirement.Variant.Code} is lower than realisations |{rl.Variant.Code} what is bad");
                            }
                            break;
                        case ">":
                            if (requirement.NumericValue > rl.NumericValue)
                            {
                                throw new Exception($"The value requirement |{requirement.Variant.Code} is bigger than realisation  |{rl.Variant.Code} what is bad");
                            }
                            break;
                        case "<=":
                            if (requirement.NumericValue <= rl.NumericValue)
                            {
                                throw new Exception($"The value requirement |{requirement.Variant.Code} is lower than realisation |{rl.Variant.Code}  what is bad");
                            }
                            break;
                        case ">=":
                            if (requirement.NumericValue >= rl.NumericValue)
                            {
                                throw new Exception($"The value requirement  |{requirement.Variant.Code}is bigger than realisation  |{rl.Variant.Code}  what is bad");
                            }
                            break;
                        case "==":
                            if (requirement.NumericValue  != rl.NumericValue)
                            {
                                throw new Exception($"The value requirement |{requirement.Variant.Code} is not equal to realisation |{rl.Variant.Code}what is bad");
                            }
                            break;
                    }
                    break;
                   case "Date":
                    switch (requirement.Comparator!.Code)
                    {
                        case "<":
                            if (requirement.DateValue < rl.DateValue)
                            {
                                throw new Exception($"Date  |{requirement.Variant.Code} is lower than realisations |{rl.Variant.Code} what is bad");
                            }
                            break;
                        case ">":
                            if (requirement.DateValue > rl.DateValue)
                            {
                                throw new Exception($"Date |{requirement.Variant.Code} is bigger than realisation  |{rl.Variant.Code} what is bad");
                            }
                            break;
                        case "<=":
                            if (requirement.DateValue <= rl.DateValue)
                            {
                                throw new Exception($"Date |{requirement.Variant.Code} is lower than realisation |{rl.Variant.Code}  what is bad");
                            }
                            break;
                        case ">=":
                            if (requirement.DateValue >= rl.DateValue)
                            {
                                throw new Exception($"Date |{requirement.Variant.Code}is bigger than realisation  |{rl.Variant.Code}  what is bad");
                            }
                            break;
                        case "==":
                            if (requirement.DateValue  != rl.DateValue)
                            {
                                throw new Exception($"Date |{requirement.Variant.Code} is not equal to realisation |{rl.Variant.Code}what is bad");
                            }
                            break;
                    }
                    break;
                    case "Variant Realisation":
                        if (requirement.VariantRealisation!.Id != rl.VariantRealisation!.Id)
                        {
                            throw new Exception("Variant realisations  differ");
                        }
                        break;
            }
        }
    }
}