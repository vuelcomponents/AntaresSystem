import RealisationService from "@/services/RealisationService";
import {VariantRealisationService} from "@/services/VariantRealisationService";
import {VariantService} from "@/services/VariantService";
import {EmployeeService} from "@/services/EmployeeService";
import {CompanyService} from "@/services/CompanyService";
import RealisationClass from "@/views/dashboard/DataView/variants/RealisationClass";
import VariantRealisationClass from "@/views/dashboard/DataView/variants/VariantRealisationClass";
import VariantClass from "@/views/dashboard/DataView/variants/VariantClass";
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";
import CompanyClass from "@/views/dashboard/DataView/companies/CompanyClass";
import EmployeeColumns from "@/views/dashboard/DataView/employees/EmployeeColumns";
import CompanyColumns from "@/views/dashboard/DataView/companies/CompanyColumns";
import RealisationColumn from "@/views/dashboard/DataView/variants/RealisationColumn";
import VariantRealisationColumn from "@/views/dashboard/DataView/variants/VariantRealisationColumn";
import EmployeeShortColumn from "@/views/dashboard/DataView/employees/EmployeeShortColumn";
import {VariantTypeService} from "@/services/VariantTypeService";
import VariantTypeClass from "@/views/dashboard/DataView/variants/VariantTypeClass";
import VariantTypeColumn from "@/views/dashboard/DataView/variants/VariantTypeColumn";
import CompanyShortColumn from "@/views/dashboard/DataView/companies/CompanyShortColumn";

import {DocumentService} from "@/services/DocumentService";
import DocumentClass from "@/views/dashboard/DataView/DocumentClass";
import DocumentColumn from "@/views/dashboard/DataView/DocumentColumn";
import DocumentShortColumn from "@/views/dashboard/DataView/DocumentShortColumn";
import StatusColumn from "@/views/dashboard/DataView/status/StatusColumn";
import StatusClass from "@/views/dashboard/DataView/status/StatusClass";
import {StatusService} from "@/services/StatusService";
import {StoreModelService} from "@/services/store/StoreModelService";
import StoreModelClass from "@/views/dashboard/DataView/StoreModelClass";
import EmployeeLife from "@/views/dashboard/DataView/employees/EmployeeLife";
import DocumentLife from "@/views/dashboard/DataView/documents/DocumentLife";
import StatusLife from "@/views/dashboard/DataView/status/StatusLife";
import Life from "@/utils/classes/Life";
import CompanyLife from "@/views/dashboard/DataView/companies/CompanyLife";
import VariantLife from "@/views/dashboard/DataView/variants/VariantLife";
import EmployeeCrudController from "@/views/dashboard/DataView/employees/controllers/EmployeeCrudController";
import CompanyCrudController from "@/views/dashboard/DataView/companies/controllers/CompanyCrudController";
import DocumentCrudController from "@/views/dashboard/DataView/documents/controllers/DocumentCrudController";
import StatusCrudController from "@/views/dashboard/DataView/status/controllers/StatusCrudController";
import VariantCrudController from "@/views/dashboard/DataView/variants/controllers/VariantCrudController";
import {StatusActionService} from "@/services/StatusActionService";
import StatusActionClass from "@/views/dashboard/DataView/status/StatusActionClass";
import StatusActionColumn from "@/views/dashboard/DataView/status/StatusActionColumn";
import StatusActionTriggerClass from "@/views/dashboard/DataView/status/StatusActionTriggerClass";
import {ActionFunctionService} from "@/services/ActionFunctionService";
import {StatusActionTriggerService} from "@/services/StatusActionTriggerService";
import StatusActionCrudController from "@/cruds/StatusActionCrudController";
import ActionFunctionClass from "@/views/dashboard/DataView/action/ActionFunctionClass";
import {SystemFunctionService} from "@/services/SystemFunctionService";
import SystemFunctionClass from "@/views/dashboard/DataView/action/SystemFunctionClass";
import ActionFunctionColumns from "@/views/dashboard/DataView/action/ActionFunctionColumns";
import {DocumentTypeService} from "@/services/DocumentTypeService";
import DocumentTypeClass from "@/views/dashboard/DataView/documentTypes/DocumentTypeClass";
import DocumentTypeColumn from "@/views/dashboard/DataView/documentTypes/DocumentTypeColumn";
import DocumentTypeCrudController from "@/cruds/DocumentTypeCrudController";
import DocumentTypeLife from "@/views/dashboard/DataView/documentTypes/DocumentTypeLife";
import DocumentTypeMenu from "@/views/dashboard/DataView/documentTypes/DocumentTypeMenu";
import ActionFunctionCrudController from "@/views/dashboard/DataView/action/controllers/ActionFunctionCrudController";
import RequirementClass from "@/views/dashboard/DataView/variants/RequirementClass";
import RequirementColumn from "@/views/dashboard/DataView/variants/RequirementColumn";
import {ComparatorService} from "@/services/ComparatorService";
import {MailService} from "@/services/MailService";
import MailClass from "@/views/dashboard/DataView/mails/MailClass";
import MailColumns from "@/views/dashboard/DataView/mails/MailColumns";
import MailLife from "@/views/dashboard/DataView/mails/MailLife";
import MailCrudController from "@/views/dashboard/DataView/mails/controllers/MailCrudController";
import PositionClass from "@/views/dashboard/DataView/companies/PositionClass";
import PositionUnitClass from "@/views/dashboard/DataView/companies/PositionUnitClass";
import PositionColumn from "@/views/dashboard/DataView/companies/PositionColumn";
import {PositionService} from "@/services/PositionService";
import {PositionUnitService} from "@/services/PositionUnitService";
import {HouseService} from "@/services/HouseService";
import HouseClass from "@/views/dashboard/DataView/houses/HouseClass";
import HouseColumns from "@/views/dashboard/DataView/houses/HouseColumns";
import HouseLife from "@/views/dashboard/DataView/houses/HouseLife";
import HouseCrudController from "@/views/dashboard/DataView/houses/controllers/HouseCrudController";
import {CarService} from "@/services/CarService";
import CarClass from "@/views/dashboard/DataView/cars/CarClass";
import CarColumns from "@/views/dashboard/DataView/cars/CarColumns";
import CarLife from "@/views/dashboard/DataView/cars/CarLife";
import CarCrudController from "@/views/dashboard/DataView/cars/controllers/CarCrudController";
import ImageClass from "@/views/dashboard/DataView/documents/ImageClass";
import OfferClass from "@/views/dashboard/DataView/offers/OfferClass";
import {OfferService} from "@/services/OfferService";
import OfferColumns from "@/views/dashboard/DataView/offers/OfferColumns";
import OfferList from "@/views/dashboard/DataView/offers/root/OfferList.vue";
import OfferLife from "@/views/dashboard/DataView/offers/OfferLife";
import OfferCrudController from "@/views/dashboard/DataView/offers/controllers/OfferCrudController";
import RecruitmentCrudController
    from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/controllers/RecruitmentCrudController";
import RecruitmentLife from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentLife";
import RecruitmentColumns from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentColumns";
import RecruitmentClass from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentClass";
import {RecruitmentService} from "@/services/RecruitmentService";
import VariantColumns from "@/views/dashboard/DataView/variants/VariantColumns";
import StageCrudController from "@/cruds/StageCrudController";
import StageColumns from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/StageColumns";
import StageClass from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/StageClass";
import {StageService} from "@/services/StageService";

const ServiceBunch = {
    realisations:RealisationService,
    realisation:RealisationService,
    variantRealisations:VariantRealisationService,
    variantRealisation:VariantRealisationService,
    variant:VariantService,
    variants:VariantService,
    employee:EmployeeService,
    employees:EmployeeService,
    company:CompanyService,
    companies:CompanyService,
    variantType:VariantTypeService,
    variantTypes:VariantTypeService,
    document:DocumentService,
    documents:DocumentService,
    status:StatusService,
    transitionTo:StatusService,
    storeModel:StoreModelService,
    statusAction:StatusActionService,
    statusActions:StatusActionService,
    statusActionTrigger:StatusActionTriggerService,
    statusActionTriggers:StatusActionTriggerService,
    actionFunction:ActionFunctionService,
    actionFunctions:ActionFunctionService,
    systemFunction:SystemFunctionService,
    systemFunctions:SystemFunctionService,
    documentType:DocumentTypeService,
    documentTypes:DocumentTypeService,
    requirements:RealisationService,
    requirement:RealisationService,
    comparator:ComparatorService,
    mail:MailService,
    mails:MailService,
    position:PositionService,
    positions:PositionService,
    positionUnit:PositionUnitService,
    positionUnits:PositionUnitService,
    house:HouseService,
    houses:HouseService,
    passengers:EmployeeService,
    drivers:EmployeeService,
    car:CarService,
    cars:CarService,
    driverCars:CarService,
    passengerCars:CarService,
    image:DocumentService,
    images:DocumentService,
    offer:OfferService,
    offers:OfferService,
    recruitmentProcess:RecruitmentService,
    recruitmentProcesses:RecruitmentService,
    recruitment:RecruitmentService,
    recruitments:RecruitmentService,
    stage:StageService,
    stages:StageService
}
const ClassBunch = {
    realisations:RealisationClass,
    realisation:RealisationClass,
    variantRealisations:VariantRealisationClass,
    variantRealisation:VariantRealisationClass,
    variant:VariantClass,
    variants:VariantClass,
    employee:EmployeeClass,
    employees:EmployeeClass,
    company:CompanyClass,
    companies:CompanyClass,
    variantType:VariantTypeClass,
    variantTypes:VariantTypeClass,
    document:DocumentClass,
    documents:DocumentClass,
    status:StatusClass,
    transitionTo:StatusClass,
    storeModel:StoreModelClass,
    statusAction:StatusActionClass,
    statusActions:StatusActionClass,
    statusActionTrigger:StatusActionTriggerClass,
    statusActionTriggers:StatusActionTriggerClass,
    actionFunction:ActionFunctionClass,
    actionFunctions:ActionFunctionClass,
    systemFunction:SystemFunctionClass,
    systemFunctions:SystemFunctionClass,
    documentType:DocumentTypeClass,
    documentTypes:DocumentTypeClass,
    requirement:RequirementClass,
    requirements:RequirementClass,
    mail:MailClass,
    mails:MailClass,
    position:PositionClass,
    positions:PositionClass,
    positionUnit:PositionUnitClass,
    positionUnits:PositionUnitClass,
    house:HouseClass,
    houses:HouseClass,
    drivers:EmployeeClass,
    passengers:EmployeeClass,
    car:CarClass,
    cars:CarClass,
    driverCars:CarClass,
    passengerCars:CarClass,
    image:ImageClass,
    images:ImageClass,
    offer:OfferClass,
    offers:OfferClass,
    recruitmentProcess:RecruitmentClass,
    recruitmentProcesses:RecruitmentClass,
    recruitment:RecruitmentClass,
    recruitments:RecruitmentClass,
    stage:StatusClass,
    stages:StageClass
}

const ColumnBunch = {
    employees:EmployeeShortColumn,
    employee:EmployeeShortColumn,
    companies:CompanyShortColumn,
    company:CompanyShortColumn,
    realisations:RealisationColumn,
    realisation:RealisationColumn,
    variantRealisations:VariantRealisationColumn,
    variantRealisation:VariantRealisationColumn,
    variantType:VariantTypeColumn,
    variantTypes:VariantTypeColumn,
    document:DocumentShortColumn,
    documents:DocumentShortColumn,
    status:StatusColumn,
    transitionTo:StatusColumn,
    statusAction:StatusActionColumn,
    statusActions:StatusActionColumn,
    actionFunction:ActionFunctionColumns,
    actionFunctions:ActionFunctionColumns,
    documentType:DocumentTypeColumn,
    documentTypes:DocumentTypeColumn,
    requirement:RequirementColumn,
    requirements:RequirementColumn,
    mail:MailColumns,
    mails:MailColumns,
    positionUnit:PositionColumn,
    positionUnits:PositionColumn,
    position:PositionColumn,
    positions:PositionColumn,
    house:HouseColumns,
    houses:HouseColumns,
    passengers:EmployeeShortColumn,
    drivers:EmployeeShortColumn,
    car:CarColumns,
    cars:CarColumns,
    driverCars:CarColumns,
    passengerCars:CarColumns,
    image:DocumentColumn,
    images:DocumentColumn,
    offer:OfferColumns,
    offers:OfferColumns,
    recruitmentProcess:RecruitmentColumns,
    recruitmentProcesses:RecruitmentColumns,
    recruitment:RecruitmentColumns,
    recruitments:RecruitmentColumns,
    variant:VariantColumns,
    variants:VariantColumns,
    stage:StageColumns,
    stages:StageColumns
}
const LifeBunch = {
    employees:EmployeeLife,
    employee:EmployeeLife,
    companies:CompanyLife,
    company:CompanyLife,
    realisations:Life,
    realisation:Life,
    requirement:Life,
    requirements:Life,
    variantRealisations:Life,
    variantRealisation:Life,
    variantType:Life,
    variantTypes:Life,
    document:DocumentLife,
    documents:DocumentLife,
    status:StatusLife,
    transitionTo:StatusLife,
    variant:VariantLife,
    variants:VariantLife,
    statusAction:Life,
    statusActions:Life,
    systemFunction:Life,
    systemFunctions:Life,
    actionFunction:Life,
    actionFunctions:Life,
    documentType:DocumentTypeLife,
    documentTypes:DocumentTypeLife,
    mail:MailLife,
    mails:MailLife,
    positionUnit:Life,
    positionUnits:Life,
    position:Life,
    positions:Life,
    house:HouseLife,
    houses:HouseLife,
    passengers:EmployeeLife,
    drivers:EmployeeLife,
    car:CarLife,
    cars:CarLife,
    driverCars:CarLife,
    passengerCars:CarLife,
    image:DocumentLife,
    images:DocumentLife,
    offer:OfferLife,
    offers:OfferLife,
    recruitmentProcess:RecruitmentLife,
    recruitmentProcesses:RecruitmentLife,
    recruitment:RecruitmentLife,
    recruitments:RecruitmentLife,
    stage:Life,
    stages:Life,
}
const CrudBunch = {
    employees:EmployeeCrudController,
    employee:EmployeeCrudController,
    companies:CompanyCrudController,
    company:CompanyCrudController,
    document:DocumentCrudController,
    documents:DocumentCrudController,
    status:StatusCrudController,
    transitionTo:StatusCrudController,
    variant:VariantCrudController,
    variants:VariantCrudController,
    statusAction:StatusActionCrudController,
    statusActions:StatusActionCrudController,
    documentType:DocumentTypeCrudController,
    documentTypes:DocumentTypeCrudController,
    actionFunction:ActionFunctionCrudController,
    actionFunctions:ActionFunctionCrudController,
    mail:MailCrudController,
    mails:MailCrudController,
    house:HouseCrudController,
    houses:HouseCrudController,
    passengers:EmployeeCrudController,
    drivers:EmployeeCrudController,
    car:CarCrudController,
    cars:CarCrudController,
    driverCars:CarCrudController,
    passengerCars:CarCrudController,
    images:DocumentCrudController,
    image:DocumentCrudController,
    offer:OfferCrudController,
    offers:OfferCrudController,
    recruitmentProcess:RecruitmentCrudController,
    recruitmentProcesses:RecruitmentCrudController,
    recruitment:RecruitmentCrudController,
    recruitments:RecruitmentCrudController,
    stage:StageCrudController,
    stages:StageCrudController

}

export {ServiceBunch,ClassBunch, ColumnBunch, LifeBunch, CrudBunch}