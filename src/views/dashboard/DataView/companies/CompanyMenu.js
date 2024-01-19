export default (createCompany, cancelChanges, deleteSelected, saveChanges, loadList, quickAdd) =>{
    return [
        {
            label:'Filters',
            icon:'mdi mdi-filter'
        },
        {
            label:'Actions',
            icon:'mdi mdi-wrench',
            items:[
                {
                    label:'Create company',
                    command:()=>{
                        createCompany()
                    },
                    icon:'mdi mdi-link-plus'
                },
                {
                    label:'Delete selected',
                    icon:'mdi mdi-minus',
                    command:()=>{
                        deleteSelected()
                    }
                },
                {
                    label:'Reload list',
                    icon:'mdi mdi-reload',
                    command:()=>{
                        loadList()
                    }
                }
            ]
        },
        {
            label:'Quick form',
            command:()=>{
                quickAdd()
            },
            icon:'mdi mdi-lightning-bolt'
        },
    ]
}