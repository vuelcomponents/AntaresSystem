import ModelClass from "@/core/ModelClass";
import RealisationService from "@/services/RealisationService";
import RealisationColumn from "@/views/dashboard/DataView/variants/RealisationColumn";

export default class extends ModelClass {

    id;
    code;
    description;
    houseAndLocalNumber;
    streetName;
    postCode;
    city;
    country;
    realisations;
    documents;
    status;
    positions;
    employees;
    headers = {
        id: "Id",
        code:"Code",
        description:"Description",
        houseAndLocalNumber:"House and local number",
        streetName:"Street name",
        postCode:"Postcode",
        city:"City",
        country:"Country",
        realisations:"Realisations",
        documents:"Documents",
        status:"Status",
        positions:"Positions",
        employees:"Employees",
        houses:"Agency accommodation",
        cars:"Cars"
    }
    text = [
        "description",
        "code",
        "houseAndLocalNumber",
        "streetName",
        "postCode",
        "city",
        "country"
    ]
    numeric = []
    required = [
        "code",
        "description"
    ]
    collections = [
        'realisations',
        'documents',
        'positions',
        'employees',
        'houses',
        'cars'
    ]
    create = [
        'realisations',
        'documents',
        'positions'
    ]
    select = [
        'documents',
        'employees',
        'houses',
        'cars'
    ]
    delete = [
        'realisations',
        'documents',
        'houses',
        'positions',
        'employees',
        'cars'
    ]
    update = [
        'realisations',
        'documents',
        'positions',
        'employees'
    ]

    objects = [
        'status'
    ]
    $hide = {
        realisations: (options) => {
            if (!this.id) {
                return true;
            }
            if (this.id === 0) {
                return true;
            }
            return false;
        },
        positions: (options) => {
            if (!this.id) {
                return true;
            }
            if (this.id === 0) {
                return true;
            }
            return false;
        },
        cars: (options) => {
            if (!this.id) {
                return true;
            }
            if (this.id === 0) {
                return true;
            }
            return false;
        },
        employees: (options) => {
            if (!this.id) {
                return true;
            }
            if (this.id === 0) {
                return true;
            }
            return false;
        },
        houses: (options) => {
            if (!this.id) {
                return true;
            }
            if (this.id === 0) {
                return true;
            }
            return false;
        },
        documents: (options)=>{
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
        status:(model, collection)=>{
            return collection.filter(c=>c.storeModel.code === 'Company');
        }
    }



}