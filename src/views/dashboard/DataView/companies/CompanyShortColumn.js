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
                    headerName: 'Description*',
                    field: 'description',
                    minWidth: 200,
                },

                {
                    headerName: 'Address',
                    field: 'address',
                    minWidth: 200,
                    children: [
                        {
                            headerName: 'House and Local Number',
                            field: 'houseAndLocalNumber',
                            minWidth: 200,
                        },
                        {
                            headerName: 'Street Name',
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
                            headerName: 'Country',
                            field: 'country',
                            minWidth: 200,
                        },
                    ]
                },

            ]
        }
    ]
}


