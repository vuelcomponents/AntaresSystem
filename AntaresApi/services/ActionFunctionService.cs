using AntaresApi.Dto.Action;
using AntaresApi.Dto.Common;
using AntaresApi.Models;
using AntaresApi.Models.Document;
using AntaresApi.Models.Mail;
using AntaresApi.Models.Position;
using AntaresApi.Models.StatusAction;
using AntaresApi.Models.StoreModel;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AntaresApi.Services;

public class ActionFunctionService : IActionFunctionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ActionFunctionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IEnumerable<ActionFunctionDto> GetAllActionFunctions()
    {
        List<ActionFunction> list = _context.ActionFunctions
            .Include(c => c.StoreModel)
            .Include(c=>c.Requirements)!
            .ThenInclude(c=>c.Variant)
            .Include(c=>c.Requirements)!
            .ThenInclude(c=>c.VariantRealisation)
            .Include(c=>c.Requirements)!
            .ThenInclude(c=>c.Comparator)
            .Include(c=>c.Company)
            .Include(s=>s.SystemFunction)
            .ThenInclude(s=>s.StoreModels)
            .Include(s=>s.Mail)
            .Include(s=>s.Position)
            .ThenInclude(p=>p.Company)
            .ToList();
     
        return list.Select(_mapper.Map<ActionFunctionDto>).ToList();
    }

    public ActionFunctionDto GetActionFunctionById(long id)
    {
        ActionFunction? actionFunction = _context.ActionFunctions
            .Include(s=>s.StoreModel)
            .Include(c=>c.Requirements)!
            .ThenInclude(c=>c.Variant)
            .Include(c=>c.Requirements)!
            .ThenInclude(c=>c.VariantRealisation)
            .Include(c=>c.Requirements)!
            .ThenInclude(c=>c.Comparator)
            .Include(c=>c.Company)
            .Include(s=>s.SystemFunction)
            .ThenInclude(s=>s.StoreModels)
            .Include(s=>s.Mail)
            .Include(s=>s.Position)
            .Include(s=>s.Position)
            .ThenInclude(p=>p.Company)
            .FirstOrDefault(c=> c.Id == id);
        if (actionFunction == null)
        {
            throw new BadHttpRequestException($"No company #{id} has been found");
        }
        return _mapper.Map<ActionFunctionDto>(actionFunction);
    }
    public ActionFunctionDto CreateActionFunction(ActionFunctionDto actionFunction)
    {
        if (actionFunction.Code.IsNullOrEmpty() || actionFunction.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }

        if (actionFunction.StoreModel == null)
        {
            throw new BadHttpRequestException("Model has not been specified");
        }

        StoreModel? storeModel = _context.StoreModels.FirstOrDefault(sm => sm.Id == actionFunction.StoreModel.Id);
        if (storeModel == null)
        {
            throw new BadHttpRequestException($"Model #{actionFunction.StoreModel.Id} does not exist");
        }

        if (actionFunction.SystemFunction == null)
        {
            throw new BadHttpRequestException("System function has not been specified");
        }

        SystemFunction? systemFunction =
            _context.SystemFunctions.Include(s=>s.StoreModels).FirstOrDefault(sf => sf.Id == actionFunction.SystemFunction.Id);
        if (systemFunction == null)
        {
            throw new BadHttpRequestException($"System function #{actionFunction.SystemFunction.Id} does not exist");
        }
        if (!(systemFunction.StoreModels.ToList().Any(model => model.Id == actionFunction.StoreModel.Id)))
        {
            throw new BadHttpRequestException("This model does not support that function");
        }
        
        ActionFunction dbActionFunction = new ActionFunction
        {
            StoreModel = storeModel,
            SystemFunction = systemFunction,
            Code = actionFunction.Code!,
            Description = actionFunction.Description!,
        };
        try
        {
            return PrepareActionFunction(dbActionFunction, actionFunction, true);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public ActionFunctionDto UpdateActionFunction(ActionFunctionDto actionFunction)
    {
        ActionFunction? dbActionFunction = _context.ActionFunctions
            .Include(af=>af.SystemFunction)
            .Include(af=>af.Requirements).FirstOrDefault(af => af.Id == actionFunction.Id);
        if (dbActionFunction == null)
        {
            throw new BadHttpRequestException("Action function has not been found");
        }

        if (actionFunction.Code.IsNullOrEmpty() || actionFunction.Description.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Required Fields [Code, Description] has not been specified");
        }

        if (actionFunction.StoreModel == null)
        {
            throw new BadHttpRequestException("Model has not been specified");
        }

        StoreModel? storeModel = _context.StoreModels.FirstOrDefault(sm => sm.Id == actionFunction.StoreModel.Id);
        if (storeModel == null)
        {
            throw new BadHttpRequestException($"Model #{actionFunction.StoreModel.Id} does not exist");
        }

        if (actionFunction.SystemFunction == null)
        {
            throw new BadHttpRequestException("System function has not been specified");
        }

        SystemFunction? systemFunction =
            _context.SystemFunctions.Include(s=>s.StoreModels).FirstOrDefault(sf => sf.Id == actionFunction.SystemFunction.Id);
        if (systemFunction == null)
        {
            throw new BadHttpRequestException($"System function #{actionFunction.SystemFunction.Id} does not exist");
        }

        if (!(systemFunction.StoreModels.Any(model => model.Id == actionFunction.StoreModel.Id)))
        {
            throw new BadHttpRequestException("This model does not support that function");
        }

        if (dbActionFunction.StoreModel.Id != storeModel.Id)
        {
            if (dbActionFunction.Requirements is { Count: > 0 })
            {
                foreach (var requirement in dbActionFunction.Requirements)
                {
                    _context.Realisations.Remove(requirement);
                }
            }
        }

        if (dbActionFunction.SystemFunction?.Id != systemFunction.Id)
        {
            dbActionFunction.Mail = null;
            dbActionFunction.Company = null;
        }
        dbActionFunction.StoreModel = storeModel;
        dbActionFunction.SystemFunction = systemFunction;
        dbActionFunction.Code = actionFunction.Code!;
        dbActionFunction.Description = actionFunction.Description!;
        try
        {
            return PrepareActionFunction(dbActionFunction, actionFunction, false);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public List<IdDto> DeleteMultipleActionFunction(List<IdDto> actionFunctions)
    {
        if(actionFunctions.Count >0)
        {
            foreach (var com in actionFunctions)
            {
                ActionFunction? actionFunction = _context.ActionFunctions
                    .Include(c=>c.Requirements)
                    .FirstOrDefault(c => c.Id == com.Id);
                if (actionFunction != null)
                {
                    _context.ActionFunctions.Remove(actionFunction);
                }
            }

            _context.SaveChanges();
        }
        return actionFunctions;
    }

    private ActionFunctionDto PrepareActionFunction(ActionFunction dbActionFunction, ActionFunctionDto actionFunction, bool create)
    {
        switch (actionFunction.SystemFunction!.Code)
        {
            case "Mail":
                if (actionFunction.Mail == null)
                {
                    throw new BadHttpRequestException("Mail must be set");
                }
                Mail? mail = _context.Mails.FirstOrDefault(m => m.Id == actionFunction.Mail.Id);
                if (mail == null)
                {
                    throw new BadHttpRequestException($"Mail #{actionFunction.Mail.Id} has not been found");
                }

                dbActionFunction.Mail = mail;
                if (create)
                {
                    _context.ActionFunctions.Add(dbActionFunction);
                }
                _context.SaveChanges();
                return _mapper.Map<ActionFunctionDto>(dbActionFunction);
            case "Hire":
                if (actionFunction.Company == null)
                {
                    throw new BadHttpRequestException("Hire function needs a company destination");
                }

                Company? companyToHire =
                    _context.Companies.Include(c=>c.Positions).FirstOrDefault(c => c.Id == actionFunction.Company.Id);
                if (companyToHire == null)
                {
                    throw new BadHttpRequestException($"Company #{actionFunction.Company.Id} has not been found");
                }

                if (actionFunction.Position == null)
                {
                    throw new BadHttpRequestException("Position must be fulfilled");
                }

                Position? position = _context.Positions.FirstOrDefault(p => p.Id == actionFunction.Position.Id);
                if (position == null)
                {
                    throw new BadHttpRequestException($"Position #{actionFunction.Position.Id} has not been found");
                }

                if (!(companyToHire.Positions!.Any(p => p.Id == position.Id)))
                {
                    throw new BadHttpRequestException($"This position does not belong to company {companyToHire.Code}");
                }
                dbActionFunction.Company = companyToHire;
                dbActionFunction.Position = position;
                if (create)
                {
                    _context.ActionFunctions.Add(dbActionFunction);
                }
                _context.SaveChanges();
                return _mapper.Map<ActionFunctionDto>(dbActionFunction);
            case "Terminate employment":
                if (actionFunction.Company == null)
                {
                    throw new BadHttpRequestException("Terminate employment function needs a company destination");
                }

                Company? companyToTerminateEmployment =
                    _context.Companies
                        .Include(c=>c.Positions)
                        .FirstOrDefault(c => c.Id == actionFunction.Company.Id);
                if (companyToTerminateEmployment == null)
                {
                    throw new BadHttpRequestException($"Company #{actionFunction.Company.Id} has not been found");
                }
                if (actionFunction.Position == null)
                {
                    throw new BadHttpRequestException("Position must be fulfilled");
                }

                Position? position2 = _context.Positions.FirstOrDefault(p => p.Id == actionFunction.Position.Id);
                if (position2 == null)
                {
                    throw new BadHttpRequestException($"Position #{actionFunction.Position.Id} has not been found");
                }

                if (!(companyToTerminateEmployment.Positions!.Any(p => p.Id == position2.Id)))
                {
                    throw new BadHttpRequestException($"This position does not belong to company {companyToTerminateEmployment.Code}");
                }

                dbActionFunction.Position = position2;
                dbActionFunction.Company = companyToTerminateEmployment;
                if (create)
                {
                    _context.ActionFunctions.Add(dbActionFunction);
                }
                _context.SaveChanges();
                return _mapper.Map<ActionFunctionDto>(dbActionFunction);
        }
        throw new NotImplementedException();
    }
}