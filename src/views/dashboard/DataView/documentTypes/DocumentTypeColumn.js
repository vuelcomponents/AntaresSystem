import moment from "moment";

export default () => {
    return[
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
                    headerName: 'Code',
                    field: 'code',
                    minWidth: 200,

                },
                {
                    headerName: 'Description',
                    field: 'description',
                    minWidth: 200,
                },
            ]
        },
    ]
}