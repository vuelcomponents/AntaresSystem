import MainWebPage from "@/views/webpage/MainWebPage.vue";
import LoginWebPage from "@/views/webpage/auth/LoginWebPage.vue";

export default [
    {
        path:'/webpage',
        name:'MainWebpage',
        component:MainWebPage,
        children:[
            {
                path: 'login',
                name:"LoginWebPage",
                component:LoginWebPage
            }
        ]
    }
]