
import VariantClass from "@/views/dashboard/DataView/variants/VariantClass";
import toModel from "@/core/toModel";
import DocumentClass from "@/views/dashboard/DataView/DocumentClass";

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
        console.log('to id', id)
        const variant = (await this.service.getById(id)).data;
        return VariantClass.toModel(variant, VariantClass)
    }
    async create(variant){
        const invalid = variant.validate({toast: this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        variant.id = null;
        return this.service.create(variant)
    }
    async update(variant){
        const invalid = variant.validate({toast: this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(variant)
    }
    async delete(id){
        return this.service.delete(id)
    }
    async deleteMultiple(variants){
        return this.service.deleteMultiple(variants)
    }

    async saveChanges(){
        console.log('save changes')
    }
}
