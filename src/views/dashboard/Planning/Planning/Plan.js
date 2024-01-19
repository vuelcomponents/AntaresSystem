

import ModelClass from "@/core/ModelClass";

export default class extends ModelClass{
    id;
    title;
    content;
    class = 'inherit';
    employee;
    company;
    start;
    stop;
    headers = {
        id: "Id",
        firstName:"First name",
        email:"Email",
        lastName:"Second name",
        dateOfBirth:"Date of birth",
        privatePhone:"Private phone",
        houseAndLocalNumber:"House and local number",
        streetName:"Street name",
        postCode:"Postcode",
        city:"City",
        subHouseAndLocalNumber:"House and local number(2)",
        subStreetName:"Street name(2)",
        subPostcode:"Postcode(2)",
        subCity:"City(2)",
        realisations:"Realisations",
        companies:'Companies',
        documents:'Documents',
        status:'Status',
        positions:'Positions',
        driverCars:"Driver cars",
        passengerCars:"Passenger cars",
        houses:"Agency accommodation",
        pesel:"Personal ID",
        bsn:"Tax ID"
    }
    text= [
        'firstName',
        'lastName',
        'email',
        'privatePhone',
        'houseAndLocalNumber',
        'streetName',
        'postCode',
        'city',
        'subHouseAndLocalNumber',
        'subStreetName',
        'subPostcode',
        'subCity',
        'bsn',
        'pesel'
    ]
    required = [
        'firstName',
        'lastName',
        'email'
    ]
    dates = [
        'dateOfBirth'
    ]
    numeric = []
    collections = [
        'realisations',
        'companies',
        'documents',
        'positions',
        'driverCars',
        'houses',
        'passengerCars'
    ]
    create = [
        'realisations',
        'documents'
    ]
    select = [
        'companies',
        'documents',
        'positions',
        'houses',
        'driverCars',
        'passengerCars'

    ]
    delete = [
        'realisations',
        'companies',
        'documents',
        'positions',
        'houses',
        'driverCars',
        'passengerCars'
    ]
    update = [

    ]

    objects = [
        'status'
    ]
    $hide = {
        realisations: (options)=>{
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
        positions: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
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
        passengerCars: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
        driverCars: (options)=>{
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
        status:(model, collection)=>{
            return collection.filter(c=>c.storeModel.code === 'Employee');
        },
        employees:(instance, employees, owner)=>{

            //rozpatrzenie dla przydzielania employeesow do position
            if(owner.companyId && owner.positionUnit) {
                return employees.filter(e => e.companies.some(c => c.id === owner.companyId))
            }
            return employees;
        }



    }





}