import ModelClass from "@/core/ModelClass";
import VariantRealisationClass from "@/views/dashboard/DataView/variants/VariantRealisationClass";
import {VariantRealisationService} from "@/services/VariantRealisationService";
import VariantRealisationColumn from "@/views/dashboard/DataView/variants/VariantRealisationColumn";

export default class extends ModelClass{
   id;
   code;
   description;
   variantRealisations;
   variantType;
   storeModel;
   global;
   headers = {
      id: "Id",
      code:"Code",
      description:"Description",
      variantRealisations:"Variant realisations",
      variantType:"Variant type",
      storeModel:"Destiny",
      global:"Global"
   }
   booleans = [
       "global"
   ]
   text= [
        'code',
       'description'
   ]
   required = [
       'code',
       'description',
       'variantType',
       'storeModel'
   ]
   dates = []
   numeric = []
   collections = [
       'variantRealisations'
   ]
    create = [
        'variantRealisations'
    ]
    select = [

    ]
    delete = [
        'variantRealisations',
    ]
    update = [
        'variantRealisations',
    ]
    objects = [
        'variantType',
        'storeModel'
    ]
    $hide = {
       variantRealisations: (options)=>{
           if(!this.id){
               return true;
           }
           if(this.id === 0){
               return true;
           }
           return this.variantType?.id !== 3;
       },
        global: (options)=>{
            return this.storeModel?.code !== 'Employee'
        },
       variantType: (options)=>{
              return this.id;
       },
        storeModel: (options)=>{
            return this.id;
        },
    }

}