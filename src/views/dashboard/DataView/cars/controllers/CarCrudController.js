import CarClass from "@/views/dashboard/DataView/cars/CarClass";


export default class {
    service = undefined;
    options = null;
    constructor(service, options) {
        this.service = service;
        this.options = options;
    }

    async getAll(){
        return this.service.getAll()
    }
    async getById(id){
        const car = (await this.service.getById(id)).data
        return CarClass.toModel(car, CarClass)
    }
    async create(car){
        car.id = null;
        return this.service.create(car)
    }
    async update(car){
        const invalid = car.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(car)
    }
    async delete(id){
        return this.service.delete(id)
    }
    async deleteMultiple(companies){
        return this.service.deleteMultiple(companies)
    }

    async saveChanges(){
        console.log('save changes')
    }
}
