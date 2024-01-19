
import HouseClass from "@/views/dashboard/DataView/houses/HouseClass";

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
        const house = (await this.service.getById(id)).data
        return HouseClass.toModel(house, HouseClass)
    }
    async create(house){
        house.id = null;
        return this.service.create(house)
    }
    async update(house){
        const invalid = house.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(house)
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
