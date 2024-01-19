import ModelClass from "@/core/ModelClass";
import RealisationService from "@/services/RealisationService";
import RealisationColumn from "@/views/dashboard/DataView/variants/RealisationColumn";

export default class extends ModelClass {

    id;
    code;
    documents;
    min;
    max;
    requirements;
    employees;
    companies;
    houseAndLocalNumber ;
    streetName  ;
    postCode
    city
    cars
    headers = {
        id: "Id",
        code:"Code",
        documents:"Documents",
        min:"Min",
        max:"Max",
        employees:"Employees",
        companies:"Companies",
        requirements:"Requirements",
        storeModel:"Model",
        "houseAndLocalNumber": "House and local number",
        "streetName": "Street name",
        "postCode": "Postcode",
        "city": "City",
        "cars":"Cars"
    }
    text = [
        "code",
        "houseAndLocalNumber",
        "streetName",
        "postCode",
        "city",
    ]
    numeric = [
        'min',
        'max',
    ]
    required = [
        "code",
        "houseAndLocalNumber",
        "streetName",
        "postCode",
        "city",
    ]
    collections = [
        "documents",
        "requirements",
        "employees",
        "companies",
        "cars"
    ]
    create = [
        "documents",
        "requirements"
    ]
    select = [
        "documents",
        "employees",
        "companies",
        "cars"
    ]
    delete = [
        "documents",
        "requirements",
        "employees",
        "companies",
        "cars"
    ]
    update = [
        "documents",
        "requirements"
    ]

    objects = [
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
        requirements: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
        employees: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
        cars: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
        companies: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },

    }
    $filters = {

    }



}