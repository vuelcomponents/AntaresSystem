using AntaresApi.Models;
using AntaresApi.Models.Document;
using AntaresApi.Models.Mail;
using AntaresApi.Models.Offer;
using AntaresApi.Models.Position;
using AntaresApi.Models.Status;
using AntaresApi.Models.StatusAction;
using AntaresApi.Models.StoreModel;
using Bogus.Extensions.Sweden;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace AntaresApi;

public class Seed
{
    private readonly DataContext _context;
    private readonly HttpContextAccessor _httpContextAccessor;
    public Seed(DataContext context, HttpContextAccessor accessor)
    {
        _context = context;
        
        _httpContextAccessor = accessor;
    }

    public void Release()
    {
        var models = _context.StoreModels.ToList();
        if (models.Count < 3)
        {
            SeedStoreModels();
        }
        var variantTypes = _context.VariantTypes.ToList();
        if (variantTypes.Count < 3)
        {
            SeedVariantTypes();
        }
        var systemFunctions = _context.SystemFunctions.ToList();
        if (systemFunctions.Count < 1)
        {
            SeedSystemFunctions();
        }
        var comparators = _context.Comparators.ToList();
        if (comparators.Count < 1)
        {
            SeedComparators();
        }
        // var actionFunctions = _context.ActionFunctions.ToList();
        // if (actionFunctions.Count < 1)
        // {
        //     SeedActionFunctions();
        // }
        var statusActionTriggers = _context.StatusActionTriggers.ToList();
        if (statusActionTriggers.Count < 4)
        {
            SeedStatusActionTriggers();
        }
        var positionUnits = _context.PositionUnits.ToList();
        if (positionUnits.Count < 2)
        {
            SeedPositionUnits();
        }
        var documentTypes = _context.DocumentTypes.ToList();
        if (!(documentTypes.Any(p=>p.Code == "Image")))
        {
            SeedDocumentTypes();
        }

        var status = _context.Statuses.FirstOrDefault(s => s.Code == "Registered");
        if (status == null)
        {
            SeedRegisteredStatus();
        }
        var unverified = _context.Statuses.FirstOrDefault(s => s.Code == "Unverified");
        if (unverified == null)
        {
            SeedUnverifiedStatus();
        }
        var openRecruitment = _context.Statuses.FirstOrDefault(s => s.Code == "Open Recruitment");
        if (openRecruitment == null)
        {
            SeedOpenRecruitmentStatus();
        }
        FancySeeds();
        FancySeedEmployees();
        FancySeedCompanies();
        FancySeedImages();
        FancySeedOffers();
        FancySeedMails();

    }

    private void SeedVariantTypes()
    {
        List<VariantType> variantTypes = new List<VariantType>
        {
            new VariantType
            {
                Code = "Numeric"
            },
            new VariantType
            {
                Code = "Date"
            },
            new VariantType
            {
                Code = "Variant Realisation"
            }
        };
        _context.VariantTypes.AddRange(variantTypes);
        _context.SaveChanges();
    }

    private void SeedStoreModels()
    {
        List<StoreModel> list = new List<StoreModel>
        {
            new StoreModel
            {
                Code = "Employee"
            },
            new StoreModel
            {
                Code = "Company"
            },
            new StoreModel
            {
                Code = "Document"
            },
            new StoreModel
            {
                Code = "Car"
            },

        };
        _context.StoreModels.AddRange(list);
        _context.SaveChanges();
    }
    private void SeedSystemFunctions()
    {
        List<SystemFunction> functions = new List<SystemFunction>
        {
            new SystemFunction
            {
                Code = "Mail",
                StoreModels = new List<StoreModel>
                {
                    _context.StoreModels.FirstOrDefault(s=>s.Code == "Document")!,
                    _context.StoreModels.FirstOrDefault(s=>s.Code == "Employee")!
                }
            },
            new SystemFunction
            {
                Code = "Hire",
                StoreModels = new List<StoreModel>
                {
                    _context.StoreModels.FirstOrDefault(s=>s.Code == "Employee")!,
                }
            },
            new SystemFunction
            {
                Code = "Terminate employment",
                StoreModels = new List<StoreModel>
                {
                    _context.StoreModels.FirstOrDefault(s=>s.Code == "Employee")!,
                }
            },
            new SystemFunction
            {
                Code = "Finish cooperation",
                StoreModels = new List<StoreModel>
                {
                    _context.StoreModels.FirstOrDefault(s=>s.Code == "Company")!,
                }
            },
        };
        _context.SystemFunctions.AddRange(functions);
        _context.SaveChanges();
    }
    private void SeedStatusActionTriggers()
    {
        List<StatusActionTrigger> triggers = new List<StatusActionTrigger>
        {
            new StatusActionTrigger
            {
                Code = "Start",
            },
            new StatusActionTrigger
            {
                Code = "End",
            },
          
        };
        _context.StatusActionTriggers.AddRange(triggers);
        _context.SaveChanges();
    }
    private void SeedActionFunctions()
    {
        List<ActionFunction> functions = new List<ActionFunction>
        {
            new ActionFunction
            {
                Code = "functionEmployee1",
                Description = "Funckja emp1",
                StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                SystemFunction = _context.SystemFunctions.FirstOrDefault(s=>s.Code=="Mail")!,
             
            },
            new ActionFunction
            {
                Code = "functionCompany1",
                Description = "Funckja com1",
                StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Company")!,
                SystemFunction = _context.SystemFunctions.FirstOrDefault(s=>s.Code=="Mail")!,
  
            },
            new ActionFunction
            {
                Code = "functionDocument1",
                Description = "Funckja doc1",
                StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Document")!,
                SystemFunction = _context.SystemFunctions.FirstOrDefault(s=>s.Code=="Mail")!,
            },
        };
        _context.ActionFunctions.AddRange(functions);
        _context.SaveChanges();
    }
    private void SeedComparators()
    {
        List<Comparator> comparators = new List<Comparator>
        {
            new Comparator
            {
                Code = ">",
            },
            new Comparator
            {
                Code = ">=",
            },
            new Comparator
            {
                Code = "==",
            },
            new Comparator
            {
                Code = "<",
            },
            new Comparator
            {
                Code = "<=",
            },
        };
        _context.Comparators.AddRange(comparators);
        _context.SaveChanges();
    }
    private void SeedDocumentTypes()
    {
        List<_DocumentType> dtypes = new List<_DocumentType>
        {
            new _DocumentType
            {
                Code = "Image",
                Description="Unsigned picture"
            }
        };
        _context.DocumentTypes.AddRange(dtypes);
        _context.SaveChanges();
    }
    private void SeedPositionUnits()
    {
        List<PositionUnit> positionUnits = new List<PositionUnit>
        {
            new PositionUnit
            {
                Code = "Position",
            },
            new PositionUnit
            {
                Code = "Business Unit",
            },
        };
        _context.PositionUnits.AddRange(positionUnits);
        _context.SaveChanges();
    }

    private void SeedRegisteredStatus()
    {
        _context.Statuses.Add(new Status
        {
            Code = "Registered",
            Description = "Registered",
            Reserved = true,
            StoreModel = _context.StoreModels.FirstOrDefault(s => s.Code == "Employee")!,
        });
        _context.SaveChanges();
    }
    private void SeedUnverifiedStatus()
    {
        _context.Statuses.Add(new Status
        {
            Code = "Unverified",
            Description = "Unverified",
            Reserved = true,
            StoreModel = _context.StoreModels.FirstOrDefault(s => s.Code == "Employee")!,
        });
        _context.SaveChanges();
    }
    private void SeedOpenRecruitmentStatus()
    {
        _context.Statuses.Add(new Status
        {
            Code = "Open Recruitment",
            Description = "Open Recruitment",
            Reserved = true,
            StoreModel = _context.StoreModels.FirstOrDefault(s => s.Code == "Employee")!,
        });
        _context.SaveChanges();
    }
    private void FancySeeds()
    {
        if (_context.Documents.ToList().Count <1)
        {
            var documents = new List<_Document>();
            byte[] fileContent = System.IO.File.ReadAllBytes("./webpage-logo.png");
            _context.Documents.Add(new _Document
            {
                FileData = fileContent,
                FileName = "email-logo.png",
                Description = "Email Logo",
                StoreModel = _context.StoreModels.FirstOrDefault(s => s.Code == "Employee")!,
                Code = "Email Logo"
            });
            _context.SaveChanges();
        }
        if (_context.Documents.FirstOrDefault(c => c.Code == "Email Logo") == null)
        {
       
            byte[] fileContent = System.IO.File.ReadAllBytes("./webpage-logo.png");
            _context.Documents.Add(new _Document
            {
                FileData = fileContent,
                FileName = "email-logo.png",
                Description = "Email Logo",
                StoreModel = _context.StoreModels.FirstOrDefault(s => s.Code == "Employee")!,
                Code = "Email Logo"
            });
            _context.SaveChanges();
        }

   
        if (_context.Variants.ToList().Count < 1)
        {
            List<Variant> variants = new List<Variant>
            {
                new Variant
                {
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                    Code = "Efficiency",
                    Description = "Efficiency factor based on productivity statistics",
                    VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Numeric")!
                },
                new Variant
                {
                    Code = "Quality",
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                    Description = "Quality score based on performance standards",
                    VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Numeric")!
                },
                new Variant
                {
                    Code = "CustomerSatisfaction",
                    Description = "Customer satisfaction rating",
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                    VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Numeric")!
                },
                new Variant
                {
                    Code = "LeadTime",
                    Description = "Average lead time for task completion",
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                    VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Numeric")!
                },
                new Variant
                {
                    Code = "ErrorRate",
                    Description = "Percentage of errors in the process",
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                    VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Numeric")!
                },
                new Variant
                {
                    Code = "OnTimeDelivery",
                    Description = "Percentage of tasks delivered on time",
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                    VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Numeric")!
                },
                new Variant
                {
                    Code = "DateOfBeginningJobExperience",
                    Global = true,
                    Description = "Date of first time job experience",
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                    VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Date")!
                },
                new Variant
                {
                    Code = "DateOfLastTraining",
                    Global = true,
                    Description = "Date of the most recent training session",
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                    VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Date")!
                },
                new Variant
                {
                    Code = "ContractEndDate",
                    Global = true,
                    Description = "End date of the employment contract",
                    StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                    VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Date")!
                },
               new Variant
               {
                 Code = "EnglishLevel",
                 Description = "Level of English communication",
                 Global = true,
                 StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
                 VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
                 VariantRealisations = new List<VariantRealisation>
                 {
                     new VariantRealisation
                     {
                         Code = "A1",
                         Description = "A1 - a basic communication skills"
                     },
                     new VariantRealisation
                     {
                         Code = "A2",
                         Description = "A2 - elementary communication skills"
                     },
                     new VariantRealisation
                     {
                         Code = "B1",
                         Description = "B1 - intermediate communication skills"
                     },
                     new VariantRealisation
                     {
                         Code = "B2",
                         Description = "B2 - upper-intermediate communication skills"
                     },
                     new VariantRealisation
                     {
                         Code = "C1",
                         Description = "C1 - advanced communication skills"
                     },
                     new VariantRealisation
                     {
                         Code = "C2",
                         Description = "C1 - proficiency/native"
                     }
                     
                 }
               },
                 new Variant
    {
        Code = "TechnicalSkills",
        Description = "Proficiency in technical skills",
        Global = true,
        StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
        VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
        VariantRealisations = new List<VariantRealisation>
        {
            new VariantRealisation
            {
                Code = "Basic",
                Description = "Basic technical skills"
            },
            new VariantRealisation
            {
                Code = "Intermediate",
                Description = "Intermediate technical skills"
            },
            new VariantRealisation
            {
                Code = "Advanced",
                Description = "Advanced technical skills"
            },
            new VariantRealisation
            {
                Code = "Expert",
                Description = "Expert technical skills"
            },
            new VariantRealisation
            {
                Code = "Specialist",
                Description = "Specialist technical skills"
            }
        }
    },
    new Variant
    {
        Code = "LeadershipQualities",
        Description = "Demonstrated leadership qualities",
        Global = true,
        StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
        VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
        VariantRealisations = new List<VariantRealisation>
        {
            new VariantRealisation
            {
                Code = "Visionary",
                Description = "Visionary leadership qualities"
            },
            new VariantRealisation
            {
                Code = "Inspirational",
                Description = "Inspirational leadership qualities"
            },
            new VariantRealisation
            {
                Code = "Strategic",
                Description = "Strategic leadership qualities"
            },
            new VariantRealisation
            {
                Code = "Collaborative",
                Description = "Collaborative leadership qualities"
            },
            new VariantRealisation
            {
                Code = "Adaptable",
                Description = "Adaptable leadership qualities"
            },
            }
            },
        
              
    new Variant
    {
        Code = "TeamCollaboration",
        Description = "Effectiveness in team collaboration",
        Global = true,
        StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
        VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
        VariantRealisations = new List<VariantRealisation>
        {
            new VariantRealisation
            {
                Code = "HighlyCollaborative",
                Description = "Highly collaborative team member"
            },
            new VariantRealisation
            {
                Code = "AdaptableTeamPlayer",
                Description = "Adaptable team player"
            },
            new VariantRealisation
            {
                Code = "EffectiveCommunicator",
                Description = "Effective communicator in team settings"
            },
            new VariantRealisation
            {
                Code = "SupportiveTeamMember",
                Description = "Supportive team member"
            },
            new VariantRealisation
            {
                Code = "ConflictResolution",
                Description = "Skilled in conflict resolution within the team"
            }
        }
    },
    new Variant
    {
        Code = "CreativityIndex",
        Global = true,
        Description = "Creativity index based on innovative contributions",
        StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
        VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
        VariantRealisations = new List<VariantRealisation>
        {
            new VariantRealisation
            {
                Code = "InnovativeThinker",
                Description = "Innovative thinker with original ideas"
            },
            new VariantRealisation
            {
                Code = "ProblemSolver",
                Description = "Effective problem solver using creative solutions"
            },
            new VariantRealisation
            {
                Code = "OpenToNewIdeas",
                Description = "Open to new ideas and approaches"
            },
            new VariantRealisation
            {
                Code = "CollaborativeInnovation",
                Description = "Collaborative approach to innovation"
            },
            new VariantRealisation
            {
                Code = "ContinuousImprovement",
                Description = "Contributor to continuous improvement initiatives"
            }
        }
    },
               new Variant
    {
        Code = "Adaptability",
        Description = "Ability to adapt to changing work environments",
        Global = true,
        StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
        VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
        VariantRealisations = new List<VariantRealisation>
        {
            new VariantRealisation
            {
                Code = "QuickAdaptor",
                Description = "Quick adaptor to changes"
            },
            new VariantRealisation
            {
                Code = "FlexibleApproach",
                Description = "Flexible approach to varying work conditions"
            },
            new VariantRealisation
            {
                Code = "OpenToFeedback",
                Description = "Open to feedback and adjustments"
            },
            new VariantRealisation
            {
                Code = "ProactiveAdaptation",
                Description = "Proactive in adapting to new challenges"
            },
            new VariantRealisation
            {
                Code = "ResilientResponse",
                Description = "Resilient response to unexpected changes"
            }
        }
    },
    new Variant
    {
        Code = "CommunicationSkills",
        Global = true,
        Description = "Effectiveness in communication skills",
        StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
        VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
        VariantRealisations = new List<VariantRealisation>
        {
            new VariantRealisation
            {
                Code = "ClearExpression",
                Description = "Clear and articulate expression"
            },
            new VariantRealisation
            {
                Code = "ActiveListening",
                Description = "Active listening skills"
            },
            new VariantRealisation
            {
                Code = "EmpatheticCommunication",
                Description = "Empathetic communication style"
            },
            new VariantRealisation
            {
                Code = "ConflictResolution",
                Description = "Effective conflict resolution through communication"
            },
            new VariantRealisation
            {
                Code = "PersuasiveCommunicator",
                Description = "Persuasive and influential communicator"
            }
        }
    },
                new Variant
    {
        Code = "ProblemSolvingAbility",
        Global = true,
        Description = "Problem-solving ability in complex situations",
        StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
        VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
        VariantRealisations = new List<VariantRealisation>
        {
            new VariantRealisation
            {
                Code = "AnalyticalThinker",
                Description = "Analytical thinker with strong problem-solving skills"
            },
            new VariantRealisation
            {
                Code = "CreativeProblemSolver",
                Description = "Creative problem solver with innovative solutions"
            },
            new VariantRealisation
            {
                Code = "CollaborativeApproach",
                Description = "Collaborative approach to problem-solving"
            },
            new VariantRealisation
            {
                Code = "SystematicApproach",
                Description = "Systematic and structured approach to problem-solving"
            },
            new VariantRealisation
            {
                Code = "AdaptiveProblemSolver",
                Description = "Adaptive problem solver in dynamic environments"
            }
        }
    },
    new Variant
    {
        Code = "TimeManagement",
        Global = true,
        Description = "Efficiency in time management",
        StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
        VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
        VariantRealisations = new List<VariantRealisation>
        {
            new VariantRealisation
            {
                Code = "PrioritizationSkills",
                Description = "Effective prioritization skills"
            },
            new VariantRealisation
            {
                Code = "DeadlineOrientation",
                Description = "Deadline-oriented work approach"
            },
            new VariantRealisation
            {
                Code = "MultiTaskingAbility",
                Description = "Ability to multitask efficiently"
            },
            new VariantRealisation
            {
                Code = "ProactiveTimeManagement",
                Description = "Proactive time management strategies"
            },
            new VariantRealisation
            {
                Code = "TimeOptimization",
                Description = "Optimization of time for maximum productivity"
            }
        }
    },
    new Variant
    {
        Code = "InnovationIndex",
        Global = true,
        Description = "Index measuring innovative contributions",
        StoreModel = _context.StoreModels.FirstOrDefault(s=>s.Code=="Employee")!,
        VariantType = _context.VariantTypes.FirstOrDefault(vt => vt.Code == "Variant Realisation")!,
        VariantRealisations = new List<VariantRealisation>
        {
            new VariantRealisation
            {
                Code = "IdeationEnthusiast",
                Description = "Enthusiastic ideation and brainstorming"
            },
            new VariantRealisation
            {
                Code = "ContinuousLearning",
                Description = "Commitment to continuous learning and improvement"
            },
            new VariantRealisation
            {
                Code = "RiskTaking",
                Description = "Willingness to take calculated risks for innovation"
            },
            new VariantRealisation
            {
                Code = "CrossFunctionalCollaboration",
                Description = "Collaboration across functions for innovative solutions"
            },
            new VariantRealisation
            {
                Code = "PrototypeDevelopment",
                Description = "Hands-on involvement in prototype development"
            }
        }
    },
            };
            _context.Variants.AddRange(variants);
            _context.SaveChanges();
        }
    }

    private void FancySeedEmployees()
    {
        if (_context.Employees.ToList().Count < 1)
        {
            var faker = new Bogus.Faker<Employee>()
                .RuleFor(e => e.FirstName, f => f.Person.FirstName)
                .RuleFor(e => e.LastName, f => f.Person.LastName)
                .RuleFor(e => e.Pesel, f => f.Person.Personnummer())
                .RuleFor(e => e.Bsn, f => f.Random.AlphaNumeric(10))
                .RuleFor(e => e.Email, f => f.Person.Email)
                .RuleFor(e => e.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(e => e.HouseAndLocalNumber, f => f.Address.BuildingNumber())
                .RuleFor(e => e.StreetName, f => f.Address.StreetName())
                .RuleFor(e => e.PostCode, f => f.Address.ZipCode())
                .RuleFor(e => e.City, f => f.Address.City())
                .RuleFor(e => e.SubHouseAndLocalNumber, f => f.Address.BuildingNumber())
                .RuleFor(e => e.SubStreetName, f => f.Address.StreetName())
                .RuleFor(e => e.SubPostcode, f => f.Address.ZipCode())
                .RuleFor(e => e.SubCity, f => f.Address.City());
            var employees = faker.Generate(10);
            var variants = _context.Variants.ToList();
            
            var fakerRealisation = new Bogus.Faker<Realisation>()
                .RuleFor(r => r.Variant, f => f.PickRandom(variants))
                .RuleFor(r => r.VariantRealisation, (f, r) =>
                {
                    if (r.Variant.VariantType.Code == "Variant Realisation")
                        return f.PickRandom(r.Variant.VariantRealisations);
                    return null;
                })
                .RuleFor(r => r.DateValue, f => f.Date.Recent())
                .RuleFor(r => r.NumericValue, f => f.Random.Double());
            

            foreach (var employee in employees)
            {
                employee.Realisations = fakerRealisation.Generate(5);
                employee.StoreModel = _context.StoreModels.FirstOrDefault(sm => sm.Code == "Employee")!;
            }
            _context.Employees.AddRange(employees);
            _context.SaveChanges();
            };
    }
        private void FancySeedCompanies()
        {
        if (_context.Companies.ToList().Count < 1)
        {
            var variants = _context.Variants
                .Include(v=>v.VariantRealisations)
                .Where(v=>v.StoreModel!.Code == "Employee")
                .ToList();
      
            var fakerRequirement = new Bogus.Faker<Realisation>()
                .RuleFor(e => e.Variant, f => f.PickRandom(variants))
                .RuleFor(r => r.VariantRealisation, (f, r) =>
                {
                    if (r.Variant.VariantType.Code == "Variant Realisation")
                        return f.PickRandom(r.Variant.VariantRealisations);
                    return null;
                })
                .RuleFor(r => r.DateValue, f => f.Date.Recent())
                .RuleFor(r => r.NumericValue, f => f.Random.Double())
                .RuleFor(e => e.Comparator, (f, e) => f.PickRandom(_context.Comparators.ToList()));


            var fakerPosition = new Bogus.Faker<Position>()
                .RuleFor(e => e.Code, f => f.Name.JobTitle())
                .RuleFor(e => e.Description, f => f.Name.JobType())
                .RuleFor(e => e.Demand, f => f.Random.Double())
                .RuleFor(e => e.PositionUnit, f => _context.PositionUnits.FirstOrDefault(p => p.Code == "Position"))
                .RuleFor(e => e.Employees, f => new List<Employee>())
                .RuleFor(e=>e.StoreModel, f=> _context.StoreModels.FirstOrDefault(s=>s.Code == "Employee"))
                .RuleFor(e => e.Requirements, f => fakerRequirement.Generate(5))
                ;
            var faker = new Bogus.Faker<Company>()
                .RuleFor(e => e.HouseAndLocalNumber, f => f.Address.BuildingNumber())
                .RuleFor(e => e.StreetName, f => f.Address.StreetName())
                .RuleFor(e => e.PostCode, f => f.Address.ZipCode())
                .RuleFor(e => e.Code, f => f.Company.CompanyName())
                .RuleFor(e => e.Description, (f,c) => $"Description of {c.Code}")
                .RuleFor(e => e.Country, f => f.Address.Country())
                .RuleFor(e => e.PostCode, f => f.Address.ZipCode())
                .RuleFor(e => e.City, f => f.Address.City())
                .RuleFor(e => e.StoreModel, f => _context.StoreModels.FirstOrDefault(s => s.Code == "Company")!)
                .RuleFor(e => e.Positions, f => fakerPosition.Generate(5));

            var companies = faker.Generate(10);
            
            
            _context.Companies.AddRange(companies);
            _context.SaveChanges();
            };
        }

        private void FancySeedMails()
        {
            if (_context.Mails.ToList().Count < 1)
            {
                var companies = _context.Companies.ToList();
                var fakerMail = new Bogus.Faker<Mail>()
                    .RuleFor(m=>m.Code, f=>f.PickRandom(companies).Code)
                    .RuleFor(m=>m.Title, (f,m)=>$"Welcome to {m.Code} recruitment process")
                    .RuleFor(m => m.Message, (f,m) => $"<h2>Welcome to the recruitment process!</h2>"
                                                    + $"<p>It's great that you applied to {m.Code}. We appreciate your interest in joining our team.</p>"
                                                    + $"<p>Here are some details about the recruitment process:</p>"
                                                    + "<ul>"
                                                    + "<li>Interviews will be conducted to assess your skills and qualifications.</li>"
                                                    + "<li>We will review your application and get back to you as soon as possible.</li>"
                                                    + "<li>If you have any questions, feel free to reach out to our HR department.</li>"
                                                    + "</ul>"
                                                    + "<p>Thank you again for choosing us. We look forward to the possibility of working together!</p>"
                    )
                    .RuleFor(m=>m.Documents, (f,m)=> new List<_Document>
                    {
                        f.PickRandom(_context.Documents
                            .Include(d=>d.Companies)
                            .Where(d=>d.Companies!.Any(c=>c.Code == m.Code)
                            ).ToList())
                    })
                    ;
                _context.Mails.AddRange(fakerMail.Generate(10));
                _context.SaveChanges();
            }
        }
        
        private void FancySeedImages()
        {
            if (_context.DocumentTypes.FirstOrDefault(d => d.Code == "CompanyFakeLogo") == null)
            {
                List<_DocumentType> dtypes = new List<_DocumentType>
                {
                    new _DocumentType
                    {
                        Code = "CompanyFakeLogo",
                        Description="Fake company logo"
                    }
                };
                _context.DocumentTypes.AddRange(dtypes);
                _context.SaveChanges();
            }
            if(_context.Documents
                   .Include(d=>d.DocumentType)
                   .Where(d=>d.DocumentType!.Code == "CompanyFakeLogo")
                   .ToList().Count <1)
            {
                var imageFaker = new Bogus.Faker<_Document>()
                        .RuleFor(i=>i.FileData, f=>
                        {
                            var randomImageIndex = f.Random.Number(1, 6);
                            var fileName = $"fakelogo{randomImageIndex}.jpg";
                            byte[] fileContent = System.IO.File.ReadAllBytes($"./Utils/{fileName}");
                            return fileContent;
                        })
                        .RuleFor(i=>i.FileName, (f) =>
                        {
                            var randomImageIndex = f.Random.Number(1, 6);
                            var fileName = $"fakelogo{randomImageIndex}.jpg";
                            return fileName;
                        })
                        .RuleFor(i=>i.Companies, f=> new List<Company>
                        {
                            f.PickRandom(_context.Companies.ToList())
                        })
                        .RuleFor(d=>d.DocumentType, f=>_context.DocumentTypes.FirstOrDefault(dt=>dt.Code == "CompanyFakeLogo")!)
                        .RuleFor(i=>i.UploadDate, f=>DateTime.Now)
                        .RuleFor(i=>i.StoreModel, ()=>_context.StoreModels.FirstOrDefault(s=>s.Code=="Company")!)
                        .RuleFor(i=>i.Code, (f,i)=>$"Logo {i.Companies!.FirstOrDefault()!.Code}")
                        .RuleFor(i=>i.Description, (f,i)=>$"Fake logo for {i.Companies!.FirstOrDefault()!.Code}")
                    ;
                ;
                var images = imageFaker.Generate(10);
                _context.Documents.AddRange(images);
                _context.SaveChanges();
            }
        }
        private void FancySeedOffers()
        {
            if(_context.Offers.ToList().Count <1)
            {
                var offerFaker = new Bogus.Faker<Offer>()
                    .RuleFor(o => o.Company, f => f.PickRandom(_context.Companies.Include(c => c.Positions).ToList()))
                    .RuleFor(o => o.Position, (f, o) => f.PickRandom(o.Company.Positions))
                    .RuleFor(o => o.Code, (f, o) => $"Hiring to {o.Company.Description}")
                    .RuleFor(o => o.Title, (f, o) => $"We are hiring to {o.Company.Description}")
                    .RuleFor(c=>c.Image, (f)=>
                        f.PickRandom(_context.Documents.Where(dt=>dt.DocumentType!.Code == "CompanyFakeLogo").ToList())
                            )
                    .RuleFor(o=>o.Global, f=>true)
                    .RuleFor(o => o.Message, (f, o) => $@"
                        <h1>We are currently hiring for the position of {o.Position.Description}</h1>
                        <p>We are looking for individuals who {o.Position.Demand}.</p>
                        <p>If you are interested in joining our team, please apply by submitting your resume and cover letter. We are excited to hear from passionate and qualified candidates.</p>
                        <p>Feel free to reach out to us with any questions or for more information about the application process. Your future career awaits!</p>
                    ");
                ;
                var offers = offerFaker.Generate(10);
                _context.Offers.AddRange(offers);
                _context.SaveChanges();
            }
        }
    

}