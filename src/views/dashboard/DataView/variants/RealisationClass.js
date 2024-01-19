import ModelClass from "@/core/ModelClass";
import {VariantService} from "@/services/VariantService";
import {VariantRealisationService} from "@/services/VariantRealisationService";
import {EmployeeService} from "@/services/EmployeeService";
import {CompanyService} from "@/services/CompanyService";
import EmployeeColumns from "@/views/dashboard/DataView/employees/EmployeeColumns";
import CompanyColumns from "@/views/dashboard/DataView/companies/CompanyColumns";
import moment from "moment";

export default class extends ModelClass{
    id;
    variant;
    variantRealisation;
    employees;
    dateValue;
    numericValue;
    companies;
    headers = {
        id: "Id",
        variant:"Variant",
        variantRealisation:"Variant realisation",
        employees:"Employees",
        companies:"Companies",
    }
    text= []
    required = []
    dates = [
        'dateValue'
    ]
    numeric = [
        'numericValue'
    ]
    collections = [
        'employees',
        'companies'
    ]
    objects = [
        'variant',
        'variantRealisation'
    ]
    create = []
    select = []
    delete = []
    update = []
    $filters = {
        variant:(instance, variants, owner)=>{
            if(!variants){
                return variants
            }
            return variants.filter(v=>v.storeModel?.id === owner.storeModel?.id)
        },
        variantRealisation:(instance, variantRealisations)=>{
            const variant = instance?.variant;
            if(!variant || !variant.id || !variantRealisations){
                return variantRealisations;
            }
            return variantRealisations.filter(vr=> {
                if(!vr.variant){
                    return false;
                }
                return vr.variant?.id === variant.id
            })
        }
    }
    $lock = {
        variantRealisation:(options)=>{
            if(!this.variant){
                return true;
            }
            return this.variant?.variantType?.id !== 3;

        },
        dateValue:(options)=>{
            if(!this.variant){
                return true;
            }
            return this.variant?.variantType?.id !== 2;

        },
        numericValue:(options)=>{
            if(!this.variant){
                return true;
            }
            return this.variant?.variantType?.id !== 1;
        }
    }
    $hide = {
        variantRealisation:(options)=>{
            if(!this.variant){
                return true;
            }
            return this.variant?.variantType?.id !== 3;
        },
        dateValue:(options)=>{
            if(!this.variant){
                return true;
            }
            return this.variant?.variantType?.id !== 2;
        },
        numericValue:(options)=>{
            if(!this.variant){
                return true;
            }
            return this.variant?.variantType?.id !== 1;
        }
    }
    decycle() {
        const _ = super.decycle()
        return _;
    }
}