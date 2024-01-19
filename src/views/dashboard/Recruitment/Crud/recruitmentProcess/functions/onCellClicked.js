import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";

export default (params, $) =>{
    switch(params.colDef.id){
        case "goToUpdate":
            $.router.push({name: 'RecruitmentUpdate', params:{id:params.data.id}})
            break;
        case "documentStage":
            $.toast.success("Document stage")
            $.life.documentEmployee = EmployeeClass.toModel(params.data, EmployeeClass)
            break;
        case "matchingStage":
            $.toast.success("Matching stage")
            $.life.matchingEmployee = EmployeeClass.toModel(params.data, EmployeeClass).decycle()
            break;
        case "contactStage":
            $.toast.success("Contact stage")
            $.life.contactEmployee = EmployeeClass.toModel(params.data, EmployeeClass).decycle()
            break;
        case "decideStage":
            $.toast.success("Decide stage")
            $.life.decideEmployee = EmployeeClass.toModel(params.data, EmployeeClass).decycle()
            break;
        case "hire":
            $.services.emp.hire(params.data).then(res=>{
                if($.loadList){
                    $.toast.success('Employee has been hired. Status functions resolved')
                    $.loadList()
                }
            })
            break;
        case "fire":
            $.services.emp.fire(params.data).then(res=>{
                if($.loadList){
                    $.loadList().then(()=>{
                        $.toast.error('Employee has been removed from recruitment process. Status has been set to previous state')
                    })
                }
            })
            break;
    }
}