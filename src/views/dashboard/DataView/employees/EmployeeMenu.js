export default (createEmployee, cancelChanges,deleteSelected, saveChanges, loadList,quickAdd) => {
    const menu = [
        {
            label:'Filters',
            icon:'mdi mdi-filter'
        },
        {
            label:'Actions',
            icon:'mdi mdi-wrench',
            items:[
                {
                  label:'Custom actions',
                  items:[]
                },
                {
                    label:'Create employee',
                    command:()=>{
                       createEmployee()
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
                    disabled:()=>{
                        return false;
                    },
                    command:()=>{
                        loadList()
                    }
                },
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
    menu[1].items[0].items.push({
        label:'koks'
    })
return menu;
}