import ModelClass from "@/core/ModelClass";

export default class extends ModelClass {

    id;
    code;
    description;
    positionUnit;
    demand;
    storeModel;
    headers = {
        id: "Id",
        code:"Code",
        description:"Description",
        demand:"Demand"
    }
    text = [
        "description",
        "code",
    ]
    numeric = [
        "demand"
    ]
    required = [
        "code",
        "description",
        "positionUnit"
    ]
    collections = [

    ]
    create = [
    ]
    select = [
    ]
    delete = [
    ]
    update = [
    ]

    objects = [
        'positionUnit'
    ]
    $hide = {
        // realisations: (options) => {
        //     if (!this.id) {
        //         return true;
        //     }
        //     if (this.id === 0) {
        //         return true;
        //     }
        //     return false;
        // },
        // documents: (options)=>{
        //     if(!this.id){
        //         return true;
        //     }
        //     if(this.id === 0){
        //         return true;
        //     }
        //     return false;
        // },

    }
    $filters = {
        positions:(instance, positions, owner)=>{
            if(!positions){
                return positions
            }
            const list =  positions.filter(p=>owner.companies?.some(oc=>oc.id === p.company?.id))
            if(owner?.storeModel?.code === "Employee"){
                return list.filter(l=>l.positionUnit.code === "Position")
            }
            return list;
        },
    }



}