import ModelClass from "@/core/ModelClass";


export default class extends ModelClass{
    id;
    code;
    description;
    companies;
    employees;
    realisations;
    /* !important - any file needs to have 3 objects anynameData, anynameName,anynameExtension */
    fileData;
    fileName;
    fileExtension;
    storeModel;
    dateFrom;
    dateTo;
    status;
    documentType;
    houses;
    cars;
    headers = {
        id: "Id",
        code:"Code",
        description:'Description',
        'fileName':'File Name',
        companies:"Companies",
        employees:'Employees',
        'dateFrom':'Date from',
        'dateTo':'Date to',
        'fileData':"Document file",
        realisations:'Realisations',
        status:'Status',
        documentType:'Document type',
        houses:"Agency accommodation",
        cars:"Cars",
    }
    text= [
        'code',
        'description',
    ]
    required = [
        'code',
        'description',
        'fileData',
        'documentType'
    ]
    dates = [
        'dateFrom',
        'dateTo'
    ]
    numeric = []
    files = [
        'fileData'
    ]
    collections = [
        'realisations',
        'employees',
        'companies',
        'cars',
        'houses'
    ]
    create = [
        'realisations'
    ]
    select = [
        'companies',
        'employees',
        'cars',
        'houses'
    ]
    delete = [
        'realisations',
        'companies',
        'employees',
        'cars',
        'houses'
    ]
    update = [
        'realisations'
    ]
    objects = [
        'status',
        'documentType'
    ]
    $hide = {
        employees:(options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            if(options?.colman){
                return true;
            }
            return false;
        },
        cars:(options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            if(options?.colman){
                return true;
            }
            return false;
        },
        houses:(options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            if(options?.colman){
                return true;
            }
            return false;
        },
        companies:(options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            if(options?.colman){
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
            if(options?.colman){
                return true;
            }
            return false;
        },
    }
    $filters = {
        status:(model, collection)=>{
            return collection?.filter(c=>c.storeModel.code === 'Document');
        }
    }
    decycle() {
        const _ = super.decycle()
        if(this.fileData){
            _.fileData = this.fileData
        }
        return _;
    }

}