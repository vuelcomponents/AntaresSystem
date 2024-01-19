export default (params, $) =>{
    switch(params.colDef?.id){
        case "goToUpdate":
            $.router.push({name:'ActionFunctionUpdate', params:{id:params.data.id}})
            break;
    }

}