import toLocaleStr from "@/utils/functions/toLocaleStr";

export default () => {
    return [
        {
            headerName: 'Details',
            children: [

                {
                    headerCheckboxSelection: true,
                    checkboxSelection: true,
                    showDisabledCheckboxes: true,
                    filter: false,
                    width: 55,
                },
                {
                    headerName:"",
                    width: 15,
                    id:'goToUpdate',
                    headerClass: "mdi mdi-access-point biggerFont flexCenter  active-update-cell-custom",
                    cellClass:"mdi mdi-access-point biggerFont  active-update-cell-custom",
                },
                {
                    headerName: 'Code',
                    field: 'code',
                    minWidth: 200,

                },
                {
                    headerName: 'Color',
                    field: 'color',
                    cellRenderer: 'ColorPickerRenderer',
                    editable:false,
                },
                {
                    headerName: 'Description',
                    field: 'description',
                    minWidth: 200,
                },
                {
                    headerName: 'Actions',
                    collectionKey:'statusActions',
                    valueGetter:params => params.data?.statusActions?.length,
                    cellClass: `active-cell-custom`,
                    editable:'false'
                },
                {
                    headerName: 'Priority',
                    field: 'priority',
                    valueGetter:params=> toLocaleStr(params.data?.priority),
                    minWidth: 200,
                },
                {
                    headerName: 'Transition to',
                    collectionKey:'transitionTo',
                    valueGetter:params => params.data?.transitionTo?.length,
                    cellClass: `active-cell-custom`
                },
                {
                    headerName: 'Model',
                    field: 'storeModel',
                    valueGetter:params=> params.data?.storeModel?.code,
                    minWidth: 200,
                },
            ]
        }
    ]
}


