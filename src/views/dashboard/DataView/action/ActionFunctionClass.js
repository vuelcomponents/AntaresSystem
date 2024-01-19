import ModelClass from "@/core/ModelClass";


export default class extends ModelClass{
   id;
   code;
   description;
   storeModel;
   systemFunction;
   requirements = []
   company;
   mail;
   position;
   headers = {
      id: "Id",
      code:"Code",
      description:"Description",
      storeModel:"Model",
      systemFunction:"System function",
      documents:"Attach Documents",
      documentType:"All selected document types recognized with chosen object will be sent",
      requirements:"Requirements",
      company:'Company',
      mail:'Mail',
      position:"Position",
   }
   colors = [
   ]
   text= [
      'code',
      'description',
   ]
   textAreaHeaders= [
   ]
   textArea = [

   ]
   required = [
     'code',
      'storeModel',
      'systemFunction',
       'description',
       'company',
       'mail',
       'position'
   ]
   dates = [

   ]
   numeric = [
   ]
   files = [
   ]
   collections = [
       'requirements'
   ]
   create = [
       'requirements'
   ]
   select = [

   ]
   delete = [
       'requirements'
   ]
   update = [
   ]
   objects = [
      "storeModel",
      "systemFunction",
       "company",
       'mail',
       'position'
   ]
   // selectsExcludedFromApi = [
   //     'documents'
   // ]
   $lock={
      storeModel:()=>this.id
   }
   $hide = {
      mail:(options)=>{
         return modelComparison({systemFunction:this.systemFunction, storeModel:this.storeModel}, 'Mail')
      },
      // message:(options)=>{
      //    return modelComparison({systemFunction:this.systemFunction, storeModel:this.storeModel}, 'Mail')
      // },
      company:(options)=>{
         return modelComparison({systemFunction:this.systemFunction, storeModel:this.storeModel}, 'Terminate employment')
                  && modelComparison({systemFunction:this.systemFunction, storeModel:this.storeModel}, 'Hire')
      },
      position:(options)=>{
         return modelComparison({systemFunction:this.systemFunction, storeModel:this.storeModel}, 'Terminate employment')
             && modelComparison({systemFunction:this.systemFunction, storeModel:this.storeModel}, 'Hire')
      },
      requirements: (options)=>{
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
      systemFunction:(instance, systemFunctions, owner)=>{
         if(!systemFunctions){
            return systemFunctions
         }
         return systemFunctions.filter(sf=>sf.storeModels?.some(sm=>sm.id === owner.storeModel?.id ));
      },
      position:(model,collection)=>{
         return collection.filter(c=>c.company.id === model.company?.id)
      }
   }

}

const modelComparison = ($, functionCode) =>{
   if(!$.systemFunction?.storeModels){
      return true;
   }
   if(!$.storeModel?.id){
      return true;
   }
   if($.systemFunction?.storeModels.some(sm=>sm.id === $.storeModel.id) && $.systemFunction.code === functionCode){
      return false;
   }
   console.log('$', $)
   return true;
}