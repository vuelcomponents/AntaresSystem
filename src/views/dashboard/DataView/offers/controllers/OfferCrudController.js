
import OfferClass from "@/views/dashboard/DataView/offers/OfferClass";
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
        const offer = (await this.service.getById(id)).data
        return OfferClass.toModel(offer, OfferClass)
    }
    async create(offer){
        offer.id = null;
        return this.service.create(offer)
    }
    async update(offer){
        const invalid = offer.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(offer)
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
