

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
                    headerClass: "mdi mdi-car biggerFont flexCenter",
                    cellClass: "mdi mdi-car biggerFont  active-update-cell-custom"
                },

            ]
        },
        {

            headerName: 'Details',
            children: [
                {
                    headerName: 'Code',
                    field: 'code',
                    minWidth: 200,
                },
                {
                    headerName: 'Vin',
                    field: 'vin',
                    minWidth: 200,
                },
                {
                    headerName: 'Brand',
                    field: 'brand',
                    minWidth: 200,
                },
                {
                    headerName: 'Produced',
                    field: 'produced',
                    minWidth: 200,
                },
                {
                    headerName: 'Bought',
                    field: 'bought',
                    minWidth: 200,
                },
                {
                    headerName: 'Documents',
                    collectionKey:'documents',
                    valueGetter:params => params.data?.documents?.length,
                    cellClass: `active-cell-custom`
                },
            ]
        }
    ]
}


