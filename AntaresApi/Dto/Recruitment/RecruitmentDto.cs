﻿using AntaresApi.Dto.Common;
using AntaresApi.Dto.Document;
using AntaresApi.Dto.Offer;
using AntaresApi.Dto.Position;
using AntaresApi.Dto.Status;
using AntaresApi.Dto.Variant.Variant;
using AntaresApi.Models.Recruitment;

namespace AntaresApi.Dto.Recruitment;

public class RecruitmentDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public OfferDto? Offer { get; set; }
    public long? OfferId {get; set; }
    public bool? Open { get; set; }

    public List<EmployeeDto>? Employees { get; set; }
    public IdCodeDto? StoreModel { get; set; }
    public List<VariantDto>? Variants { get; set; }
    public List<_DocumentTypeDto>? DocumentTypes { get; set; }
    public StatusDto? Status { get; set; }
    public long? StatusId { get; set; }
}