﻿
using AntaresApi.Dto;
using AntaresApi.Dto.Action;
using AntaresApi.Dto.Car;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Company;
using AntaresApi.Dto.Document;
using AntaresApi.Dto.House;
using AntaresApi.Dto.Mail;
using AntaresApi.Dto.Offer;
using AntaresApi.Dto.Plan;
using AntaresApi.Dto.Position;
using AntaresApi.Dto.Recruitment;
using AntaresApi.Dto.Status;
using AntaresApi.Dto.Variant.Realisation;
using AntaresApi.Dto.Variant.Variant;
using AntaresApi.Dto.Variant.VariantRealisation;
using AntaresApi.Dto.Variant.VariantType;
using AntaresApi.Models;
using AntaresApi.Models.Car;
using AntaresApi.Models.Document;
using AntaresApi.Models.House;
using AntaresApi.Models.Mail;
using AntaresApi.Models.Offer;
using AntaresApi.Models.Position;
using AntaresApi.Models.Recruitment;
using AntaresApi.Models.Status;
using AntaresApi.Models.StatusAction;
using AntaresApi.Models.StoreModel;
using AutoMapper;

namespace AntaresApi.Core;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Company, CompanyDto>();
        CreateMap<Company, IdDto>();
        CreateMap<Company, IdCodeDto>();
        CreateMap<Company, CompanyLiteDto>();
        CreateMap<Company, CompanyShortDto>();
        CreateMap<Employee, EmployeeDto>();
        CreateMap<Employee, IdDto>();
        CreateMap<Employee, EmployeeShortDto>();
        CreateMap<Employee, EmployeeSuperShortDto>();
        CreateMap<Variant, VariantDto>();
        CreateMap<Variant, IdDto>();
        CreateMap<Variant, IdCodeDto>();
        CreateMap<Variant, VariantShortDto>();
        CreateMap<Realisation, RealisationDto>();
        CreateMap<Realisation, IdDto>();
        CreateMap<Realisation, IdCodeDto>();
        CreateMap<Realisation, RealisationShortDto>();
        CreateMap<VariantRealisation, VariantRealisationDto>();
        CreateMap<VariantRealisation, IdCodeDto>();
        CreateMap<VariantType, VariantTypeDto>();
        CreateMap<_Document, _DocumentDto>();
        CreateMap<_Document, IdDto>();
        CreateMap<_Document, IdCodeDto>();
        CreateMap<Status, StatusDto>();
        CreateMap<Status, IdDto>();
        CreateMap<StoreModel, IdDto>();
        CreateMap<StoreModel, IdCodeDto>();
        CreateMap<StatusAction, StatusActionDto>();
        CreateMap<ActionFunction, ActionFunctionDto>();
        CreateMap<StatusActionTrigger, StatusActionTriggerDto>();
        CreateMap<SystemFunction, SystemFunctionDto>();
        CreateMap<_DocumentType, _DocumentTypeDto > ();
        CreateMap<_DocumentType, IdCodeDto > ();
        CreateMap<_DocumentType, IdDto > ();
        CreateMap<Comparator, IdCodeDto>();
        CreateMap<Mail, MailDto>();
        CreateMap<Position, PositionDto>();
        CreateMap<Position, PositionShortDto>();
        CreateMap<Position, IdCodeDto>();
        CreateMap<Position, PositionLiteDto>();
        CreateMap<PositionUnit, IdCodeDto>();
        CreateMap<House, HouseDto>();
        CreateMap<House, IdDto>();
        CreateMap<House, IdCodeDto>();
        CreateMap<Car, CarDto>();
        CreateMap<Car, IdDto>();
        CreateMap<Car, IdCodeDto>();
        CreateMap<Offer, OfferDto>();
        CreateMap<Offer, IdDto>();
        CreateMap<Offer, IdCodeDto>();
        CreateMap<Offer, OfferShortDto>();
        CreateMap<Recruitment, RecruitmentDto>();
        CreateMap<Recruitment, IdCodeDto>();
        CreateMap<Recruitment, IdDto>();
        CreateMap<RecruitmentContact, RecruitmentContactDto>();
        CreateMap<RecruitmentContact, IdCodeDto>();
        CreateMap<RecruitmentContact, IdDto>();
        CreateMap<Plan,PlanDto>();
        CreateMap<Plan, IdDto>();
        CreateMap<Plan, IdCodeDto>();
    }
}