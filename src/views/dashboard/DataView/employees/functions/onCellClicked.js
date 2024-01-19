export default (params, $) =>{
    switch(params.colDef?.id){
        case "goToUpdate":
            $.router.push({name:'EmployeeUpdate', params:{id:params.data.id}})
            break;
    }

}