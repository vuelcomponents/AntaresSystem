export default (create, cancelChanges,deleteSelected, saveChanges, loadList, quickAdd) => {
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
                    label:'Create variant',
                    command:()=>{
                       create()
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

}