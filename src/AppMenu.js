import {setTheme} from "@/core/theme";

export default [
    {
        label:'Web Applications',
        icon:'mdi mdi-webhook',
        items:[
            {
                label: 'Antares, HR System ',
            }
        ]
    },
    {
        label:'Settings',
        icon:'mdi mdi-wrench',
        items:[
            {
                label:'Language',
                items:[
                    {
                        label:'Polish',
                    },
                    {
                        label:'English',
                    },
                    {
                        label:'Dutch',
                    },
                    {
                        label:'German',
                    },
                ]
            },
            {
                label:'Theme',
                items:[
                    {
                        label:'Casual',
                        command:()=>{setTheme(null)}
                    },
                    {
                        label:'RedShark',
                        command:()=>{setTheme('redShark')}
                    },
                    {
                        label:'Light',
                        command:()=>{setTheme('lightTheme')}
                    },
                ]
            }
        ]
    },
    {
        label:'Contact',
        icon:'mdi mdi-cellphone',
        items:[
            {
                label:'+ 48 798 221 083',
                icon:'mdi mdi-cellphone'
            },
            {
                label:'dev@devsharks.com',
                icon:'mdi mdi-email'
            },

        ]
    },
]