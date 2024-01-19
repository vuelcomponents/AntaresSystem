

export default () => {
    return [
        {
            groupId: 'options',
            headerName: 'Options',

            children: [
                {
                    headerCheckboxSelection: true,
                    checkboxSelection: true,
                    showDisabledCheckboxes: true,
                    filter: false,
                    width: 55,
                },
                {
                    headerName: "",
                    width: 15,
                    id: 'goToUpdate',
                    headerClass: "mdi mdi-briefcase-outline biggerFont flexCenter",
                    cellClass: "mdi mdi-briefcase-outline biggerFont  active-update-cell-custom"
                },

            ]
        },
        {

            headerName: 'Details',
            children: [
                {
                    headerName: 'Code*',
                    field: 'code',
                    minWidth: 200,

                },
                {
                    headerName: 'Description',
                    field: 'description',
                    minWidth: 200,
                },
                {
                    headerName: 'Job offer',
                    field: 'offer',
                    owner:'recruitment',
                    cellRenderer:'SelectionRenderer',
                    editable:false,
                    minWidth: 200,
                },
                {
                    headerName: 'Position',
                    field: 'position',
                    valueGetter:params=> params.data.offer?.position?.code,
                    editable:false,
                    minWidth: 200,
                },
                {
                    headerName: 'Company',
                    field: 'company',
                    valueGetter:params=> params.data.offer?.company?.code,
                    editable:false,
                    minWidth: 200,
                },
                {
                    headerName: 'Variants',
                    collectionKey:'variants',
                    valueGetter:params => params.data?.variants?.length,
                    cellClass: `active-cell-custom`
                },
                {
                    headerName: 'Employees',
                    collectionKey:'employees',
                    valueGetter:params => params.data?.employees?.length,
                    cellClass: `active-cell-custom`
                },
                {
                    headerName: 'Document Types',
                    collectionKey:'documentTypes',
                    valueGetter:params => params.data?.documentTypes?.length,
                    cellClass: `active-cell-custom`
                },
                {
                    headerName: 'Hire Status',
                    editable:false,
                    field:'status.code'
                },
            ]
        }
    ]
}


