

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
                    headerClass: "mdi mdi-home biggerFont flexCenter",
                    cellClass: "mdi mdi-home biggerFont  active-update-cell-custom"
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
                    headerName: 'House and local number',
                    field: 'houseAndLocalNumber',
                    minWidth: 200,
                },
                {
                    headerName: 'Street name',
                    field: 'streetName',
                    minWidth: 200,
                },
                {
                    headerName: 'Postcode',
                    field: 'postCode',
                    minWidth: 200,
                },
                {
                    headerName: 'City',
                    field: 'city',
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


