import ModelClass from "@/core/ModelClass";
export default class extends ModelClass {

    id;
    code;
    documents;
    min;
    max;
    realisations;
    passengers;
    drivers;
    companies;
    houses;
    vin;
    brand;
    produced;
    bought;
    headers = {
        id: "Id",
        code:"Code",
        documents:"Documents",
        min:"Min",
        max:"Max",
        drivers:"Drivers",
        passengers:"Passengers",
        companies:"Companies",
        realisations:"Realisations",
        houses:"Houses",
        vin:"Vin",
        brand:"Brand",
        produced:"Produced",
        bought:"Bought"
    }
    text = [
        "code",
        "vin",
        "brand"
    ]
    dates=[
        'produced',
        'bought'
    ]
    numeric = [
        'min',
        'max',
    ]
    required = [
        "code",
        "vin",
        "brand",
        "produced"

    ]
    collections = [
        "documents",
        "realisations",
        "passengers",
        "drivers",
        "companies",
        "houses"
    ]
    create = [
        "documents",
        "realisations"
    ]
    select = [
        "documents",
        "passengers",
        "drivers",
        "companies",
        "houses"
    ]
    delete = [
        "documents",
        "realisations",
        "passengers",
        "drivers",
        "companies",
        "houses"
    ]
    update = [
        "documents",
        "realisations"
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
        realisations: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
        passengers: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
        houses: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
        drivers: (options)=>{
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