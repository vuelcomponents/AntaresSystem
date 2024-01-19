
export default class {
    $router;
    $save;
    constructor(router, save) {
        this.$router = router;
        this.$save = save;
    }
    stepperSettings = {
        items: [
            {
                title: 'Data',
                grid:true,
                template: '1fr 1fr',  // Example grid template, adjust as needed
            },
            {
                title: 'Qualifications',
                grid:true,
                template: '1fr',
            },
            {
                title: 'Payment',
                grid:true,
                template: '1fr 1fr',
            },
        ],
    }
    menuItems = [
        {
            icon: 'mdi mdi-content-save',
            text: 'Item 1',
            command:()=>{
                this.$save()
            }
        },
        {
            icon: 'mdi mdi-settings',
            text: 'Item 2',
        },
        {
            icon: 'mdi mdi-close-box-outline',
            text: 'close',
            command: () => {
                this.$router.back()
            }
        }
    ]
}

