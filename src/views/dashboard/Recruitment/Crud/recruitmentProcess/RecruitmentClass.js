import ModelClass from "@/core/ModelClass";
import RealisationService from "@/services/RealisationService";
import RealisationColumn from "@/views/dashboard/DataView/variants/RealisationColumn";

export default class extends ModelClass {

    id;
    code;
    description;
    message;
    documents;
    offer;
    employees;
    variants;
    status;
    headers = {
        id: "Id",
        code:"Code",
        description:"Title",
        message:"Message",
        documents:"Documents",
        offer:"Job offer",
        company:"Company",
        position:"Position",
        employees:"Employees",
        variants:"Variants",
        status:"Hire Status"
    }
    text = [
        "code",
        'description',
    ]
    numeric = []
    required = [
        "code",
        "description",
        "offer",
        "company",
        "position",
        "variants",
        "status",

    ]
    collections = [
        'employees',
        'variants',
    ]
    create = [
        'stages'
    ]
    select = [
        'employees',
        'variants',
        'documentTypes'
    ]
    delete = [
        'employees',
        'variants',
        'documentTypes'
    ]
    update = [
        'variants',
    ]

    objects = [
        'offer',
        'status'
    ]
    textArea = [

    ]
    textAreaHeaders = [

    ]
    $hide = {
        documents: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },

        variants: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
    }
    $lock = {

    }
    $filters = {
        status:(model, collection)=>{
            return collection.filter(s=>s.storeModel?.code === "Employee" && !s.reserved)
        }
    }



}