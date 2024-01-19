export default (params, $) =>{
    switch(params.colDef?.id){
        case "goToUpdate":
            $.router.push({name:'StatusUpdate', params:{id:params.data.id}})
            break;
    }
}